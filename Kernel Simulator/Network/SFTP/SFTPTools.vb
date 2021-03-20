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

Imports System.IO
Imports Newtonsoft.Json.Linq

Public Module SFTPTools

    ''' <summary>
    ''' Prompts user for a password
    ''' </summary>
    ''' <param name="user">A user name</param>
    ''' <param name="Address">A host address</param>
    ''' <param name="Port">A port for the address</param>
    Public Sub SFTPPromptForPassword(ByVal user As String, ByVal Address As String, ByVal Port As Integer)
        'Prompt for password
        W(DoTranslation("Password for {0}: ", currentLang), False, ColTypes.Input, user)

        'Get input
        SFTPPass = ReadLineNoInput("*")
        Console.WriteLine()
        ClientSFTP = New SftpClient(Address, Port, user, SFTPPass)

        'Connect to FTP
        ConnectSFTP()
    End Sub

    ''' <summary>
    ''' Tries to connect to the FTP server
    ''' </summary>
    ''' <param name="address">An FTP server. You may specify it like "[address]" or "[address]:[port]"</param>
    Public Sub SFTPTryToConnect(ByVal address As String)
        If connected = True Then
            W(DoTranslation("You should disconnect from server before connecting to another server", currentLang), True, ColTypes.Err)
        Else
            Try
                'Create an SFTP stream to connect to
                Dim SftpHost As String = address.Replace("sftp://", "").Replace(address.Substring(address.LastIndexOf(":")), "")
                Dim SftpPort As String = address.Replace("sftp://", "").Replace(SftpHost + ":", "")

                'Check to see if no port is provided by client
                If SftpHost = SftpPort Then
                    SftpPort = 22
                End If

                'Prompt for username
                W(DoTranslation("Username for {0}: ", currentLang), False, ColTypes.Input, address)
                user = Console.ReadLine()
                If user = "" Then
                    Wdbg("W", "User is not provided. Fallback to ""anonymous""")
                    user = "anonymous"
                End If

                SFTPPromptForPassword(user, SftpHost, SftpPort)
            Catch ex As Exception
                Wdbg("W", "Error connecting to {0}: {1}", address, ex.Message)
                WStkTrc(ex)
                If DebugMode = True Then
                    W(DoTranslation("Error when trying to connect to {0}: {1}", currentLang) + vbNewLine +
                      DoTranslation("Stack Trace: {2}", currentLang), True, ColTypes.Err, address, ex.Message, ex.StackTrace)
                Else
                    W(DoTranslation("Error when trying to connect to {0}: {1}", currentLang), True, ColTypes.Err, address, ex.Message)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Tries to connect to the SFTP server.
    ''' </summary>
    Private Sub ConnectSFTP()
        'Connect
        W(DoTranslation("Trying to connect to {0}...", currentLang), True, ColTypes.Neutral, ClientSFTP.ConnectionInfo.Host)
        Wdbg("I", "Connecting to {0} with {1}...", ClientSFTP.ConnectionInfo.Host)
        ClientSFTP.Connect()

        'Show that it's connected
        W(DoTranslation("Connected to {0}", currentLang), True, ColTypes.Neutral, ClientSFTP.ConnectionInfo.Host)
        Wdbg("I", "Connected.")
        SFTPConnected = True

        'Prepare to print current SFTP directory
        SFTPCurrentRemoteDir = ClientSFTP.WorkingDirectory
        Wdbg("I", "Working directory: {0}", SFTPCurrentRemoteDir)
        sftpsite = ClientSFTP.ConnectionInfo.Host
        SFTPUser = ClientSFTP.ConnectionInfo.Username

        'Write connection information to Speed Dial file if it doesn't exist there
        Dim SpeedDialEntries As List(Of JToken) = ListSpeedDialEntries(SpeedDialType.SFTP)
        Dim SpeedDialEntry As String = sftpsite + "," + CStr(ClientSFTP.ConnectionInfo.Port) + "," + SFTPUser
        Wdbg("I", "Speed dial length: {0}", SpeedDialEntries.Count)
        If SpeedDialEntries.Contains(SpeedDialEntry) Then
            Wdbg("I", "Site already there.")
            Exit Sub
        Else
            'Speed dial format is below:
            'Site,Port,Username
            AddEntryToSpeedDial(SpeedDialEntry, SpeedDialType.SFTP)
        End If
    End Sub

    ''' <summary>
    ''' Opens speed dial prompt
    ''' </summary>
    Sub SFTPQuickConnect()
        If File.Exists(paths("SFTPSpeedDial")) Then
            Dim SpeedDialLines As List(Of JToken) = ListSpeedDialEntries(SpeedDialType.SFTP)
            Wdbg("I", "Speed dial length: {0}", SpeedDialLines.Count)
            Dim Counter As Integer = 1
            Dim Answer As String
            Dim Answering As Boolean = True
            If Not SpeedDialLines.Count = 0 Then
                For Each SpeedDialLine As String In SpeedDialLines
                    Wdbg("I", "Speed dial line: {0}", SpeedDialLine)
                    W(DoTranslation("Select an address to connect to:", currentLang), True, ColTypes.Neutral)
                    W("{0}: {1}", True, ColTypes.Neutral, Counter, SpeedDialLine)
                    Counter += 1
                Next
                While Answering
                    W(">> ", False, ColTypes.Input)
                    Answer = Console.ReadLine
                    Wdbg("I", "Response: {0}", Answer)
                    Console.WriteLine()
                    If IsNumeric(Answer) Then
                        Wdbg("I", "Response is numeric. IsNumeric(Answer) returned true. Checking to see if in-bounds...")
                        Dim AnswerInt As Integer = Answer
                        If AnswerInt <= SpeedDialLines.Count Then
                            Answering = False
                            Wdbg("I", "Response is in-bounds. Connecting...")
                            Dim ChosenSpeedDialLine As String = SpeedDialLines(AnswerInt - 1)
                            Wdbg("I", "Chosen connection: {0}", ChosenSpeedDialLine)
                            Dim ChosenLineSeparation As String() = ChosenSpeedDialLine.Split(",")
                            Dim Address As String = ChosenLineSeparation(0)
                            Dim Port As String = ChosenLineSeparation(1)
                            Dim Username As String = ChosenLineSeparation(2)
                            Wdbg("I", "Address: {0}, Port: {1}, Username: {2}", Address, Port, Username)
                            SFTPPromptForPassword(Username, Address, Port)
                        Else
                            Wdbg("I", "Response is out-of-bounds. Retrying...")
                            W(DoTranslation("The selection is out of range. Select between 1-{0}. Try again.", currentLang), True, ColTypes.Err, SpeedDialLines.Count)
                        End If
                    Else
                        Wdbg("W", "Response isn't numeric. IsNumeric(Answer) returned false.")
                        W(DoTranslation("The selection is not a number. Try again.", currentLang), True, ColTypes.Err)
                    End If
                End While
            Else
                Wdbg("E", "Speed dial is empty. Lines count is 0.")
                W(DoTranslation("Speed dial is empty. Connect to a server to add an address to it.", currentLang), True, ColTypes.Err)
            End If
        Else
            Wdbg("E", "File doesn't exist.")
            W(DoTranslation("Speed dial doesn't exist. Connect to a server to add an address to it.", currentLang), True, ColTypes.Err)
        End If
    End Sub

End Module