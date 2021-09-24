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
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports KS.TextWriterColor
Imports KS.ColorTools
Imports KS.PlatformDetector
Imports KS.KernelTools

Module LocaleGenerator

    ''' <summary>
    ''' Entry point
    ''' </summary>
    Sub Main(Args As String())
        'Check for terminal (macOS only). Go to Kernel.vb on Kernel Simulator for more info.
#If STOCKTERMINALMACOS = False Then
        If IsOnMacOS() Then
            If GetTerminalEmulator() = "Apple_Terminal" Then
                Console.WriteLine("Kernel Simulator makes use of VT escape sequences, but Terminal.app has broken support for 255 and true colors. This program can't continue.")
                Environment.Exit(5)
            End If
        End If
#End If

        Try
            'Enumerate the translations folder
            Dim ToParse As New List(Of String)
            Dim EnglishFile As String = "eng.txt"
            Dim Files = Directory.EnumerateFiles("Translations")

            'Add languages to parse list
            For Each File As String In Files
                If File.EndsWith(".txt") Then
                    Debug.WriteLine(File)
                    ToParse.Add(File)
                End If
                If File.Contains("eng.txt") Then
                    Debug.WriteLine("English file: " + File)
                    EnglishFile = File
                End If
            Next
            ToParse.Sort()

            'Make a localized JSON file for target languages
            For Each File As String In ToParse
                'Initialize two arrays for localization
                Dim FileLines() As String = IO.File.ReadAllLines(File)
                Dim FileLinesEng() As String = IO.File.ReadAllLines(EnglishFile)
                Debug.WriteLine("Lines for {0} (Eng: {1}, Loc: {2})", Path.GetFileNameWithoutExtension(File), FileLinesEng.Length, FileLines.Length)

                'Make a JSON object for each language entry
                Dim LocalizedJson As New JObject
                For i As Integer = 0 To FileLines.Length - 1
                    If Not String.IsNullOrWhiteSpace(FileLines(i)) And Not String.IsNullOrWhiteSpace(FileLinesEng(i)) Then
                        Try
                            Debug.WriteLine("Adding ""{0}, {1}""...", FileLinesEng(i), FileLines(i))
                            LocalizedJson.Add(FileLinesEng(i), FileLines(i))
                        Catch ex As Exception
                            W("Malformed line" + $" {i + 1}: {FileLinesEng(i)} -> {FileLines(i)}", True, ColTypes.Error)
                            W("Error trying to parse above line:" + $" {ex.Message}", True, ColTypes.Error)
                        End Try
                    End If
                Next

                'Save changes
                Debug.WriteLine("Saving as {0}...", Path.GetFileNameWithoutExtension(File) + ".json")
                If Args.Length > 0 AndAlso Args(0) = "--CopyToResources" Then
                    IO.File.WriteAllText("../Resources/" + Path.GetFileNameWithoutExtension(File) + ".json", JsonConvert.SerializeObject(LocalizedJson, Formatting.Indented))
                    W("Saved new language JSON file to" + $" ../Resources/{Path.GetFileNameWithoutExtension(File)}.json!", True, ColTypes.Success)
                Else
                    Directory.CreateDirectory("Translations/Output")
                    IO.File.WriteAllText("Translations/Output/" + Path.GetFileNameWithoutExtension(File) + ".json", JsonConvert.SerializeObject(LocalizedJson, Formatting.Indented))
                    W("Saved new language JSON file to" + $" Translations/Output/{Path.GetFileNameWithoutExtension(File)}.json!", True, ColTypes.Success)
                End If
            Next
        Catch ex As Exception
            W("Unexpected error in converter:" + $" {ex.Message}", True, ColTypes.Error)
            W(ex.StackTrace, True, ColTypes.Error)
        End Try

        'Finish the program
        W("Press any key to continue...", True, ColTypes.Neutral)
        Console.ReadKey()
    End Sub

End Module
