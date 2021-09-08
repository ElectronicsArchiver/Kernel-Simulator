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

Imports Newtonsoft.Json.Linq
Imports System.IO

Public Module JsonMinifier

    ''' <summary>
    ''' Minifies the JSON text contained in the file.
    ''' </summary>
    ''' <param name="JsonFile">Path to JSON file. It's automatically neutralized using <see cref="NeutralizePath(String, Boolean)"/>.</param>
    ''' <returns>Minified JSON</returns>
    ''' <exception cref="FileNotFoundException"></exception>
    Public Function MinifyJson(JsonFile As String) As String
        'Neutralize the file path
        Wdbg("I", "Neutralizing json file {0}...", JsonFile)
        JsonFile = NeutralizePath(JsonFile, True)
        Wdbg("I", "Got json file {0}...", JsonFile)

        'Try to minify JSON
        Dim JsonFileContents As String = File.ReadAllText(JsonFile)
        Return MinifyJsonText(JsonFileContents)
    End Function

    ''' <summary>
    ''' Minifies the JSON text.
    ''' </summary>
    ''' <param name="JsonText">Contents of a beautified JSON.</param>
    ''' <returns>Minified JSON</returns>
    Public Function MinifyJsonText(JsonText As String) As String
        'Make an instance of JToken with this text
        Dim JsonToken As JToken = JToken.Parse(JsonText)
        Wdbg("I", "Created a token with text length of {0}", JsonText.Length)

        'Minify JSON
        Dim MinifiedJson As String = JsonConvert.SerializeObject(JsonToken)
        Wdbg("I", "Minified the JSON text. Length: {0}", MinifiedJson.Length)
        Return MinifiedJson
    End Function

End Module
