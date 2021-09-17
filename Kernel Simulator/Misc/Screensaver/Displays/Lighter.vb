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

Module LighterDisplay

    Public WithEvents Lighter As New BackgroundWorker With {.WorkerSupportsCancellation = True}

    ''' <summary>
    ''' Handles the code of Lighter
    ''' </summary>
    Sub Lighter_DoWork(sender As Object, e As DoWorkEventArgs) Handles Lighter.DoWork
        Try
            'Variables
            Dim RandomDriver As New Random()
            Dim CoveredPositions As New ArrayList

            'Preparations
            Console.BackgroundColor = ConsoleColor.Black
            Console.Clear()
            Console.CursorVisible = False
            Wdbg(DebugLevel.I, "Console geometry: {0}x{1}", Console.WindowWidth, Console.WindowHeight)

            'Screensaver logic
            Do While True
                If Lighter.CancellationPending = True Then
                    Wdbg(DebugLevel.W, "Cancellation is pending. Cleaning everything up...")
                    e.Cancel = True
                    SetInputColor()
                    LoadBack()
                    Console.CursorVisible = True
                    Wdbg(DebugLevel.I, "All clean. Lighter screensaver stopped.")
                    SaverAutoReset.Set()
                    Exit Do
                Else
                    'Select a position
                    SleepNoBlock(LighterDelay, Lighter)
                    Dim Left As Integer = RandomDriver.Next(Console.WindowWidth)
                    Dim Top As Integer = RandomDriver.Next(Console.WindowHeight)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Selected left and top: {0}, {1}", Left, Top)
                    Console.SetCursorPosition(Left, Top)
                    If Not CoveredPositions.Contains(Left & ";" & Top) Then
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Covering position...")
                        CoveredPositions.Add(Left & ";" & Top)
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Position covered. Covered positions: {0}", CoveredPositions.Count)
                    End If

                    'Select a color and write the space
                    Dim esc As Char = GetEsc()
                    If LighterTrueColor Then
                        Dim RedColorNum As Integer = RandomDriver.Next(255)
                        Dim GreenColorNum As Integer = RandomDriver.Next(255)
                        Dim BlueColorNum As Integer = RandomDriver.Next(255)
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color (R;G;B: {0};{1};{2})", RedColorNum, GreenColorNum, BlueColorNum)
                        Dim ColorStorage As New RGB(RedColorNum, GreenColorNum, BlueColorNum)
                        Console.Write(esc + "[48;2;" + ColorStorage.ToString + "m ")
                    ElseIf Lighter255Colors Then
                        Dim ColorNum As Integer = RandomDriver.Next(255)
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color ({0})", ColorNum)
                        Console.Write(esc + "[48;5;" + CStr(ColorNum) + "m ")
                    Else
                        Console.BackgroundColor = colors(RandomDriver.Next(colors.Length - 1))
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color ({0})", Console.BackgroundColor)
                        Console.Write(" ")
                    End If

                    'Simulate a trail effect
                    If CoveredPositions.Count = LighterMaxPositions Then
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Covered positions exceeded max positions of {0}", LighterMaxPositions)
                        Dim WipeLeft As Integer = CoveredPositions(0).ToString.Substring(0, CoveredPositions(0).ToString.IndexOf(";"))
                        Dim WipeTop As Integer = CoveredPositions(0).ToString.Substring(CoveredPositions(0).ToString.IndexOf(";") + 1)
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Wiping in {0}, {1}...", WipeLeft, WipeTop)
                        Console.SetCursorPosition(WipeLeft, WipeTop)
                        Console.BackgroundColor = ConsoleColor.Black
                        Console.Write(" ")
                        CoveredPositions.RemoveAt(0)
                    End If
                End If
            Loop
        Catch ex As Exception
            Wdbg(DebugLevel.W, "Screensaver experienced an error: {0}. Cleaning everything up...", ex.Message)
            WStkTrc(ex)
            e.Cancel = True
            SetInputColor()
            LoadBack()
            Console.CursorVisible = True
            Wdbg(DebugLevel.I, "All clean. Lighter screensaver stopped.")
            W(DoTranslation("Screensaver experienced an error while displaying: {0}. Press any key to exit."), True, ColTypes.Error, ex.Message)
            SaverAutoReset.Set()
        End Try
    End Sub

End Module
