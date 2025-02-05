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

Imports System.IO
Imports KS.Network.RemoteDebug.Interface

Namespace Network.RemoteDebug.Commands
    Class Debug_UsernameCommand
        Inherits RemoteDebugCommandExecutor
        Implements IRemoteDebugCommand

        Public Overrides Sub Execute(StringArgs As String, ListArgs() As String, SocketStreamWriter As StreamWriter, DeviceAddress As String) Implements IRemoteDebugCommand.Execute
            SocketStreamWriter.WriteLine(CurrentUser.Username)
        End Sub

    End Class
End Namespace