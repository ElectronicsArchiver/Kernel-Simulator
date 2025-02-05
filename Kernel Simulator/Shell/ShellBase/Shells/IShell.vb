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

Namespace Shell.ShellBase.Shells
    ''' <summary>
    ''' Base shell functions
    ''' </summary>
    Public Interface IShell

        ''' <summary>
        ''' The shell type
        ''' </summary>
        ReadOnly Property ShellType As ShellType
        ''' <summary>
        ''' If true, will exit the shell
        ''' </summary>
        Property Bail As Boolean
        ''' <summary>
        ''' The shell initializer
        ''' </summary>
        Sub InitializeShell(ParamArray ShellArgs() As Object)

    End Interface
End Namespace
