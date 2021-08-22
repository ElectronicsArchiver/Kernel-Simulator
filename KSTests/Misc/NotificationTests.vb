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

Imports KS

<TestClass()> Public Class NotificationTests

    ''' <summary>
    ''' Tests notification creation
    ''' </summary>
    <TestMethod()> Public Sub TestNotifyCreate()
        NotifyCreate("This is the title.", "This is the description.", NotifPriority.Medium, NotifType.Normal).ShouldNotBeNull
    End Sub

    ''' <summary>
    ''' Tests notification sending
    ''' </summary>
    <TestMethod> Public Sub TestNotifySend()
        Dim Notif As Notification = NotifyCreate("Notification title", "This is a high priority notification", NotifPriority.High, NotifType.Normal)
        NotifySend(Notif)
        NotifRecents.ShouldNotBeEmpty
    End Sub

    ''' <summary>
    ''' Tests notifications sending
    ''' </summary>
    <TestMethod> Public Sub TestNotifySendRange()
        Dim Notif1 As Notification = NotifyCreate("High notification title", "This is a high priority notification", NotifPriority.High, NotifType.Normal)
        Dim Notif2 As Notification = NotifyCreate("Medium notification title", "This is a medium priority notification", NotifPriority.Medium, NotifType.Normal)
        Dim Notif3 As Notification = NotifyCreate("Low notification title", "This is a low priority notification", NotifPriority.Low, NotifType.Normal)
        Dim Notifs As New List(Of Notification) From {Notif1, Notif2, Notif3}
        NotifySendRange(Notifs)
        NotifRecents.ShouldNotBeEmpty
    End Sub

    ''' <summary>
    ''' Tests notification dismiss
    ''' </summary>
    <TestMethod> Public Sub TestNotifyDismiss()
        Dim Notif As Notification = NotifyCreate("Redundant title", "This is a redundant notification", NotifPriority.Low, NotifType.Normal)
        NotifySend(Notif)
        NotifDismiss(NotifRecents.Count - 1).ShouldBeTrue
    End Sub

End Class