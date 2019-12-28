﻿
'    Kernel Simulator  Copyright (C) 2018-2019  EoflaOE
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

Imports System.Net.Sockets
Imports System.Threading

Module RemoteProcedure

    Public RPCListen As UdpClient
    Public RPCPort As Integer = 12345
    Public RPCThread As New Thread(AddressOf ListenRPC) With {.IsBackground = True}
    Public RPCStopping As Boolean

    Sub StartRPC()
        Try
            RPCThread.Start()
        Catch ex As ThreadStateException
            W(DoTranslation("RPC is already running.", currentLang), True, ColTypes.Neutral)
        End Try
    End Sub
    Sub ListenRPC()
        RPCListen = New UdpClient(RPCPort)
        RPCListen.EnableBroadcast = True
        Wdbg("RPC: Listener started")
        Dim RPCListener As New Thread(AddressOf RecCommand) With {.IsBackground = True}
        RPCListener.Start()
        Wdbg("RPC: Thread started")
        W(DoTranslation("RPC listening on all addresses using port {0}.", currentLang), True, ColTypes.Neutral, RPCPort)
    End Sub

End Module
