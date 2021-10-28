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
Imports Extensification.ListExts

Module SnakerDisplay

    Public WithEvents Snaker As New BackgroundWorker With {.WorkerSupportsCancellation = True}

    ''' <summary>
    ''' Handles the code of Snaker
    ''' </summary>
    Sub Snaker_DoWork(sender As Object, e As DoWorkEventArgs) Handles Snaker.DoWork
        'Variables
        Dim RandomDriver As New Random()
        Dim CurrentWindowWidth As Integer = Console.WindowWidth
        Dim CurrentWindowHeight As Integer = Console.WindowHeight
        Dim ResizeSyncing As Boolean
        Dim SnakeLength As Integer = 1
        Dim SnakeMassPositions As New List(Of String)

        'Preparations
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.Clear()
        Wdbg(DebugLevel.I, "Console geometry: {0}x{1}", Console.WindowWidth, Console.WindowHeight)

        'Screensaver logic
        Do While True
            Console.CursorVisible = False
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Console.Clear()
            If Snaker.CancellationPending = True Then
                HandleSaverCancel()
                Exit Do
            Else
                'Sanity checks for color levels
                If SnakerTrueColor Or Snaker255Colors Then
                    SnakerMinimumRedColorLevel = If(SnakerMinimumRedColorLevel >= 0 And SnakerMinimumRedColorLevel <= 255, SnakerMinimumRedColorLevel, 0)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Minimum red color level: {0}", SnakerMinimumRedColorLevel)
                    SnakerMinimumGreenColorLevel = If(SnakerMinimumGreenColorLevel >= 0 And SnakerMinimumGreenColorLevel <= 255, SnakerMinimumGreenColorLevel, 0)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Minimum green color level: {0}", SnakerMinimumGreenColorLevel)
                    SnakerMinimumBlueColorLevel = If(SnakerMinimumBlueColorLevel >= 0 And SnakerMinimumBlueColorLevel <= 255, SnakerMinimumBlueColorLevel, 0)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Minimum blue color level: {0}", SnakerMinimumBlueColorLevel)
                    SnakerMinimumColorLevel = If(SnakerMinimumColorLevel >= 0 And SnakerMinimumColorLevel <= 255, SnakerMinimumColorLevel, 0)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Minimum color level: {0}", SnakerMinimumColorLevel)
                    SnakerMaximumRedColorLevel = If(SnakerMaximumRedColorLevel >= 0 And SnakerMaximumRedColorLevel <= 255, SnakerMaximumRedColorLevel, 255)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Maximum red color level: {0}", SnakerMaximumRedColorLevel)
                    SnakerMaximumGreenColorLevel = If(SnakerMaximumGreenColorLevel >= 0 And SnakerMaximumGreenColorLevel <= 255, SnakerMaximumGreenColorLevel, 255)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Maximum green color level: {0}", SnakerMaximumGreenColorLevel)
                    SnakerMaximumBlueColorLevel = If(SnakerMaximumBlueColorLevel >= 0 And SnakerMaximumBlueColorLevel <= 255, SnakerMaximumBlueColorLevel, 255)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Maximum blue color level: {0}", SnakerMaximumBlueColorLevel)
                    SnakerMaximumColorLevel = If(SnakerMaximumColorLevel >= 0 And SnakerMaximumColorLevel <= 255, SnakerMaximumColorLevel, 255)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Maximum color level: {0}", SnakerMaximumColorLevel)
                Else
                    SnakerMinimumColorLevel = If(SnakerMinimumColorLevel >= 0 And SnakerMinimumColorLevel <= 15, SnakerMinimumColorLevel, 0)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Minimum color level: {0}", SnakerMinimumColorLevel)
                    SnakerMaximumColorLevel = If(SnakerMaximumColorLevel >= 0 And SnakerMaximumColorLevel <= 15, SnakerMaximumColorLevel, 15)
                    WdbgConditional(ScreensaverDebug, DebugLevel.I, "Maximum color level: {0}", SnakerMaximumColorLevel)
                End If

                'Get the floor color ready
                Dim FloorColor As Color = ChangeSnakeColor()

                'Draw the floor
                If Not ResizeSyncing Then
                    Dim FloorTopLeftEdge As Integer = 2
                    Dim FloorBottomLeftEdge As Integer = 2
                    Dim FloorTopRightEdge As Integer = Console.WindowWidth - 3
                    Dim FloorBottomRightEdge As Integer = Console.WindowWidth - 3
                    Dim FloorTopEdge As Integer = 2
                    Dim FloorBottomEdge As Integer = Console.WindowHeight - 2
                    Dim FloorLeftEdge As Integer = 2
                    Dim FloorRightEdge As Integer = Console.WindowWidth - 4
                    Console.Write(FloorColor.VTSequenceBackground)

                    'First, draw the floor top edge
                    For x As Integer = FloorTopLeftEdge To FloorTopRightEdge
                        Console.SetCursorPosition(x, 1)
                        Console.Write(" ")
                    Next

                    'Second, draw the floor bottom edge
                    For x As Integer = FloorBottomLeftEdge To FloorBottomRightEdge
                        Console.SetCursorPosition(x, FloorBottomEdge)
                        Console.Write(" ")
                    Next

                    'Third, draw the floor left edge
                    For y As Integer = FloorTopEdge To FloorBottomEdge
                        Console.SetCursorPosition(FloorLeftEdge, y)
                        Console.Write("  ")
                    Next

                    'Finally, draw the floor right edge
                    For y As Integer = FloorTopEdge To FloorBottomEdge
                        Console.SetCursorPosition(FloorRightEdge, y)
                        Console.Write("  ")
                    Next
                End If

                'Get the snake color ready
                Dim SnakeColor As Color = ChangeSnakeColor()

                'A typical snake usually starts in the middle.
                If Not ResizeSyncing Then
                    Dim Dead As Boolean
                    Dim FloorTopEdge As Integer = 2
                    Dim FloorBottomEdge As Integer = Console.WindowHeight - 2
                    Dim FloorLeftEdge As Integer = 4
                    Dim FloorRightEdge As Integer = Console.WindowWidth - 4
                    Dim SnakeCurrentX As Integer = Console.WindowWidth / 2
                    Dim SnakeCurrentY As Integer = Console.WindowHeight / 2
                    Dim SnakeAppleX As Integer = RandomDriver.Next(FloorLeftEdge + 1, FloorRightEdge - 1)
                    Dim SnakeAppleY As Integer = RandomDriver.Next(FloorTopEdge + 1, FloorBottomEdge - 1)

                    Do Until Dead
                        'Clear the stage
                        Console.BackgroundColor = ConsoleColor.Black
                        For x As Integer = 5 To FloorRightEdge - 1
                            For y As Integer = 3 To FloorBottomEdge - 1
                                Console.SetCursorPosition(x, y)
                                Console.Write(" ")
                            Next
                        Next
                        Console.Write(SnakeColor.VTSequenceBackground)

                        'Always draw an apple
                        Console.SetCursorPosition(SnakeAppleX, SnakeAppleY)
                        Console.Write("+")

                        'Make a snake
                        For PositionIndex As Integer = SnakeMassPositions.Count - 1 To 0 Step -1
                            Dim PositionStrings() As String = SnakeMassPositions(PositionIndex).Split("/")
                            Dim PositionX As Integer = PositionStrings(0)
                            Dim PositionY As Integer = PositionStrings(1)
                            Console.SetCursorPosition(PositionX, PositionY)
                            Console.Write(" ")
                            Console.SetCursorPosition(PositionX, PositionY)
                        Next

                        'Change the snake direction
                        Dim Direction As SnakeDirection = SnakeDirection.Bottom
                        WdbgConditional(ScreensaverDebug, DebugLevel.I, "Snake is facing {0}.", Direction.ToString)
                        Select Case Direction
                            Case SnakeDirection.Bottom
                                SnakeCurrentY += 1
                            Case SnakeDirection.Top
                                SnakeCurrentY -= 1
                            Case SnakeDirection.Left
                                SnakeCurrentX -= 1
                            Case SnakeDirection.Right
                                SnakeCurrentX += 1
                        End Select
                        Dead = SnakeMassPositions.Contains($"{SnakeCurrentX}/{SnakeCurrentY}")
                        SnakeMassPositions.AddIfNotFound($"{SnakeCurrentX}/{SnakeCurrentY}")
                        If SnakeMassPositions.Count > SnakeLength Then SnakeMassPositions.RemoveAt(0)

                        'Check death state
                        If Not Dead Then Dead = SnakeCurrentY = FloorTopEdge
                        If Not Dead Then Dead = SnakeCurrentY = FloorBottomEdge
                        If Not Dead Then Dead = SnakeCurrentX = FloorLeftEdge
                        If Not Dead Then Dead = SnakeCurrentX = FloorRightEdge

                        'If dead, show dead face
                        If Dead Then
                            Console.Write("X")
                        End If

                        'If the snake ate the apple, grow it up
                        If SnakeCurrentX = SnakeAppleX And SnakeCurrentY = SnakeAppleY Then
                            SnakeLength += 1
                            SnakeAppleX = RandomDriver.Next(FloorLeftEdge + 1, FloorRightEdge - 1)
                            SnakeAppleY = RandomDriver.Next(FloorTopEdge + 1, FloorBottomEdge - 1)
                        End If
                    Loop
                End If

                'Show the stage for few seconds before wiping
                SleepNoBlock(SnakerStageDelay, Snaker)

                'Reset resize sync
                ResizeSyncing = False
                CurrentWindowWidth = Console.WindowWidth
                CurrentWindowHeight = Console.WindowHeight
            End If
            SleepNoBlock(SnakerDelay, Snaker)
        Loop
    End Sub

    ''' <summary>
    ''' Checks for any screensaver error
    ''' </summary>
    Sub CheckForError(sender As Object, e As RunWorkerCompletedEventArgs) Handles Snaker.RunWorkerCompleted
        HandleSaverError(e.Error)
    End Sub

    ''' <summary>
    ''' Changes the snake color
    ''' </summary>
    Function ChangeSnakeColor() As Color
        Dim RandomDriver As New Random()
        Dim esc As Char = GetEsc()
        If SnakerTrueColor Then
            Dim RedColorNum As Integer = RandomDriver.Next(SnakerMinimumRedColorLevel, SnakerMaximumRedColorLevel)
            Dim GreenColorNum As Integer = RandomDriver.Next(SnakerMinimumGreenColorLevel, SnakerMaximumGreenColorLevel)
            Dim BlueColorNum As Integer = RandomDriver.Next(SnakerMinimumBlueColorLevel, SnakerMaximumBlueColorLevel)
            WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color (R;G;B: {0};{1};{2})", RedColorNum, GreenColorNum, BlueColorNum)
            Return New Color($"{RedColorNum};{GreenColorNum};{BlueColorNum}")
        ElseIf Snaker255Colors Then
            Dim ColorNum As Integer = RandomDriver.Next(SnakerMinimumColorLevel, SnakerMaximumColorLevel)
            WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color ({0})", ColorNum)
            Return New Color(ColorNum)
        Else
            Console.BackgroundColor = colors(RandomDriver.Next(SnakerMinimumColorLevel, SnakerMaximumColorLevel))
            WdbgConditional(ScreensaverDebug, DebugLevel.I, "Got color ({0})", Console.BackgroundColor)
        End If
    End Function

    ''' <summary>
    ''' Where would the snake go?
    ''' </summary>
    Enum SnakeDirection
        Top
        Bottom
        Left
        Right
    End Enum

End Module
