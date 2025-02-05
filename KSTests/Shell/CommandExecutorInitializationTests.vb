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

Imports KS.Shell.ShellBase.Commands

<TestFixture> Public Class CommandExecutorInitializationTests

    Shared CommandInstance As CommandExecutor

    ''' <summary>
    ''' Tests initializing the command instance from base
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestInitializeCommandExecutorFromBase()
        'Create instance
        CommandInstance = New CommandTest()

        'Check for null
        CommandInstance.ShouldNotBeNull
    End Sub

    ''' <summary>
    ''' Tests initializing the command instance from base
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestInitializedCommandExecution()
        Should.NotThrow(New Action(Sub() CommandInstance.Execute("", Array.Empty(Of String)(), Array.Empty(Of String)(), Array.Empty(Of String)())))
    End Sub

    ''' <summary>
    ''' Tests initializing the command instance from base
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestInitializedCommandExecutionWithArguments()
        Should.NotThrow(New Action(Sub() CommandInstance.Execute("Hello World", {"Hello", "World"}, {"Hello", "World"}, Array.Empty(Of String)())))
    End Sub

    ''' <summary>
    ''' Tests initializing the command instance from base
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestInitializedCommandExecutionWithSwitches()
        Should.NotThrow(New Action(Sub() CommandInstance.Execute("-s", {"-s"}, Array.Empty(Of String)(), {"-s"})))
    End Sub

    ''' <summary>
    ''' Tests initializing the command instance from base
    ''' </summary>
    <Test, Description("Initialization")> Public Sub TestInitializedCommandExecutionWithArgumentsAndSwitches()
        Should.NotThrow(New Action(Sub() CommandInstance.Execute("-s Hello!", {"-s", "Hello!"}, {"Hello!"}, {"-s"})))
    End Sub

End Class