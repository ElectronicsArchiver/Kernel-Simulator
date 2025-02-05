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

Namespace Shell.Commands
    Class SftpCommand
        Inherits CommandExecutor
        Implements ICommand

        Public Overrides Sub Execute(StringArgs As String, ListArgs() As String, ListArgsOnly As String(), ListSwitchesOnly As String()) Implements ICommand.Execute
            Try
                If ListArgs?.Length = 0 Or ListArgs Is Nothing Then
                    StartShell(ShellType.SFTPShell)
                Else
                    StartShell(ShellType.SFTPShell, ListArgs(0))
                End If
            Catch sftpex As Exceptions.SFTPShellException
                Write(sftpex.Message, True, ColTypes.Error)
            Catch ex As Exception
                WStkTrc(ex)
                Write(DoTranslation("Unknown SFTP shell error:") + " {0}", True, ColTypes.Error, ex.Message)
            End Try
        End Sub

    End Class
End Namespace