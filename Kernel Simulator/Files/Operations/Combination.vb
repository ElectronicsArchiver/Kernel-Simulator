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

Imports KS.Files.Read

Namespace Files.Operations
    Public Module Combination

        ''' <summary>
        ''' Combines the files and puts the combined output to the array
        ''' </summary>
        ''' <param name="Input">An input file</param>
        ''' <param name="TargetInputs">The target inputs to merge</param>
        Public Function CombineFiles(Input As String, TargetInputs() As String) As String()
            Try
                Dim CombinedContents As New List(Of String)

                'Add the input contents
                ThrowOnInvalidPath(Input)
                CombinedContents.AddRange(ReadContents(Input))

                'Enumerate the target inputs
                For Each TargetInput As String In TargetInputs
                    ThrowOnInvalidPath(TargetInput)
                    CombinedContents.AddRange(ReadContents(TargetInput))
                Next

                'Return the combined contents
                Return CombinedContents.ToArray
            Catch ex As Exception
                WStkTrc(ex)
                Wdbg(DebugLevel.E, "Failed to combine files: {0}", ex.Message)
                Throw New Exceptions.FilesystemException(DoTranslation("Failed to combine files."), ex)
            End Try
        End Function

    End Module
End Namespace
