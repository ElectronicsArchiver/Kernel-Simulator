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

Imports KS.ConsoleBase.Colors

Namespace Misc.Writers.FancyWriters.Tools
    Public Class CellOptions

        ''' <summary>
        ''' The column, or row value, number
        ''' </summary>
        Public ReadOnly Property ColumnNumber As Integer
        ''' <summary>
        ''' The row number
        ''' </summary>
        Public ReadOnly Property RowNumber As Integer
        ''' <summary>
        ''' The column, or row value, index
        ''' </summary>
        Public ReadOnly Property ColumnIndex As Integer
        ''' <summary>
        ''' The row index
        ''' </summary>
        Public ReadOnly Property RowIndex As Integer
        ''' <summary>
        ''' Whether to color the cell
        ''' </summary>
        Public Property ColoredCell As Boolean
        ''' <summary>
        ''' The custom cell color
        ''' </summary>
        Public Property CellColor As Color = NeutralTextColor
        ''' <summary>
        ''' The custom background cell color
        ''' </summary>
        Public Property CellBackgroundColor As Color = BackgroundColor

        ''' <summary>
        ''' Makes a new instance of the cell options class
        ''' </summary>
        ''' <param name="ColumnNumber">The column number</param>
        ''' <param name="RowNumber">The row number</param>
        Public Sub New(ColumnNumber As Integer, RowNumber As Integer)
            Me.ColumnNumber = ColumnNumber
            Me.RowNumber = RowNumber
            ColumnIndex = ColumnNumber - 1
            RowIndex = RowNumber - 1
        End Sub

    End Class
End Namespace
