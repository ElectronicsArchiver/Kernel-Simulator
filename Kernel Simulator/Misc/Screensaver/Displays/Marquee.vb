﻿
'    Kernel Simulator  Copyright (C) 2018-2021  EoflaOE
'
'    This file is part of Kernel Simulator
'
'    Kernel Simulator is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    Kernel Simulator is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <https://www.gnu.org/licenses/>.

Imports System.ComponentModel

Module MarqueeDisplay

    Public WithEvents Marquee As New BackgroundWorker With {.WorkerSupportsCancellation = True}

    ''' <summary>
    ''' Handles the code of Marquee
    ''' </summary>
    Sub Marquee_DoWork(sender As Object, e As DoWorkEventArgs) Handles Marquee.DoWork
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.Clear()
        Console.CursorVisible = False
        Dim RandomDriver As New Random()
        MarqueeWrite = MarqueeWrite.ReplaceAll({vbCr, vbLf}, " - ")
        Try
            Do While True
                If Marquee.CancellationPending = True Then
                    Wdbg("W", "Cancellation is pending. Cleaning everything up...")
                    e.Cancel = True
                    SetInputColor()
                    LoadBack()
                    Console.CursorVisible = True
                    Wdbg("I", "All clean. Marquee screensaver stopped.")
                    SaverAutoReset.Set()
                    Exit Do
                Else
                    SleepNoBlock(MarqueeDelay, Marquee)
                    Console.Clear()

                    'Ensure that the top position of the written text is always centered if AlwaysCentered is enabled. Else, select a random height.
                    Dim TopPrinted As Integer = Console.WindowHeight / 2
                    If Not MarqueeAlwaysCentered Then
                        TopPrinted = RandomDriver.Next(Console.WindowHeight - 1)
                    End If
                    WdbgConditional(ScreensaverDebug, "I", "Top position: {0}", TopPrinted)

                    'Start with the left position as the right position.
                    Dim CurrentLeft As Integer = Console.WindowWidth - 1
                    Dim CurrentLeftOtherEnd As Integer = Console.WindowWidth - 1
                    Dim CurrentCharacterNum As Integer = 0

                    'We need to set colors for the text.
                    If MarqueeTrueColor Then
                        Dim RedColorNum As Integer = RandomDriver.Next(255)
                        Dim GreenColorNum As Integer = RandomDriver.Next(255)
                        Dim BlueColorNum As Integer = RandomDriver.Next(255)
                        WdbgConditional(ScreensaverDebug, "I", "Got color (R;G;B: {0};{1};{2})", RedColorNum, GreenColorNum, BlueColorNum)
                        SetConsoleColor(New Color($"{RedColorNum};{GreenColorNum};{BlueColorNum}"))
                    ElseIf Marquee255Colors Then
                        Dim color As Integer = RandomDriver.Next(255)
                        WdbgConditional(ScreensaverDebug, "I", "Got color ({0})", color)
                        SetConsoleColor(New Color(color))
                    Else
                        Console.ForegroundColor = colors(RandomDriver.Next(colors.Length - 1))
                        WdbgConditional(ScreensaverDebug, "I", "Got color ({0})", Console.ForegroundColor)
                    End If

                    'If the text is at the right and is longer than the console width, crop it until it's complete.
                    Do Until CurrentLeftOtherEnd = 0
                        SleepNoBlock(MarqueeDelay, Marquee)
                        If Marquee.CancellationPending Then Exit Do
                        If MarqueeUseConsoleAPI Then Console.Clear()
                        WdbgConditional(ScreensaverDebug, "I", "Current left: {0} | Current left on other end: {1}", CurrentLeft, CurrentLeftOtherEnd)

                        'Declare variable for written marquee text
                        Dim MarqueeWritten As String = MarqueeWrite
                        Dim Middle As Boolean = MarqueeWrite.Length - (CurrentLeftOtherEnd - CurrentLeft) <> CurrentCharacterNum - (CurrentLeftOtherEnd - CurrentLeft)
                        WdbgConditional(ScreensaverDebug, "I", "Middle of long text: {0}", Middle)

                        'If the current left position is not zero (not on the left), take the substring starting from the beginning of the string until the
                        'written variable equals the base text variable. However, if we're on the left, take the substring so that the character which was
                        'shown previously won't be shown again.
                        If Not CurrentLeft = 0 Then
                            MarqueeWritten = MarqueeWritten.Substring(0, CurrentLeftOtherEnd - CurrentLeft)
                        ElseIf CurrentLeft = 0 And Middle Then
                            MarqueeWritten = MarqueeWritten.Substring(CurrentCharacterNum - (CurrentLeftOtherEnd - CurrentLeft), CurrentLeftOtherEnd - CurrentLeft)
                        Else
                            MarqueeWritten = MarqueeWritten.Substring(MarqueeWrite.Length - (CurrentLeftOtherEnd - CurrentLeft))
                        End If
                        WdbgConditional(ScreensaverDebug, "I", "Written result: {0}", MarqueeWritten)
                        If Not MarqueeUseConsoleAPI Then MarqueeWritten += GetEsc() + "[0K"

                        'Set the appropriate cursor position and write the results
                        Console.SetCursorPosition(CurrentLeft, TopPrinted)
                        Console.Write(MarqueeWritten)
                        If Middle Then CurrentCharacterNum += 1

                        'If we're not on the left, decrement the current left position
                        If Not CurrentLeft = 0 Then
                            CurrentLeft -= 1
                        End If

                        'If we're on the left or the entire text is written, decrement the current left other end position
                        If Not Middle Then
                            CurrentLeftOtherEnd -= 1
                        End If
                    Loop
                End If
            Loop
        Catch ex As Exception
            Wdbg("W", "Screensaver experienced an error: {0}. Cleaning everything up...", ex.Message)
            WStkTrc(ex)
            e.Cancel = True
            SetInputColor()
            LoadBack()
            Console.CursorVisible = True
            Wdbg("I", "All clean. Marquee screensaver stopped.")
            W(DoTranslation("Screensaver experienced an error while displaying: {0}. Press any key to exit."), True, ColTypes.Error, ex.Message)
            SaverAutoReset.Set()
        End Try
    End Sub

End Module
