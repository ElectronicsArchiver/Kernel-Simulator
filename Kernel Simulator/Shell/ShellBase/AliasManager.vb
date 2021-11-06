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

Public Module AliasManager

    Public Aliases As New Dictionary(Of String, String)
    Public RemoteDebugAliases As New Dictionary(Of String, String)
    Public FTPShellAliases As New Dictionary(Of String, String)
    Public MailShellAliases As New Dictionary(Of String, String)
    Public SFTPShellAliases As New Dictionary(Of String, String)
    Public TextShellAliases As New Dictionary(Of String, String)
    Public TestShellAliases As New Dictionary(Of String, String)
    Public ZIPShellAliases As New Dictionary(Of String, String)
    Public RSSShellAliases As New Dictionary(Of String, String)
    Public JsonShellAliases As New Dictionary(Of String, String)
    Public HTTPShellAliases As New Dictionary(Of String, String)
    Friend AliasesToBeRemoved As New Dictionary(Of String, ShellCommandType)

    'TODO: Remove AliasType in RC1
    ''' <summary>
    ''' Aliases type
    ''' </summary>
    <Obsolete("There is already an enumeration called ShellCommandType. Use that.")> Public Enum AliasType
        ''' <summary>
        ''' Normal shell
        ''' </summary>
        Shell = 1
        ''' <summary>
        ''' Remote debugging shell
        ''' </summary>
        RDebug
        ''' <summary>
        ''' FTP shell
        ''' </summary>
        FTPShell
        ''' <summary>
        ''' SFTP shell
        ''' </summary>
        SFTPShell
        ''' <summary>
        ''' Mail shell
        ''' </summary>
        MailShell
        ''' <summary>
        ''' Text shell
        ''' </summary>
        TextShell
        ''' <summary>
        ''' Test shell
        ''' </summary>
        TestShell
        ''' <summary>
        ''' ZIP shell
        ''' </summary>
        ZIPShell
        ''' <summary>
        ''' RSS shell
        ''' </summary>
        RSSShell
    End Enum

    ''' <summary>
    ''' Initializes aliases
    ''' </summary>
    Public Sub InitAliases()
        'Get all aliases from file
        If Not FileExists(GetKernelPath(KernelPathType.Aliases)) Then MakeFile(GetKernelPath(KernelPathType.Aliases))
        Dim AliasJsonContent As String = File.ReadAllText(GetKernelPath(KernelPathType.Aliases))
        Dim AliasNameToken As JToken = JToken.Parse(If(Not String.IsNullOrEmpty(AliasJsonContent), AliasJsonContent, "{}"))
        Dim AliasCmd, ActualCmd, AliasType As String

        For Each AliasObject As JObject In AliasNameToken
            AliasCmd = AliasObject("Alias")
            ActualCmd = AliasObject("Command")
            AliasType = AliasObject("Type")
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from Aliases.json to {2} list...", AliasCmd, ActualCmd, AliasType)
            Select Case AliasType
                Case "Shell"
                    If Not Aliases.ContainsKey(AliasCmd) Then
                        Aliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "Remote"
                    If Not RemoteDebugAliases.ContainsKey(AliasCmd) Then
                        RemoteDebugAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "FTPShell"
                    If Not FTPShellAliases.ContainsKey(AliasCmd) Then
                        FTPShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "SFTPShell"
                    If Not SFTPShellAliases.ContainsKey(AliasCmd) Then
                        SFTPShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "Mail"
                    If Not MailShellAliases.ContainsKey(AliasCmd) Then
                        MailShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "Text"
                    If Not TextShellAliases.ContainsKey(AliasCmd) Then
                        TextShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "Test"
                    If Not TestShellAliases.ContainsKey(AliasCmd) Then
                        TestShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "ZIP"
                    If Not ZIPShellAliases.ContainsKey(AliasCmd) Then
                        ZIPShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "RSS"
                    If Not RSSShellAliases.ContainsKey(AliasCmd) Then
                        RSSShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "JSON"
                    If Not JsonShellAliases.ContainsKey(AliasCmd) Then
                        JsonShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case "HTTP"
                    If Not HTTPShellAliases.ContainsKey(AliasCmd) Then
                        HTTPShellAliases.Add(AliasCmd, ActualCmd)
                    End If
                Case Else
                    Wdbg(DebugLevel.E, "Invalid type {0}", AliasType)
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Saves aliases
    ''' </summary>
    Public Sub SaveAliases()
        'Get all aliases from file
        If Not FileExists(GetKernelPath(KernelPathType.Aliases)) Then MakeFile(GetKernelPath(KernelPathType.Aliases))
        Dim AliasJsonContent As String = File.ReadAllText(GetKernelPath(KernelPathType.Aliases))
        Dim AliasNameToken As JArray = JArray.Parse(If(Not String.IsNullOrEmpty(AliasJsonContent), AliasJsonContent, "[]"))

        'Shell aliases
        For i As Integer = 0 To Aliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type Shell...", Aliases.Keys(i), Aliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", Aliases.Keys(i)},
                {"Command", Aliases.Values(i)},
                {"Type", "Shell"}
            }
            If Not DoesAliasExist(Aliases.Keys(i), ShellCommandType.Shell) Then AliasNameToken.Add(AliasObject)
        Next

        'Remote Debug aliases
        For i As Integer = 0 To RemoteDebugAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type Remote...", RemoteDebugAliases.Keys(i), RemoteDebugAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", RemoteDebugAliases.Keys(i)},
                {"Command", RemoteDebugAliases.Values(i)},
                {"Type", "Remote"}
            }
            If Not DoesAliasExist(RemoteDebugAliases.Keys(i), ShellCommandType.RemoteDebugShell) Then AliasNameToken.Add(AliasObject)
        Next

        'FTP shell aliases
        For i As Integer = 0 To FTPShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type FTPShell...", FTPShellAliases.Keys(i), FTPShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", FTPShellAliases.Keys(i)},
                {"Command", FTPShellAliases.Values(i)},
                {"Type", "FTPShell"}
            }
            If Not DoesAliasExist(FTPShellAliases.Keys(i), ShellCommandType.FTPShell) Then AliasNameToken.Add(AliasObject)
        Next

        'SFTP shell aliases
        For i As Integer = 0 To SFTPShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type SFTPShell...", SFTPShellAliases.Keys(i), SFTPShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", SFTPShellAliases.Keys(i)},
                {"Command", SFTPShellAliases.Values(i)},
                {"Type", "SFTPShell"}
            }
            If Not DoesAliasExist(SFTPShellAliases.Keys(i), ShellCommandType.SFTPShell) Then AliasNameToken.Add(AliasObject)
        Next

        'Mail shell aliases
        For i As Integer = 0 To MailShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type Mail...", MailShellAliases.Keys(i), MailShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", MailShellAliases.Keys(i)},
                {"Command", MailShellAliases.Values(i)},
                {"Type", "Mail"}
            }
            If Not DoesAliasExist(MailShellAliases.Keys(i), ShellCommandType.MailShell) Then AliasNameToken.Add(AliasObject)
        Next

        'Text shell aliases
        For i As Integer = 0 To TextShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type Text...", TextShellAliases.Keys(i), TextShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", TextShellAliases.Keys(i)},
                {"Command", TextShellAliases.Values(i)},
                {"Type", "Text"}
            }
            If Not DoesAliasExist(TextShellAliases.Keys(i), ShellCommandType.TextShell) Then AliasNameToken.Add(AliasObject)
        Next

        'Test shell aliases
        For i As Integer = 0 To TestShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type Test...", TestShellAliases.Keys(i), TestShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", TestShellAliases.Keys(i)},
                {"Command", TestShellAliases.Values(i)},
                {"Type", "Test"}
            }
            If Not DoesAliasExist(TestShellAliases.Keys(i), ShellCommandType.TestShell) Then AliasNameToken.Add(AliasObject)
        Next

        'ZIP shell aliases
        For i As Integer = 0 To ZIPShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type ZIP...", ZIPShellAliases.Keys(i), ZIPShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", ZIPShellAliases.Keys(i)},
                {"Command", ZIPShellAliases.Values(i)},
                {"Type", "ZIP"}
            }
            If Not DoesAliasExist(ZIPShellAliases.Keys(i), ShellCommandType.ZIPShell) Then AliasNameToken.Add(AliasObject)
        Next

        'RSS shell aliases
        For i As Integer = 0 To RSSShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type RSS...", RSSShellAliases.Keys(i), RSSShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", RSSShellAliases.Keys(i)},
                {"Command", RSSShellAliases.Values(i)},
                {"Type", "RSS"}
            }
            If Not DoesAliasExist(RSSShellAliases.Keys(i), ShellCommandType.RSSShell) Then AliasNameToken.Add(AliasObject)
        Next

        'JSON shell aliases
        For i As Integer = 0 To JsonShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type JSON...", JsonShellAliases.Keys(i), JsonShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", JsonShellAliases.Keys(i)},
                {"Command", JsonShellAliases.Values(i)},
                {"Type", "JSON"}
            }
            If Not DoesAliasExist(JsonShellAliases.Keys(i), ShellCommandType.JsonShell) Then AliasNameToken.Add(AliasObject)
        Next

        'HTTP shell aliases
        For i As Integer = 0 To HTTPShellAliases.Count - 1
            Wdbg(DebugLevel.I, "Adding ""{0}"" and ""{1}"" from list to Aliases.json with type HTTP...", HTTPShellAliases.Keys(i), HTTPShellAliases.Values(i))
            Dim AliasObject As New JObject From {
                {"Alias", HTTPShellAliases.Keys(i)},
                {"Command", HTTPShellAliases.Values(i)},
                {"Type", "HTTP"}
            }
            If Not DoesAliasExist(HTTPShellAliases.Keys(i), ShellCommandType.HTTPShell) Then AliasNameToken.Add(AliasObject)
        Next

        'Save changes
        File.WriteAllText(GetKernelPath(KernelPathType.Aliases), JsonConvert.SerializeObject(AliasNameToken, Formatting.Indented))
    End Sub

    ''' <summary>
    ''' Manages the alias
    ''' </summary>
    ''' <param name="mode">Either add or rem</param>
    ''' <param name="Type">Alias type (Shell or Remote Debug)</param>
    ''' <param name="AliasCmd">A specified alias</param>
    ''' <param name="DestCmd">A destination command (target)</param>
    Public Sub ManageAlias(mode As String, Type As ShellCommandType, AliasCmd As String, Optional DestCmd As String = "")
        If [Enum].IsDefined(GetType(ShellCommandType), Type) Then
            If mode = "add" Then
                'User tries to add an alias.
                Try
                    AddAlias(AliasCmd, DestCmd, Type)
                    Write(DoTranslation("You can now run ""{0}"" as a command: ""{1}""."), True, ColTypes.Neutral, AliasCmd, DestCmd)
                Catch ex As Exception
                    Wdbg(DebugLevel.E, "Failed to add alias. Stack trace written using WStkTrc(). {0}", ex.Message)
                    WStkTrc(ex)
                    Write(ex.Message, True, ColTypes.Error)
                End Try
            ElseIf mode = "rem" Then
                'User tries to remove an alias
                Try
                    RemoveAlias(AliasCmd, Type)
                    PurgeAliases()
                    Write(DoTranslation("Removed alias {0} successfully."), True, ColTypes.Neutral, AliasCmd)
                Catch ex As Exception
                    Wdbg(DebugLevel.E, "Failed to remove alias. Stack trace written using WStkTrc(). {0}", ex.Message)
                    WStkTrc(ex)
                    Write(ex.Message, True, ColTypes.Error)
                End Try
            Else
                Wdbg(DebugLevel.E, "Mode {0} was neither add nor rem.", mode)
                Write(DoTranslation("Invalid mode {0}."), True, ColTypes.Error, mode)
            End If

            'Save all aliases
            SaveAliases()
        Else
            Wdbg(DebugLevel.E, "Type {0} not found.", Type)
            Write(DoTranslation("Invalid type {0}."), True, ColTypes.Error, Type)
        End If
    End Sub

    ''' <summary>
    ''' Adds alias to kernel
    ''' </summary>
    ''' <param name="SourceAlias">A command to be aliased. It should exist in both shell and remote debug.</param>
    ''' <param name="Destination">A one-word ccommand to alias to.</param>
    ''' <param name="Type">Alias type, whether it be shell or remote debug.</param>
    ''' <returns>True if successful, False if unsuccessful.</returns>
    ''' <exception cref="Exceptions.AliasInvalidOperationException"></exception>
    ''' <exception cref="Exceptions.AliasNoSuchCommandException"></exception>
    ''' <exception cref="Exceptions.AliasAlreadyExistsException"></exception>
    ''' <exception cref="Exceptions.AliasNoSuchTypeException"></exception>
    Public Function AddAlias(SourceAlias As String, Destination As String, Type As ShellCommandType) As Boolean
        If [Enum].IsDefined(GetType(ShellCommandType), Type) Then
            If SourceAlias = Destination Then
                Wdbg(DebugLevel.I, "Assertion succeeded: {0} = {1}", SourceAlias, Destination)
                Throw New Exceptions.AliasInvalidOperationException(DoTranslation("Alias can't be the same name as a command."))
            ElseIf Not IsCommandFound(Destination) Then
                Wdbg(DebugLevel.W, "{0} not found in all the command lists", Destination)
                Throw New Exceptions.AliasNoSuchCommandException(DoTranslation("Command not found to alias to {0}."), Destination)
            ElseIf DoesAliasExist(SourceAlias, Type) Then
                Wdbg(DebugLevel.W, "Alias {0} already found", SourceAlias)
                Throw New Exceptions.AliasAlreadyExistsException(DoTranslation("Alias already found: {0}"), SourceAlias)
            Else
                Wdbg(DebugLevel.W, "Aliasing {0} to {1}", SourceAlias, Destination)
                Select Case Type
                    Case ShellCommandType.Shell
                        Aliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.RemoteDebugShell
                        RemoteDebugAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.FTPShell
                        FTPShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.SFTPShell
                        SFTPShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.MailShell
                        MailShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.TextShell
                        TextShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.TestShell
                        TestShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.ZIPShell
                        ZIPShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.RSSShell
                        RSSShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.JsonShell
                        JsonShellAliases.Add(SourceAlias, Destination)
                    Case ShellCommandType.HTTPShell
                        HTTPShellAliases.Add(SourceAlias, Destination)
                End Select
                Return True
            End If
        Else
            Wdbg(DebugLevel.E, "Type {0} not found.", Type)
            Throw New Exceptions.AliasNoSuchTypeException(DoTranslation("Invalid type {0}."), Type)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Removes alias from kernel
    ''' </summary>
    ''' <param name="TargetAlias">An alias that needs to be removed.</param>
    ''' <param name="Type">Alias type, whether it be shell or remote debug.</param>
    ''' <returns>True if successful, False if unsuccessful.</returns>
    ''' <exception cref="Exceptions.AliasNoSuchAliasException"></exception>
    ''' <exception cref="Exceptions.AliasNoSuchTypeException"></exception>
    Public Function RemoveAlias(TargetAlias As String, Type As ShellCommandType) As Boolean
        'Variables
        Dim TargetAliasList = Aliases
        Dim TargetAliasType As ShellCommandType = ShellCommandType.Shell

        'Iterate through type to select appropriate alias list
        Select Case Type
            Case ShellCommandType.RemoteDebugShell
                TargetAliasList = RemoteDebugAliases
                TargetAliasType = ShellCommandType.RemoteDebugShell
            Case ShellCommandType.FTPShell
                TargetAliasList = FTPShellAliases
                TargetAliasType = ShellCommandType.FTPShell
            Case ShellCommandType.SFTPShell
                TargetAliasList = SFTPShellAliases
                TargetAliasType = ShellCommandType.SFTPShell
            Case ShellCommandType.MailShell
                TargetAliasList = MailShellAliases
                TargetAliasType = ShellCommandType.MailShell
            Case ShellCommandType.TextShell
                TargetAliasList = TextShellAliases
                TargetAliasType = ShellCommandType.TextShell
            Case ShellCommandType.TestShell
                TargetAliasList = TestShellAliases
                TargetAliasType = ShellCommandType.TestShell
            Case ShellCommandType.ZIPShell
                TargetAliasList = ZIPShellAliases
                TargetAliasType = ShellCommandType.ZIPShell
            Case ShellCommandType.RSSShell
                TargetAliasList = RSSShellAliases
                TargetAliasType = ShellCommandType.RSSShell
            Case ShellCommandType.JsonShell
                TargetAliasList = JsonShellAliases
                TargetAliasType = ShellCommandType.JsonShell
            Case ShellCommandType.HTTPShell
                TargetAliasList = HTTPShellAliases
                TargetAliasType = ShellCommandType.HTTPShell
        End Select

        'Do the action!
        If TargetAliasList.ContainsKey(TargetAlias) Then
            Dim Aliased As String = TargetAliasList(TargetAlias)
            Wdbg(DebugLevel.I, "aliases({0}) is found. That makes it {1}", TargetAlias, Aliased)
            TargetAliasList.Remove(TargetAlias)
            AliasesToBeRemoved.Add($"{AliasesToBeRemoved.Count + 1}-{TargetAlias}", TargetAliasType)
            Return True
        Else
            Wdbg(DebugLevel.W, "{0} is not found in the {1} aliases", TargetAlias, TargetAliasType.ToString)
            Throw New Exceptions.AliasNoSuchAliasException(DoTranslation("Alias {0} is not found to be removed."), TargetAlias)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Purge aliases that are removed from config
    ''' </summary>
    Public Sub PurgeAliases()
        'Get all aliases from file
        If Not FileExists(GetKernelPath(KernelPathType.Aliases)) Then MakeFile(GetKernelPath(KernelPathType.Aliases))
        Dim AliasJsonContent As String = File.ReadAllText(GetKernelPath(KernelPathType.Aliases))
        Dim AliasNameToken As JArray = JArray.Parse(If(Not String.IsNullOrEmpty(AliasJsonContent), AliasJsonContent, "[]"))

        'Purge aliases that are to be removed from config
        For Each TargetAliasItem As String In AliasesToBeRemoved.Keys
            For RemovedAliasIndex As Integer = AliasNameToken.Count - 1 To 0 Step -1
                Dim TargetAliasType As ShellCommandType = AliasesToBeRemoved(TargetAliasItem)
                Dim TargetAlias As String = TargetAliasItem.Substring(TargetAliasItem.IndexOf("-") + 1)
                Select Case TargetAliasType
                    Case ShellCommandType.Shell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "Shell" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.RemoteDebugShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "Remote" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.FTPShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "FTPShell" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.SFTPShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "SFTPShell" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.MailShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "Mail" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.TextShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "Text" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.TestShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "Test" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.ZIPShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "ZIP" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.RSSShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "RSS" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.JsonShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "JSON" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                    Case ShellCommandType.HTTPShell
                        If AliasNameToken(RemovedAliasIndex)("Alias") = TargetAlias And AliasNameToken(RemovedAliasIndex)("Type") = "HTTP" Then AliasNameToken.RemoveAt(RemovedAliasIndex)
                End Select
            Next
        Next

        'Clear the "to be removed" list of aliases
        AliasesToBeRemoved.Clear()

        'Save the changes
        File.WriteAllText(GetKernelPath(KernelPathType.Aliases), JsonConvert.SerializeObject(AliasNameToken, Formatting.Indented))
    End Sub

    ''' <summary>
    ''' Checks to see if the specified alias exists.
    ''' </summary>
    ''' <param name="TargetAlias">The existing alias</param>
    ''' <param name="Type">The alias type</param>
    ''' <returns>True if it exists; false if it doesn't exist</returns>
    Public Function DoesAliasExist(TargetAlias As String, Type As ShellCommandType) As Boolean
        'Get all aliases from file
        If Not FileExists(GetKernelPath(KernelPathType.Aliases)) Then MakeFile(GetKernelPath(KernelPathType.Aliases))
        Dim AliasJsonContent As String = File.ReadAllText(GetKernelPath(KernelPathType.Aliases))
        Dim AliasNameToken As JArray = JArray.Parse(If(Not String.IsNullOrEmpty(AliasJsonContent), AliasJsonContent, "[]"))

        'Check to see if the specified alias exists
        Select Case Type
            Case ShellCommandType.Shell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "Shell" Then Return True
                Next
            Case ShellCommandType.RemoteDebugShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "Remote" Then Return True
                Next
            Case ShellCommandType.FTPShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "FTPShell" Then Return True
                Next
            Case ShellCommandType.SFTPShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "SFTPShell" Then Return True
                Next
            Case ShellCommandType.MailShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "Mail" Then Return True
                Next
            Case ShellCommandType.TextShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "Text" Then Return True
                Next
            Case ShellCommandType.TestShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "Test" Then Return True
                Next
            Case ShellCommandType.ZIPShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "ZIP" Then Return True
                Next
            Case ShellCommandType.RSSShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "RSS" Then Return True
                Next
            Case ShellCommandType.JsonShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "JSON" Then Return True
                Next
            Case ShellCommandType.HTTPShell
                For Each AliasName As JObject In AliasNameToken
                    If AliasName("Alias") = TargetAlias And AliasName("Type") = "HTTP" Then Return True
                Next
            Case Else
                Wdbg(DebugLevel.E, "Type {0} not found.", Type)
                Throw New Exceptions.AliasNoSuchTypeException(DoTranslation("Invalid type {0}."), Type)
        End Select
        Return False
    End Function

End Module
