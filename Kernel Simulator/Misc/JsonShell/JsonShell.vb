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
Imports System.Threading
Imports Newtonsoft.Json.Linq

Public Module JsonShell

    'Variables
    Public ReadOnly JsonShell_Commands As New Dictionary(Of String, CommandInfo) From {{"addproperty", New CommandInfo("addproperty", ShellCommandType.JsonShell, "Adds a new property at the end of the JSON file", {"<parentProperty> <propertyName> <propertyValue>"}, True, 3, New JsonShell_AddPropertyCommand)},
                                                                                       {"clear", New CommandInfo("clear", ShellCommandType.JsonShell, "Clears the JSON file", {}, False, 0, New JsonShell_ClearCommand)},
                                                                                       {"delproperty", New CommandInfo("delproperty", ShellCommandType.JsonShell, "Removes a property from the JSON file", {"<propertyName>"}, True, 1, New JsonShell_DelPropertyCommand)},
                                                                                       {"exit", New CommandInfo("exit", ShellCommandType.JsonShell, "Exits the JSON shell", {}, False, 0, New JsonShell_ExitCommand)},
                                                                                       {"exitnosave", New CommandInfo("exitnosave", ShellCommandType.JsonShell, "Exits the JSON shell without saving the changes", {}, False, 0, New JsonShell_ExitNoSaveCommand)},
                                                                                       {"help", New CommandInfo("help", ShellCommandType.JsonShell, "Lists available commands", {"[command]"}, False, 0, New JsonShell_HelpCommand)},
                                                                                       {"print", New CommandInfo("print", ShellCommandType.JsonShell, "Prints the JSON file", {"[property]"}, False, 0, New JsonShell_PrintCommand)},
                                                                                       {"save", New CommandInfo("save", ShellCommandType.JsonShell, "Saves the JSON file", {"[-b|-m]"}, False, 0, New JsonShell_SaveCommand, False, False, False, False, False, New Action(AddressOf (New JsonShell_SaveCommand).HelpHelper))}}
    Public JsonShell_ModCommands As New ArrayList
    Public JsonShell_FileStream As FileStream
    Public JsonShell_FileToken As JToken = JToken.Parse("{}")
    Friend JsonShell_FileTokenOrig As JToken = JToken.Parse("{}")
    Public JsonShell_AutoSave As New Thread(AddressOf JsonShell_HandleAutoSaveJsonFile) With {.Name = "JSON Shell Autosave Thread"}
    Public JsonShell_AutoSaveFlag As Boolean = True
    Public JsonShell_AutoSaveInterval As Integer = 60
    Public JsonShell_PromptStyle As String = ""
    Public JsonShell_Formatting As Formatting = Formatting.Indented
    Friend JsonShell_Exiting As Boolean

    Public Sub InitializeJsonShell(FilePath As String)
        'Add handler for JSON shell
        SwitchCancellationHandler(ShellCommandType.JsonShell)

        While Not JsonShell_Exiting
            SyncLock JsonShellCancelSync
                'Open file if not open
                If JsonShell_FileStream Is Nothing Then
                    Wdbg(DebugLevel.W, "File not open yet. Trying to open {0}...", FilePath)
                    If Not JsonShell_OpenJsonFile(FilePath) Then
                        Write(DoTranslation("Failed to open file. Exiting shell..."), True, ColTypes.Error)
                        Exit While
                    End If
                    JsonShell_AutoSave.Start()
                End If

                'Prepare for prompt
                If DefConsoleOut IsNot Nothing Then
                    Console.SetOut(DefConsoleOut)
                End If
                Wdbg(DebugLevel.I, "JsonShell_PromptStyle = {0}", JsonShell_PromptStyle)
                If JsonShell_PromptStyle = "" Then
                    Write("[", False, ColTypes.Gray) : Write("{0}{1}", False, ColTypes.UserName, Path.GetFileName(FilePath), If(JsonShell_WasJsonEdited(), "*", "")) : Write("] > ", False, ColTypes.Gray)
                Else
                    Dim ParsedPromptStyle As String = ProbePlaces(JsonShell_PromptStyle)
                    ParsedPromptStyle.ConvertVTSequences
                    Write(ParsedPromptStyle, False, ColTypes.Gray)
                End If
                SetInputColor()

                'Prompt for command
                KernelEventManager.RaiseTextShellInitialized()
                Dim WrittenCommand As String = Console.ReadLine

                'Check to see if the command doesn't start with spaces or if the command is nothing
                Wdbg(DebugLevel.I, "Starts with spaces: {0}, Is Nothing: {1}, Is Blank {2}", WrittenCommand?.StartsWith(" "), WrittenCommand Is Nothing, WrittenCommand = "")
                If Not (WrittenCommand = Nothing Or WrittenCommand?.StartsWithAnyOf({" ", "#"}) = True) Then
                    Dim Command As String = WrittenCommand.SplitEncloseDoubleQuotes(" ")(0)
                    Wdbg(DebugLevel.I, "Checking command {0} for existence.", Command)
                    If JsonShell_Commands.ContainsKey(Command) Then
                        Wdbg(DebugLevel.I, "Command {0} found in the list of {1} commands.", Command, JsonShell_Commands.Count)
                        Dim Params As New ExecuteCommandThreadParameters(WrittenCommand, ShellCommandType.JsonShell, Nothing)
                        JsonShell_CommandThread = New Thread(AddressOf ExecuteCommand) With {.Name = "JSON Shell Command Thread"}
                        KernelEventManager.RaiseTextPreExecuteCommand(WrittenCommand)
                        Wdbg(DebugLevel.I, "Made new thread. Starting with argument {0}...", WrittenCommand)
                        JsonShell_CommandThread.Start(Params)
                        JsonShell_CommandThread.Join()
                        KernelEventManager.RaiseTextPostExecuteCommand(WrittenCommand)
                    ElseIf JsonShell_ModCommands.Contains(Command) Then
                        Wdbg(DebugLevel.I, "Mod command {0} executing...", Command)
                        ExecuteModCommand(WrittenCommand)
                    ElseIf JsonShellAliases.Keys.Contains(Command) Then
                        Wdbg(DebugLevel.I, "JSON shell alias command found.")
                        WrittenCommand = WrittenCommand.Replace($"""{Command}""", Command)
                        ExecuteJsonAlias(WrittenCommand)
                    Else
                        Write(DoTranslation("The specified JSON shell command is not found."), True, ColTypes.Error)
                        Wdbg(DebugLevel.E, "Command {0} not found in the list of {1} commands.", Command, JsonShell_Commands.Count)
                    End If
                End If
            End SyncLock
        End While

        'Close file
        JsonShell_CloseTextFile()
        JsonShell_AutoSave.Abort()
        JsonShell_AutoSave = New Thread(AddressOf JsonShell_HandleAutoSaveJsonFile) With {.Name = "JSON Shell Autosave Thread"}

        'Remove handler for JSON shell
        SwitchCancellationHandler(LastShellType)
        JsonShell_Exiting = False
    End Sub

End Module
