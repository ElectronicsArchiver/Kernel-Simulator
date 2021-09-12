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

Class FileInfoCommand
    Inherits CommandExecutor
    Implements ICommand

    Public Overrides Sub Execute(StringArgs As String, ListArgs() As String) Implements ICommand.Execute
        For Each FileName As String In ListArgs
            Dim FilePath As String = NeutralizePath(FileName)
            Wdbg(DebugLevel.I, "Neutralized file path: {0} ({1})", FilePath, File.Exists(FilePath))
            WriteSeparator(FileName, True)
            If File.Exists(FilePath) Then
                Dim FileInfo As New FileInfo(FilePath)
                W(DoTranslation("Name: {0}"), True, ColTypes.Neutral, FileInfo.Name)
                W(DoTranslation("Full name: {0}"), True, ColTypes.Neutral, NeutralizePath(FileInfo.FullName))
                W(DoTranslation("File size: {0}"), True, ColTypes.Neutral, FileInfo.Length.FileSizeToString)
                W(DoTranslation("Creation time: {0}"), True, ColTypes.Neutral, Render(FileInfo.CreationTime))
                W(DoTranslation("Last access time: {0}"), True, ColTypes.Neutral, Render(FileInfo.LastAccessTime))
                W(DoTranslation("Last write time: {0}"), True, ColTypes.Neutral, Render(FileInfo.LastWriteTime))
                W(DoTranslation("Attributes: {0}"), True, ColTypes.Neutral, FileInfo.Attributes)
                W(DoTranslation("Where to find: {0}"), True, ColTypes.Neutral, NeutralizePath(FileInfo.DirectoryName))
            Else
                W(DoTranslation("Can't get information about nonexistent file."), True, ColTypes.Error)
            End If
        Next
    End Sub

End Class