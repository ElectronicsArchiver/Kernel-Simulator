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

Imports System.IO
Imports KS.Misc.Encryption

<TestFixture> Public Class EncryptionActionTests

    ''' <summary>
    ''' Tests string encryption
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:="9413827E"),
     TestCase(Algorithms.MD5, ExpectedResult:="C4C1867580D6D25B11210F84F935359A"),
     TestCase(Algorithms.SHA1, ExpectedResult:="CFF9FDA895B0B638957E17CF952457D81ADD622F"),
     TestCase(Algorithms.SHA256, ExpectedResult:="525514740C93C5442DBCB8FB92FB1B17B6F8B94B3C98E6F07CA8AEB093C2E79F"),
     TestCase(Algorithms.SHA384, ExpectedResult:="B26ADFF6A6BDD59612F4B560E7D2A0240D7A611AF46BD4D2891181F46341E4886A8D74724877955AFC908F6B17A5B613"),
     TestCase(Algorithms.SHA512, ExpectedResult:="0015CAF195A7248127F7E50C8D839935681A2234344387B5E9DF761E6D4F152CC4458ADCD45A19F59413EA6BC5E7C907A01A0B47B548CE0DAD04787CE416157D"),
     Description("Action")>
    Public Function TestGetEncryptedString(Algorithm As Algorithms) As String
        Dim TextHash As String = "Test hashing."
        Return GetEncryptedString(TextHash, Algorithm)
    End Function

    ''' <summary>
    ''' Tests file encryption
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:="9413827E"),
     TestCase(Algorithms.MD5, ExpectedResult:="C4C1867580D6D25B11210F84F935359A"),
     TestCase(Algorithms.SHA1, ExpectedResult:="CFF9FDA895B0B638957E17CF952457D81ADD622F"),
     TestCase(Algorithms.SHA256, ExpectedResult:="525514740C93C5442DBCB8FB92FB1B17B6F8B94B3C98E6F07CA8AEB093C2E79F"),
     TestCase(Algorithms.SHA384, ExpectedResult:="B26ADFF6A6BDD59612F4B560E7D2A0240D7A611AF46BD4D2891181F46341E4886A8D74724877955AFC908F6B17A5B613"),
     TestCase(Algorithms.SHA512, ExpectedResult:="0015CAF195A7248127F7E50C8D839935681A2234344387B5E9DF761E6D4F152CC4458ADCD45A19F59413EA6BC5E7C907A01A0B47B548CE0DAD04787CE416157D"),
     Description("Action")>
    Public Function TestGetEncryptedFileUsingStream(Algorithm As Algorithms) As String
        Dim FileStreamHash As FileStream = File.Create(HomePath + "/TestSum.txt")
        FileStreamHash.Write(Text.Encoding.Default.GetBytes("Test hashing."), 0, 13)
        FileStreamHash.Flush()
        FileStreamHash.Position = 0
        Dim ResultHash As String = GetEncryptedFile(FileStreamHash, Algorithm)
        FileStreamHash.Close()
        File.Delete(HomePath + "/TestSum.txt")
        Return ResultHash
    End Function

    ''' <summary>
    ''' Tests file encryption
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:="D394D7F0"),
     TestCase(Algorithms.MD5, ExpectedResult:="CD5578C85A4CF32E48D157746A90C7F6"),
     TestCase(Algorithms.SHA1, ExpectedResult:="36EBF31AF7234D6C99CA65DC4EDA524161600657"),
     TestCase(Algorithms.SHA256, ExpectedResult:="7E6857729A34755DE8C2C9E535A8765BDE241F593BE3588B8FA6D29D949EFADA"),
     TestCase(Algorithms.SHA384, ExpectedResult:="92CBCB3F982C7EC24EED668175D4FE7C73D9BBCBECA659EDDE6D6E56B798D64C808F86C7E13FA6BE03464AE2D145BB60"),
     TestCase(Algorithms.SHA512, ExpectedResult:="6DF635C184D4B131B0243D4F2BD66925A61B82A5093F573920F42D7B8474D6332FD2886920F3CA36D9206C73DD59C8F1EEA18501E6FEF15FDDA664B1ABB0E361"),
     Description("Action")>
    Public Function TestGetEncryptedFileUsingPath(Algorithm As Algorithms) As String
        Dim FileStreamHash As FileStream = File.Create(HomePath + "/TestSum.txt")
        FileStreamHash.Write(Text.Encoding.Default.GetBytes("Test hashing with path."), 0, 23)
        FileStreamHash.Flush()
        FileStreamHash.Close()
        Dim FileHash As String = GetEncryptedFile(HomePath + "/TestSum.txt", Algorithm)
        File.Delete(HomePath + "/TestSum.txt")
        Return FileHash
    End Function

    ''' <summary>
    ''' Tests hash verification
    ''' </summary>
    <TestCase(Algorithms.CRC32, "D394D7F0", ExpectedResult:=True),
     TestCase(Algorithms.MD5, "CD5578C85A4CF32E48D157746A90C7F6", ExpectedResult:=True),
     TestCase(Algorithms.SHA1, "36EBF31AF7234D6C99CA65DC4EDA524161600657", ExpectedResult:=True),
     TestCase(Algorithms.SHA256, "7E6857729A34755DE8C2C9E535A8765BDE241F593BE3588B8FA6D29D949EFADA", ExpectedResult:=True),
     TestCase(Algorithms.SHA384, "92CBCB3F982C7EC24EED668175D4FE7C73D9BBCBECA659EDDE6D6E56B798D64C808F86C7E13FA6BE03464AE2D145BB60", ExpectedResult:=True),
     TestCase(Algorithms.SHA512, "6DF635C184D4B131B0243D4F2BD66925A61B82A5093F573920F42D7B8474D6332FD2886920F3CA36D9206C73DD59C8F1EEA18501E6FEF15FDDA664B1ABB0E361", ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyHashFromHash(Algorithm As Algorithms, ExpectedHash As String) As Boolean
        Dim FileStreamHash As FileStream = File.Create(HomePath + "/TestSum.txt")
        FileStreamHash.Write(Text.Encoding.Default.GetBytes("Test hashing with path."), 0, 23)
        FileStreamHash.Flush()
        FileStreamHash.Close()
        Dim FileHash As String = GetEncryptedFile(HomePath + "/TestSum.txt", Algorithm)
        Dim Result As Boolean = VerifyHashFromHash(HomePath + "/TestSum.txt", Algorithm, ExpectedHash, FileHash)
        File.Delete(HomePath + "/TestSum.txt")
        Return Result
    End Function

    ''' <summary>
    ''' Tests hash verification from hashes file
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:=True),
     TestCase(Algorithms.MD5, ExpectedResult:=True),
     TestCase(Algorithms.SHA1, ExpectedResult:=True),
     TestCase(Algorithms.SHA256, ExpectedResult:=True),
     TestCase(Algorithms.SHA384, ExpectedResult:=True),
     TestCase(Algorithms.SHA512, ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyHashFromFileStdFormat(Algorithm As Algorithms) As Boolean
        Dim DataPath As String = TestContext.CurrentContext.TestDirectory + "/TestData/"
        Dim FileHash As String = GetEncryptedFile(DataPath + "TestText.txt", Algorithm)
        Dim Result As Boolean = VerifyHashFromHashesFile(DataPath + "TestText.txt", Algorithm, DataPath + "TestVerify" + Algorithm.ToString + ".txt", FileHash)
        Return Result
    End Function

    ''' <summary>
    ''' Tests hash verification from hashes file
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:=True),
     TestCase(Algorithms.MD5, ExpectedResult:=True),
     TestCase(Algorithms.SHA1, ExpectedResult:=True),
     TestCase(Algorithms.SHA256, ExpectedResult:=True),
     TestCase(Algorithms.SHA384, ExpectedResult:=True),
     TestCase(Algorithms.SHA512, ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyHashFromFileKSFormat(Algorithm As Algorithms) As Boolean
        Dim DataPath As String = TestContext.CurrentContext.TestDirectory + "/TestData/"
        Dim FileHash As String = GetEncryptedFile(DataPath + "TestText.txt", Algorithm)
        Dim Result As Boolean = VerifyHashFromHashesFile(DataPath + "TestText.txt", Algorithm, DataPath + "TestVerify" + Algorithm.ToString + "KS.txt", FileHash)
        Return Result
    End Function

    ''' <summary>
    ''' Tests hash verification for an uncalculated file
    ''' </summary>
    <TestCase(Algorithms.CRC32, "D394D7F0", ExpectedResult:=True),
     TestCase(Algorithms.MD5, "CD5578C85A4CF32E48D157746A90C7F6", ExpectedResult:=True),
     TestCase(Algorithms.SHA1, "36EBF31AF7234D6C99CA65DC4EDA524161600657", ExpectedResult:=True),
     TestCase(Algorithms.SHA256, "7E6857729A34755DE8C2C9E535A8765BDE241F593BE3588B8FA6D29D949EFADA", ExpectedResult:=True),
     TestCase(Algorithms.SHA384, "92CBCB3F982C7EC24EED668175D4FE7C73D9BBCBECA659EDDE6D6E56B798D64C808F86C7E13FA6BE03464AE2D145BB60", ExpectedResult:=True),
     TestCase(Algorithms.SHA512, "6DF635C184D4B131B0243D4F2BD66925A61B82A5093F573920F42D7B8474D6332FD2886920F3CA36D9206C73DD59C8F1EEA18501E6FEF15FDDA664B1ABB0E361", ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyUncalculatedHashFromHash(Algorithm As Algorithms, ExpectedHash As String) As Boolean
        Dim FileStreamHash As FileStream = File.Create(HomePath + "/TestSum.txt")
        FileStreamHash.Write(Text.Encoding.Default.GetBytes("Test hashing with path."), 0, 23)
        FileStreamHash.Flush()
        FileStreamHash.Close()
        Dim Result As Boolean = VerifyUncalculatedHashFromHash(HomePath + "/TestSum.txt", Algorithm, ExpectedHash)
        File.Delete(HomePath + "/TestSum.txt")
        Return Result
    End Function

    ''' <summary>
    ''' Tests hash verification from hashes file for an uncalculated file
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:=True),
     TestCase(Algorithms.MD5, ExpectedResult:=True),
     TestCase(Algorithms.SHA1, ExpectedResult:=True),
     TestCase(Algorithms.SHA256, ExpectedResult:=True),
     TestCase(Algorithms.SHA384, ExpectedResult:=True),
     TestCase(Algorithms.SHA512, ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyUncalculatedHashFromFileStdFormat(Algorithm As Algorithms) As Boolean
        Dim DataPath As String = TestContext.CurrentContext.TestDirectory + "/TestData/"
        Dim Result As Boolean = VerifyUncalculatedHashFromHashesFile(DataPath + "TestText.txt", Algorithm, DataPath + "TestVerify" + Algorithm.ToString + ".txt")
        Return Result
    End Function

    ''' <summary>
    ''' Tests hash verification from hashes file for an uncalculated file
    ''' </summary>
    <TestCase(Algorithms.CRC32, ExpectedResult:=True),
     TestCase(Algorithms.MD5, ExpectedResult:=True),
     TestCase(Algorithms.SHA1, ExpectedResult:=True),
     TestCase(Algorithms.SHA256, ExpectedResult:=True),
     TestCase(Algorithms.SHA384, ExpectedResult:=True),
     TestCase(Algorithms.SHA512, ExpectedResult:=True),
     Description("Action")>
    Public Function TestVerifyUncalculatedHashFromFileKSFormat(Algorithm As Algorithms) As Boolean
        Dim DataPath As String = TestContext.CurrentContext.TestDirectory + "/TestData/"
        Dim Result As Boolean = VerifyUncalculatedHashFromHashesFile(DataPath + "TestText.txt", Algorithm, DataPath + "TestVerify" + Algorithm.ToString + "KS.txt")
        Return Result
    End Function

    <TestCase(Algorithms.CRC32, ExpectedResult:="00000000"),
     TestCase(Algorithms.MD5, ExpectedResult:="D41D8CD98F00B204E9800998ECF8427E"),
     TestCase(Algorithms.SHA1, ExpectedResult:="DA39A3EE5E6B4B0D3255BFEF95601890AFD80709"),
     TestCase(Algorithms.SHA256, ExpectedResult:="E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"),
     TestCase(Algorithms.SHA384, ExpectedResult:="38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B"),
     TestCase(Algorithms.SHA512, ExpectedResult:="CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E"),
     Description("Action")>
    Public Function TestGetEmptyHash(Algorithm As Algorithms) As String
        Dim Empty As String = GetEmptyHash(Algorithm)
        Empty.ShouldNotBeNullOrEmpty
        Return Empty
    End Function

End Class