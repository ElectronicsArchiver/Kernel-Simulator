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

Imports KS.Misc.Notifications

Namespace TestShell.Commands
    Class Test_SendNotProgCommand
        Inherits CommandExecutor
        Implements ICommand

        Public Overrides Sub Execute(StringArgs As String, ListArgs() As String, ListArgsOnly As String(), ListSwitchesOnly As String()) Implements ICommand.Execute
            Dim Notif As New Notification(ListArgs(1), ListArgs(2), ListArgs(0), NotifType.Progress)
            NotifySend(Notif)
            Do While Not Notif.ProgressCompleted
                Threading.Thread.Sleep(100)
                If ListArgs(3) >= 0 And Notif.Progress >= ListArgs(3) Then
                    Notif.ProgressFailed = True
                End If
                Notif.Progress += 1
            Loop
        End Sub

    End Class
End Namespace