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

Module Flags

    'Variables: Normal environment
    Public TimeDateIsSet As Boolean = False                     'To fix a bug after reboot
    Public StopPanicAndGoToDoublePanic As Boolean               'Double panic mode in kernel error
    Public DebugMode As Boolean = False                         'Toggle Debugging mode
    Public LoginFlag As Boolean                                 'Flag for log-in
    Public CommandFlag As Boolean = False                       'A signal for command kernel argument
    Public argsFlag As Boolean                                  'A flag for checking for an argument later
    Public argsInjected As Boolean                              'A flag for checking for an argument on reboot
    Public setRootPasswd As Boolean = False                     'Set Root Password boolean
    Public RootPasswd As String = ""                            'Set Root Password to any password
    Public maintenance As Boolean = False                       'Maintenance Mode
    Public argsOnBoot As Boolean = False                        'Arguments On Boot
    Public clsOnLogin As Boolean = False                        'Clear Screen On Log-in
    Public showMOTD As Boolean = True                           'Show MOTD on log-in
    Public simHelp As Boolean = False                           'Simplified Help Command
    Public slotProbe As Boolean = True                          'Probe slots
    Public quietProbe As Boolean = False                        'Probe quietly
    Public CornerTD As Boolean = False                          'Show Time/Date on corner
    Public instanceChecked As Boolean = False                   'Instance checking
    Public LogoutRequested As Boolean = False                   'A signal when user logs out.
    Public RebootRequested As Boolean = False                   'Reboot requested
    Public FTPLoggerUsername As Boolean                         'Log username for FTP
    Public FTPLoggerIP As Boolean                               'Log IP address for FTP
    Public FTPFirstProfileOnly As Boolean                       'Only first profile will be returned
    Public SafeMode As Boolean                                  'Whether safe mode is enabled
    Public FullParseMode As Boolean                             'Whether or not to parse whole directory for size
    Public ScrnTimeReached As Boolean                           'When the screensaver timer has reached
    Public StartScroll As Boolean = True                        'Enable marquee on startup
    Public LongTimeDate As Boolean = True                       'Whether or not to render time and dates short or long
    Public LoggedIn As Boolean                                  'Whether or not to detect if the user is logged in
    Public ShowAvailableUsers As Boolean = True                 'Whether or not to show available usernames on login
    Public HiddenFiles As Boolean                               'Whether or not to show hidden files
    Public CheckUpdateStart As Boolean = True                   'Whether or not to ckeck for updates on startup
    Public ColorMix255Colors As Boolean                         'Enable 255 color support for ColorMix
    Public Disco255Colors As Boolean                            'Enable 255 color support for Disco
    Public GlitterColor255Colors As Boolean                     'Enable 255 color support for GlitterColor
    Public Lines255Colors As Boolean                            'Enable 255 color support for Lines

    'Private flags
    Friend NotifyConfigError As Boolean                        'Notifies user as soon as the kernel finished booting if there is an error reading configuration.

End Module
