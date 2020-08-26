﻿
'    Kernel Simulator  Copyright (C) 2018-2020  EoflaOE
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
Imports KS

<TestClass()> Public Class LocalizationTests

    <TestMethod()> Public Sub TestTranslate()
        Dim ExpectedTranslation As String = "---===+++> Bienvenido al kernel | Versión {0} <+++===---"
        Dim ActualTranslation As String = DoTranslation("---===+++> Welcome to the kernel | Version {0} <+++===---", "spa")
        Assert.AreEqual(ExpectedTranslation, ActualTranslation, "Translation test is not done properly. Got {0}", ActualTranslation)
    End Sub

    <TestMethod> Public Sub TestPrepareDict()
        Dim ExpectedLength As Integer = KS.My.Resources.spa.Replace(Chr(13), "").Split(Chr(10)).ToList.Count
        Dim ActualLength As Integer = PrepareDict("spa").Values.Count
        Assert.AreEqual(ExpectedLength, ActualLength, "Dictionary preparation test is not done properly. Expected {0}, Got {1}", ExpectedLength, ActualLength)
    End Sub

    <TestMethod> Public Sub TestUpdateCulture()
        currentLang = "spa"
        Dim ExpectedCulture As String = "Spanish (Spain, International Sort)"
        UpdateCulture()
        Assert.AreEqual(ExpectedCulture, CurrentCult.EnglishName, "Culture update test is not done properly. Got {0}", CurrentCult.EnglishName)
    End Sub

    <TestMethod> Public Sub TestSetLang()
        InitPaths()
        Dim PathToTestConfig As String = Path.GetFullPath("TestConfig.ini")
        If Not File.Exists(paths("Configuration")) Then File.Copy(PathToTestConfig, paths("Configuration"))
        Assert.IsTrue(SetLang("spa"), "Setting language failed. Returned False.")
    End Sub

End Class