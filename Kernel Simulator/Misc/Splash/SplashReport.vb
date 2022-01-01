﻿
'    Kernel Simulator  Copyright (C) 2018-2022  EoflaOE
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

Public Module SplashReport

    Friend _Progress As Integer = 0
    Friend _ProgressText As String = ""

    ''' <summary>
    ''' The progress indicator of the kernel 
    ''' </summary>
    Public ReadOnly Property Progress As Integer
        Get
            Return _Progress
        End Get
    End Property

    ''' <summary>
    ''' The progress text to indicate how did the kernel progress
    ''' </summary>
    Public ReadOnly Property ProgressText As String
        Get
            Return _ProgressText
        End Get
    End Property

    ''' <summary>
    ''' Reports the progress for the splash screen while the kernel is booting
    ''' </summary>
    ''' <param name="Text">The progress text to indicate how did the kernel progress</param>
    ''' <param name="Progress">The progress indicator of the kernel</param>
    Friend Sub ReportProgress(Text As String, Progress As Integer, Optional ColTypes As ColTypes = ColTypes.Neutral)
        If CurrentSplashInfo.DisplaysProgress Then
            If EnableSplash Then
                CurrentSplash.Report(_Progress, Text, CurrentSplash.ProgressWritePositionX, CurrentSplash.ProgressWritePositionY, CurrentSplash.ProgressReportWritePositionX, CurrentSplash.ProgressReportWritePositionY)
            ElseIf Not QuietKernel Then
                Write(Text, True, ColTypes)
            End If
        End If
        _Progress += Progress
        _ProgressText = Text
        If _Progress > 100 Then _Progress = 100
    End Sub

End Module
