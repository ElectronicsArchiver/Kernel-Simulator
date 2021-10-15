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

Public Module ConsoleExtensions

    ''' <summary>
    ''' Clears the console buffer, but keeps the current cursor position
    ''' </summary>
    Public Sub ClearKeepPosition()
        Dim Left As Integer = Console.CursorLeft
        Dim Top As Integer = Console.CursorTop
        Console.Clear()
        Console.SetCursorPosition(Left, Top)
    End Sub

    ''' <summary>
    ''' Clears the line to the right
    ''' </summary>
    Public Sub ClearLineToRight()
        Console.Write(GetEsc() + "[0K")
    End Sub

    ''' <summary>
    ''' Gets how many times to repeat the character to represent the appropriate percentage level for the specified number.
    ''' </summary>
    ''' <param name="CurrentNumber">The current number that is less than or equal to the maximum number.</param>
    ''' <param name="MaximumNumber">The maximum number.</param>
    ''' <param name="WidthOffset">The console window width offset. It's usually a multiple of 2.</param>
    ''' <returns>How many times to repeat the character</returns>
    Public Function PercentRepeat(CurrentNumber As Integer, MaximumNumber As Integer, WidthOffset As Integer) As Integer
        Return CurrentNumber * 100 / MaximumNumber * ((Console.WindowWidth - WidthOffset) * 0.01)
    End Function

    ''' <summary>
    ''' Gets how many times to repeat the character to represent the appropriate percentage level for the specified number.
    ''' </summary>
    ''' <param name="CurrentNumber">The current number that is less than or equal to the maximum number.</param>
    ''' <param name="MaximumNumber">The maximum number.</param>
    ''' <param name="TargetWidth">The target width</param>
    ''' <returns>How many times to repeat the character</returns>
    Public Function PercentRepeatTargeted(CurrentNumber As Integer, MaximumNumber As Integer, TargetWidth As Integer) As Integer
        Return CurrentNumber * 100 / MaximumNumber * (TargetWidth * 0.01)
    End Function

End Module
