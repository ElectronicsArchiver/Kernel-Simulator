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

Imports System.Globalization

Namespace TestShell.Commands
    Class Test_LsCulturesCommand
        Inherits CommandExecutor
        Implements ICommand

        Public Overrides Sub Execute(StringArgs As String, ListArgs() As String, ListArgsOnly As String(), ListSwitchesOnly As String()) Implements ICommand.Execute
            Dim Cults As CultureInfo() = CultureInfo.GetCultures(CultureTypes.AllCultures)
            For Each Cult As CultureInfo In Cults
                If ListArgs?.Length > 0 Or ListArgs IsNot Nothing Then
                    If Cult.Name.ToLower.Contains(ListArgs(0).ToLower) Or Cult.EnglishName.ToLower.Contains(ListArgs(0).ToLower) Then
                        Write("{0}: {1}", True, ColTypes.Neutral, Cult.Name, Cult.EnglishName)
                    End If
                Else
                    Write("{0}: {1}", True, ColTypes.Neutral, Cult.Name, Cult.EnglishName)
                End If
            Next
        End Sub

    End Class
End Namespace