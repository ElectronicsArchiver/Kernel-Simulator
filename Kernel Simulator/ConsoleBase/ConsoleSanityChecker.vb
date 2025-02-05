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

Imports KS.Kernel.Exceptions

Namespace ConsoleBase
    Public Module ConsoleSanityChecker

        ''' <summary>
        ''' Checks the running console for sanity, like the incompatible consoles, insane console types, etc.
        ''' </summary>
        Public Sub CheckConsole()
            'First: Check if the console is running on Apple_Terminal (terminal.app). Explanation below:
            '---
            'This check is needed because we have the stock Terminal.app (Apple_Terminal according to $TERM_PROGRAM) that has incompatibilities with
            'VT sequences, causing broken display. It claims it supports XTerm, yet it isn't fully XTerm-compliant, so we exit the program early when
            'this stock terminal is spotted.
            '---
            'More information regarding this check: The blacklisted terminals will not be able to run Kernel Simulator properly, because they have
            'broken support for colors and possibly more features. For example, we have Apple_Terminal that has no support for 255 and true colors;
            'it only supports 16 colors setting by VT sequences and nothing can change that, although it's fully XTerm compliant.
            If IsOnMacOS() Then
                If GetTerminalEmulator() = "Apple_Terminal" Then
                    Throw New InsaneConsoleDetectedException("Kernel Simulator makes use of VT escape sequences, but Terminal.app has broken support for 255 and true colors." + NewLine +
                                                             "Possible solution: Download iTerm2 here: https://iterm2.com/downloads.html")
                End If
            End If

            'Second: Check if the terminal type is "dumb". Explanation below:
            '---
            'The "dumb" terminals usually are not useful for interactive applications, since they only provide the stdout and stderr streams without
            'support for console cursor manipulation, which Kernel Simulator heavily depends on. These terminals are used for streaming output to the
            'appropriate variable, like the frontend applications that rely on console applications and their outputs to do their job (for example,
            'Brasero, a disk burning program, uses wodim, xorriso, and such applications to do its very intent of burning a blank CD-ROM. All these
            'backend applications are console programs).
            If GetTerminalType() = "dumb" Then
                Throw New InsaneConsoleDetectedException("Kernel Simulator makes use of inputs and cursor manipulation, but the ""dumb"" terminals have no support for such tasks." + NewLine +
                                                         "Possible solution: Use an appropriate terminal emulator or consult your terminal settings to set the terminal type into something other than ""dumb""." + NewLine +
                                                         "                   We recommend using the ""vt100"" terminal emulators to get the most out of Kernel Simulator.")
            End If
        End Sub

    End Module
End Namespace