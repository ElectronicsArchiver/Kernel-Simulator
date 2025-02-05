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

Imports KS.TimeDate

<TestFixture> Public Class TimeInitializationTests

    ''' <summary>
    ''' Tests initializing current times in all timezones
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestGetTimeZones()
        KernelDateTime = Date.Now
        KernelDateTimeUtc = Date.UtcNow
        Dim TimeZones As Dictionary(Of String, Date) = GetTimeZones()
        TimeZones.ShouldNotBeNull
        TimeZones.ShouldNotBeEmpty
    End Sub

End Class