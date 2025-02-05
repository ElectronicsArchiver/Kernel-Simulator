
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

Imports KS.Network.SFTP.Commands

Namespace Network.SFTP
    Public Module SFTPShellCommon

        Public ReadOnly SFTPCommands As New Dictionary(Of String, CommandInfo) From {
            {"connect", New CommandInfo("connect", ShellType.SFTPShell, "Connects to an SFTP server (it must start with ""sftp://"")", {"<server>"}, True, 1, New SFTP_ConnectCommand)},
            {"cdl", New CommandInfo("cdl", ShellType.SFTPShell, "Changes local directory to download to or upload from", {"<directory>"}, True, 1, New SFTP_CdlCommand)},
            {"cdr", New CommandInfo("cdr", ShellType.SFTPShell, "Changes remote directory to download from or upload to", {"<directory>"}, True, 1, New SFTP_CdrCommand)},
            {"del", New CommandInfo("del", ShellType.SFTPShell, "Deletes remote file from server", {"<file>"}, True, 1, New SFTP_DelCommand)},
            {"disconnect", New CommandInfo("disconnect", ShellType.SFTPShell, "Disconnects from server", Array.Empty(Of String), False, 0, New SFTP_DisconnectCommand)},
            {"exit", New CommandInfo("exit", ShellType.SFTPShell, "Exits SFTP shell and returns to kernel", Array.Empty(Of String), False, 0, New SFTP_ExitCommand)},
            {"get", New CommandInfo("get", ShellType.SFTPShell, "Downloads remote file to local directory using binary or text", {"<file>"}, True, 1, New SFTP_GetCommand)},
            {"help", New CommandInfo("help", ShellType.SFTPShell, "Shows help screen", Array.Empty(Of String), False, 0, New SFTP_HelpCommand)},
            {"lsl", New CommandInfo("lsl", ShellType.SFTPShell, "Lists local directory", {"[-showdetails|-suppressmessages] [dir]"}, False, 0, New SFTP_LslCommand, False, False, False, False, False)},
            {"lsr", New CommandInfo("lsr", ShellType.SFTPShell, "Lists remote directory", {"[-showdetails] [dir]"}, False, 0, New SFTP_LsrCommand, False, False, False, False, False)},
            {"put", New CommandInfo("put", ShellType.SFTPShell, "Uploads local file to remote directory using binary or text", {"<file>"}, True, 1, New SFTP_PutCommand)},
            {"pwdl", New CommandInfo("pwdl", ShellType.SFTPShell, "Gets current local directory", Array.Empty(Of String), False, 0, New SFTP_PwdlCommand)},
            {"pwdr", New CommandInfo("pwdr", ShellType.SFTPShell, "Gets current remote directory", Array.Empty(Of String), False, 0, New SFTP_PwdrCommand)},
            {"quickconnect", New CommandInfo("quickconnect", ShellType.SFTPShell, "Uses information from Speed Dial to connect to any network quickly", Array.Empty(Of String), False, 0, New SFTP_QuickConnectCommand)}
        }
        Public SFTPCurrDirect As String
        Public SFTPCurrentRemoteDir As String
        Public SFTPShellPromptStyle As String = ""
        Public SFTPShowDetailsInList As Boolean = True
        Public SFTPUserPromptStyle As String = ""
        Public SFTPNewConnectionsToSpeedDial As Boolean = True
        Friend SFTPConnected As Boolean
        Friend _clientSFTP As SftpClient
        Friend SFTPSite As String
        Friend SFTPPass As String
        Friend SFTPUser As String
        Friend ReadOnly SFTPModCommands As New Dictionary(Of String, CommandInfo)

        ''' <summary>
        ''' The SFTP client used to connect to the SFTP server
        ''' </summary>
        Public ReadOnly Property ClientSFTP As SftpClient
            Get
                Return _clientSFTP
            End Get
        End Property

    End Module
End Namespace
