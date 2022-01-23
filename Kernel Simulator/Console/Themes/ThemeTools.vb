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

Namespace ConsoleBase.Themes
    Public Module ThemeTools

        ''' <summary>
        ''' All the available built-in themes
        ''' </summary>
        Public ReadOnly Themes As New Dictionary(Of String, ThemeInfo) From {{"Default", New ThemeInfo("_Default")},
                                                                         {"3Y-Diamond", New ThemeInfo("_3Y_Diamond")},
                                                                         {"Aquatic", New ThemeInfo("Aquatic")},
                                                                         {"AyuDark", New ThemeInfo("AyuDark")},
                                                                         {"AyuLight", New ThemeInfo("AyuLight")},
                                                                         {"AyuMirage", New ThemeInfo("AyuMirage")},
                                                                         {"BlackOnWhite", New ThemeInfo("BlackOnWhite")},
                                                                         {"BedOS", New ThemeInfo("BedOS")},
                                                                         {"Bloody", New ThemeInfo("Bloody")},
                                                                         {"Blue Power", New ThemeInfo("Blue_Power")},
                                                                         {"Bluesome", New ThemeInfo("Bluesome")},
                                                                         {"Bluespire", New ThemeInfo("Bluespire")},
                                                                         {"BrandingBlue", New ThemeInfo("BrandingBlue")},
                                                                         {"BrandingPurple", New ThemeInfo("BrandingPurple")},
                                                                         {"BreezeDark", New ThemeInfo("BreezeDark")},
                                                                         {"Breeze", New ThemeInfo("Breeze")},
                                                                         {"Dawn Aurora", New ThemeInfo("Dawn_Aurora")},
                                                                         {"Debian", New ThemeInfo("Debian")},
                                                                         {"Fire", New ThemeInfo("Fire")},
                                                                         {"Grape", New ThemeInfo("Grape")},
                                                                         {"Grape Kiwi", New ThemeInfo("Grape_Kiwi")},
                                                                         {"GTASA", New ThemeInfo("GTASA")},
                                                                         {"Grays", New ThemeInfo("Grays")},
                                                                         {"GrayOnYellow", New ThemeInfo("GrayOnYellow")},
                                                                         {"Green Mix", New ThemeInfo("Green_Mix")},
                                                                         {"Grink", New ThemeInfo("Grink")},
                                                                         {"Hacker", New ThemeInfo("Hacker")},
                                                                         {"Lemon", New ThemeInfo("Lemon")},
                                                                         {"Light Planks", New ThemeInfo("Light_Planks")},
                                                                         {"LinuxColoredDef", New ThemeInfo("LinuxColoredDef")},
                                                                         {"LinuxUncolored", New ThemeInfo("LinuxUncolored")},
                                                                         {"Materialistic", New ThemeInfo("Materialistic")},
                                                                         {"Metallic", New ThemeInfo("Metallic")},
                                                                         {"Mint", New ThemeInfo("Mint")},
                                                                         {"Mint Gum", New ThemeInfo("Mint_Gum")},
                                                                         {"Mintback", New ThemeInfo("Mintback")},
                                                                         {"Mintish", New ThemeInfo("Mintish")},
                                                                         {"NeonBreeze", New ThemeInfo("NeonBreeze")},
                                                                         {"NFSHP-Cop", New ThemeInfo("NFSHP_Cop")},
                                                                         {"NFSHP-Racer", New ThemeInfo("NFSHP_Racer")},
                                                                         {"Planted Wood", New ThemeInfo("Planted_Wood")},
                                                                         {"Purlow", New ThemeInfo("Purlow")},
                                                                         {"Red Breeze", New ThemeInfo("Red_Breeze")},
                                                                         {"RedConsole", New ThemeInfo("RedConsole")},
                                                                         {"Reddish", New ThemeInfo("Reddish")},
                                                                         {"SolarizedDark", New ThemeInfo("SolarizedDark")},
                                                                         {"SolarizedLight", New ThemeInfo("SolarizedLight")},
                                                                         {"Tealed", New ThemeInfo("Tealed")},
                                                                         {"TealerOS", New ThemeInfo("TealerOS")},
                                                                         {"TrafficLight", New ThemeInfo("TrafficLight")},
                                                                         {"Ubuntu", New ThemeInfo("Ubuntu")},
                                                                         {"YellowBG", New ThemeInfo("YellowBG")},
                                                                         {"YellowFG", New ThemeInfo("YellowFG")},
                                                                         {"Windows11", New ThemeInfo("Windows11")},
                                                                         {"Windows11Light", New ThemeInfo("Windows11Light")},
                                                                         {"Windows95", New ThemeInfo("Windows95")},
                                                                         {"Wood", New ThemeInfo("Wood")}}

        ''' <summary>
        ''' Sets system colors according to the programmed templates
        ''' </summary>
        ''' <param name="theme">A specified theme</param>
        Public Sub ApplyThemeFromResources(theme As String)
            Wdbg(DebugLevel.I, "Theme: {0}", theme)
            If Themes.ContainsKey(theme) Then
                Wdbg(DebugLevel.I, "Theme found.")

                'Populate theme info
                Dim ThemeInfo As ThemeInfo
                theme = theme.ReplaceAll({"-", " "}, "_")
                If theme = "Default" Then
                    ResetColors()
                ElseIf theme = "3Y-Diamond" Then
                    ThemeInfo = New ThemeInfo("_3Y_Diamond")
                Else
                    ThemeInfo = New ThemeInfo(theme)
                End If

                If Not theme = "Default" Then
#Disable Warning BC42104
                    'Set colors as appropriate
                    SetColorsTheme(ThemeInfo)
#Enable Warning BC42104
                End If

                'Raise event
                Kernel.KernelEventManager.RaiseThemeSet(theme)
            Else
                Write(DoTranslation("Invalid color template {0}"), True, ColTypes.Error, theme)
                Wdbg(DebugLevel.E, "Theme not found.")

                'Raise event
                Kernel.KernelEventManager.RaiseThemeSetError(theme, ThemeSetErrorReasons.NotFound)
            End If
        End Sub

        ''' <summary>
        ''' Sets system colors according to the template file
        ''' </summary>
        ''' <param name="ThemeFile">Theme file</param>
        Public Sub ApplyThemeFromFile(ThemeFile As String)
            Try
                Wdbg(DebugLevel.I, "Theme file name: {0}", ThemeFile)
                ThemeFile = NeutralizePath(ThemeFile, True)
                Wdbg(DebugLevel.I, "Theme file path: {0}", ThemeFile)

                'Populate theme info
                Dim ThemeInfo As New ThemeInfo(New StreamReader(ThemeFile))

                If Not ThemeFile = "Default" Then
                    'Set colors as appropriate
                    SetColorsTheme(ThemeInfo)
                End If

                'Raise event
                Kernel.KernelEventManager.RaiseThemeSet(ThemeFile)
            Catch ex As Exception
                Write(DoTranslation("Invalid color template {0}"), True, ColTypes.Error, ThemeFile)
                Wdbg(DebugLevel.E, "Theme not found.")

                'Raise event
                Kernel.KernelEventManager.RaiseThemeSetError(ThemeFile, ThemeSetErrorReasons.NotFound)
            End Try
        End Sub

        ''' <summary>
        ''' Sets custom colors. It only works if colored shell is enabled.
        ''' </summary>
        ''' <param name="ThemeInfo">Theme information</param>
        ''' <returns>True if successful; False if unsuccessful</returns>
        ''' <exception cref="InvalidOperationException"></exception>
        ''' <exception cref="Exceptions.ColorException"></exception>
        Public Function SetColorsTheme(ThemeInfo As ThemeInfo) As Boolean
            If ThemeInfo Is Nothing Then Throw New ArgumentNullException(NameOf(ThemeInfo))

            'Set the colors
            If ColoredShell = True Then
                Try
                    InputColor = ThemeInfo.ThemeInputColor
                    LicenseColor = ThemeInfo.ThemeLicenseColor
                    ContKernelErrorColor = ThemeInfo.ThemeContKernelErrorColor
                    UncontKernelErrorColor = ThemeInfo.ThemeUncontKernelErrorColor
                    HostNameShellColor = ThemeInfo.ThemeHostNameShellColor
                    UserNameShellColor = ThemeInfo.ThemeUserNameShellColor
                    BackgroundColor = ThemeInfo.ThemeBackgroundColor
                    NeutralTextColor = ThemeInfo.ThemeNeutralTextColor
                    ListEntryColor = ThemeInfo.ThemeListEntryColor
                    ListValueColor = ThemeInfo.ThemeListValueColor
                    StageColor = ThemeInfo.ThemeStageColor
                    ErrorColor = ThemeInfo.ThemeErrorColor
                    WarningColor = ThemeInfo.ThemeWarningColor
                    OptionColor = ThemeInfo.ThemeOptionColor
                    BannerColor = ThemeInfo.ThemeBannerColor
                    NotificationTitleColor = ThemeInfo.ThemeNotificationTitleColor
                    NotificationDescriptionColor = ThemeInfo.ThemeNotificationDescriptionColor
                    NotificationProgressColor = ThemeInfo.ThemeNotificationProgressColor
                    NotificationFailureColor = ThemeInfo.ThemeNotificationFailureColor
                    QuestionColor = ThemeInfo.ThemeQuestionColor
                    SuccessColor = ThemeInfo.ThemeSuccessColor
                    UserDollarColor = ThemeInfo.ThemeUserDollarColor
                    TipColor = ThemeInfo.ThemeTipColor
                    SeparatorTextColor = ThemeInfo.ThemeSeparatorTextColor
                    SeparatorColor = ThemeInfo.ThemeSeparatorColor
                    ListTitleColor = ThemeInfo.ThemeListTitleColor
                    DevelopmentWarningColor = ThemeInfo.ThemeDevelopmentWarningColor
                    StageTimeColor = ThemeInfo.ThemeStageTimeColor
                    ColorTools.ProgressColor = ThemeInfo.ThemeProgressColor
                    BackOptionColor = ThemeInfo.ThemeBackOptionColor
                    LowPriorityBorderColor = ThemeInfo.ThemeLowPriorityBorderColor
                    MediumPriorityBorderColor = ThemeInfo.ThemeMediumPriorityBorderColor
                    HighPriorityBorderColor = ThemeInfo.ThemeHighPriorityBorderColor
                    TableSeparatorColor = ThemeInfo.ThemeTableSeparatorColor
                    TableHeaderColor = ThemeInfo.ThemeTableHeaderColor
                    TableValueColor = ThemeInfo.ThemeTableValueColor
                    SelectedOptionColor = ThemeInfo.ThemeSelectedOptionColor
                    LoadBack()
                    MakePermanent()

                    'Raise event
                    Kernel.KernelEventManager.RaiseColorSet()
                    Return True
                Catch ex As Exception
                    WStkTrc(ex)
                    Kernel.KernelEventManager.RaiseColorSetError(ColorSetErrorReasons.InvalidColors)
                    Throw New Exceptions.ColorException(DoTranslation("One or more of the colors is invalid.") + " {0}", ex, ex.Message)
                End Try
            Else
                Kernel.KernelEventManager.RaiseColorSetError(ColorSetErrorReasons.NoColors)
                Throw New InvalidOperationException(DoTranslation("Colors are not available. Turn on colored shell in the kernel config."))
            End If
            Return False
        End Function

    End Module
End Namespace