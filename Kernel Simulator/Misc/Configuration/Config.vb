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

Imports System.Globalization
Imports System.IO
Imports MimeKit.Text
Imports Newtonsoft.Json.Linq

Public Module Config

    ''' <summary>
    ''' Base config token to be loaded each kernel startup.
    ''' </summary>
    Friend ConfigToken As JObject

    ''' <summary>
    ''' Config category enumeration
    ''' </summary>
    Public Enum ConfigCategory
        ''' <summary>
        ''' All general kernel settings, mainly for maintaining the kernel.
        ''' </summary>
        General
        ''' <summary>
        ''' Color settings
        ''' </summary>
        Colors
        ''' <summary>
        ''' Hardware settings
        ''' </summary>
        Hardware
        ''' <summary>
        ''' Login settings
        ''' </summary>
        Login
        ''' <summary>
        ''' Shell settings
        ''' </summary>
        Shell
        ''' <summary>
        ''' Filesystem settings
        ''' </summary>
        Filesystem
        ''' <summary>
        ''' Network settings
        ''' </summary>
        Network
        ''' <summary>
        ''' Screensaver settings
        ''' </summary>
        Screensaver
        ''' <summary>
        ''' Miscellaneous settings
        ''' </summary>
        Misc
    End Enum

    ''' <summary>
    ''' Creates the kernel configuration file
    ''' </summary>
    ''' <returns>True if successful; False if unsuccessful.</returns>
    ''' <exception cref="Exceptions.ConfigException"></exception>
    Public Function CreateConfig() As Boolean
        Try
            Dim ConfigurationObject As New JObject

            'The General Section
            Dim GeneralConfig As New JObject From {
                    {"Prompt for Arguments on Boot", ArgsOnBoot},
                    {"Maintenance Mode", Maintenance},
                    {"Change Root Password", SetRootPassword},
                    {"Set Root Password to", RootPassword},
                    {"Check for Updates on Startup", CheckUpdateStart},
                    {"Custom Startup Banner", CustomBanner},
                    {"Change Culture when Switching Languages", LangChangeCulture},
                    {"Language", CurrentLanguage},
                    {"Culture", CurrentCult.Name},
                    {"Show app information during boot", ShowAppInfoOnBoot},
                    {"Parse command-line arguments", ParseCommandLineArguments},
                    {"Show stage finish times", ShowStageFinishTimes},
                    {"Start kernel modifications on boot", StartKernelMods},
                    {"Show current time before login", ShowCurrentTimeBeforeLogin},
                    {"Notify for any fault during boot", NotifyFaultsBoot}
            }
            ConfigurationObject.Add("General", GeneralConfig)

            'The Colors Section
            Dim ColorConfig As New JObject From {
                    {"User Name Shell Color", If(New Color(UserNameShellColor).Type = ColorType.TrueColor, UserNameShellColor.EncloseByDoubleQuotes, UserNameShellColor)},
                    {"Host Name Shell Color", If(New Color(HostNameShellColor).Type = ColorType.TrueColor, HostNameShellColor.EncloseByDoubleQuotes, HostNameShellColor)},
                    {"Continuable Kernel Error Color", If(New Color(ContKernelErrorColor).Type = ColorType.TrueColor, ContKernelErrorColor.EncloseByDoubleQuotes, ContKernelErrorColor)},
                    {"Uncontinuable Kernel Error Color", If(New Color(UncontKernelErrorColor).Type = ColorType.TrueColor, UncontKernelErrorColor.EncloseByDoubleQuotes, UncontKernelErrorColor)},
                    {"Text Color", If(New Color(NeutralTextColor).Type = ColorType.TrueColor, NeutralTextColor.EncloseByDoubleQuotes, NeutralTextColor)},
                    {"License Color", If(New Color(LicenseColor).Type = ColorType.TrueColor, LicenseColor.EncloseByDoubleQuotes, LicenseColor)},
                    {"Background Color", If(New Color(BackgroundColor).Type = ColorType.TrueColor, BackgroundColor.EncloseByDoubleQuotes, BackgroundColor)},
                    {"Input Color", If(New Color(InputColor).Type = ColorType.TrueColor, InputColor.EncloseByDoubleQuotes, InputColor)},
                    {"List Entry Color", If(New Color(ListEntryColor).Type = ColorType.TrueColor, ListEntryColor.EncloseByDoubleQuotes, ListEntryColor)},
                    {"List Value Color", If(New Color(ListValueColor).Type = ColorType.TrueColor, ListValueColor.EncloseByDoubleQuotes, ListValueColor)},
                    {"Kernel Stage Color", If(New Color(StageColor).Type = ColorType.TrueColor, StageColor.EncloseByDoubleQuotes, StageColor)},
                    {"Error Text Color", If(New Color(ErrorColor).Type = ColorType.TrueColor, ErrorColor.EncloseByDoubleQuotes, ErrorColor)},
                    {"Warning Text Color", If(New Color(WarningColor).Type = ColorType.TrueColor, WarningColor.EncloseByDoubleQuotes, WarningColor)},
                    {"Option Color", If(New Color(OptionColor).Type = ColorType.TrueColor, OptionColor.EncloseByDoubleQuotes, OptionColor)},
                    {"Banner Color", If(New Color(BannerColor).Type = ColorType.TrueColor, BannerColor.EncloseByDoubleQuotes, BannerColor)},
                    {"Notification Title Color", If(New Color(NotificationTitleColor).Type = ColorType.TrueColor, NotificationTitleColor.EncloseByDoubleQuotes, NotificationTitleColor)},
                    {"Notification Description Color", If(New Color(NotificationDescriptionColor).Type = ColorType.TrueColor, NotificationDescriptionColor.EncloseByDoubleQuotes, NotificationDescriptionColor)},
                    {"Notification Progress Color", If(New Color(NotificationProgressColor).Type = ColorType.TrueColor, NotificationProgressColor.EncloseByDoubleQuotes, NotificationProgressColor)},
                    {"Notification Failure Color", If(New Color(NotificationFailureColor).Type = ColorType.TrueColor, NotificationFailureColor.EncloseByDoubleQuotes, NotificationFailureColor)},
                    {"Question Color", If(New Color(QuestionColor).Type = ColorType.TrueColor, QuestionColor.EncloseByDoubleQuotes, QuestionColor)},
                    {"Success Color", If(New Color(SuccessColor).Type = ColorType.TrueColor, SuccessColor.EncloseByDoubleQuotes, SuccessColor)},
                    {"User Dollar Color", If(New Color(UserDollarColor).Type = ColorType.TrueColor, UserDollarColor.EncloseByDoubleQuotes, UserDollarColor)},
                    {"Tip Color", If(New Color(TipColor).Type = ColorType.TrueColor, TipColor.EncloseByDoubleQuotes, TipColor)},
                    {"Separator Text Color", If(New Color(SeparatorTextColor).Type = ColorType.TrueColor, SeparatorTextColor.EncloseByDoubleQuotes, SeparatorTextColor)},
                    {"Separator Color", If(New Color(SeparatorColor).Type = ColorType.TrueColor, SeparatorColor.EncloseByDoubleQuotes, SeparatorColor)},
                    {"List Title Color", If(New Color(ListTitleColor).Type = ColorType.TrueColor, ListTitleColor.EncloseByDoubleQuotes, ListTitleColor)},
                    {"Development Warning Color", If(New Color(DevelopmentWarningColor).Type = ColorType.TrueColor, DevelopmentWarningColor.EncloseByDoubleQuotes, DevelopmentWarningColor)},
                    {"Stage Time Color", If(New Color(StageTimeColor).Type = ColorType.TrueColor, StageTimeColor.EncloseByDoubleQuotes, StageTimeColor)},
                    {"Progress Color", If(New Color(ProgressColor).Type = ColorType.TrueColor, ProgressColor.EncloseByDoubleQuotes, ProgressColor)},
                    {"Back Option Color", If(New Color(BackOptionColor).Type = ColorType.TrueColor, BackOptionColor.EncloseByDoubleQuotes, BackOptionColor)},
                    {"Low Priority Border Color", If(New Color(LowPriorityBorderColor).Type = ColorType.TrueColor, LowPriorityBorderColor.EncloseByDoubleQuotes, LowPriorityBorderColor)},
                    {"Medium Priority Border Color", If(New Color(MediumPriorityBorderColor).Type = ColorType.TrueColor, MediumPriorityBorderColor.EncloseByDoubleQuotes, MediumPriorityBorderColor)},
                    {"High Priority Border Color", If(New Color(HighPriorityBorderColor).Type = ColorType.TrueColor, HighPriorityBorderColor.EncloseByDoubleQuotes, HighPriorityBorderColor)}
            }
            ConfigurationObject.Add("Colors", ColorConfig)

            'The Hardware Section
            Dim HardwareConfig As New JObject From {
                    {"Quiet Probe", QuietHardwareProbe},
                    {"Full Probe", FullHardwareProbe},
                    {"Verbose Probe", VerboseHardwareProbe}
            }
            ConfigurationObject.Add("Hardware", HardwareConfig)

            'The Login Section
            Dim LoginConfig As New JObject From {
                    {"Show MOTD on Log-in", ShowMOTD},
                    {"Clear Screen on Log-in", ClearOnLogin},
                    {"Host Name", HostName},
                    {"Show available usernames", ShowAvailableUsers},
                    {"MOTD Path", MOTDFilePath},
                    {"MAL Path", MALFilePath},
                    {"Username prompt style", UsernamePrompt},
                    {"Password prompt style", PasswordPrompt},
                    {"Show MAL on Log-in", ShowMAL}
            }
            ConfigurationObject.Add("Login", LoginConfig)

            'The Shell Section
            Dim ShellConfig As New JObject From {
                    {"Colored Shell", ColoredShell},
                    {"Simplified Help Command", SimHelp},
                    {"Current Directory", CurrDir},
                    {"Lookup Directories", PathsToLookup.EncloseByDoubleQuotes},
                    {"Prompt Style", ShellPromptStyle},
                    {"FTP Prompt Style", FTPShellPromptStyle},
                    {"Mail Prompt Style", MailShellPromptStyle},
                    {"SFTP Prompt Style", SFTPShellPromptStyle},
                    {"RSS Prompt Style", RSSShellPromptStyle},
                    {"Text Edit Prompt Style", TextEdit_PromptStyle},
                    {"Zip Shell Prompt Style", ZipShell_PromptStyle},
                    {"Test Shell Prompt Style", Test_PromptStyle},
                    {"JSON Shell Prompt Style", JsonShell_PromptStyle},
                    {"Probe injected commands", ProbeInjectedCommands}
            }
            ConfigurationObject.Add("Shell", ShellConfig)

            'The Filesystem Section
            Dim FilesystemConfig As New JObject From {
                    {"Filesystem sort mode", SortMode.ToString},
                    {"Filesystem sort direction", SortDirection.ToString},
                    {"Debug Size Quota in Bytes", DebugQuota},
                    {"Show Hidden Files", HiddenFiles},
                    {"Size parse mode", FullParseMode},
                    {"Show progress on filesystem operations", ShowFilesystemProgress},
                    {"Show file details in list", ShowFileDetailsList}
            }
            ConfigurationObject.Add("Filesystem", FilesystemConfig)

            'The Network Section
            Dim NetworkConfig As New JObject From {
                    {"Debug Port", DebugPort},
                    {"Download Retry Times", DownloadRetries},
                    {"Upload Retry Times", UploadRetries},
                    {"Show progress bar while downloading or uploading from ""get"" or ""put"" command", ShowProgress},
                    {"Log FTP username", FTPLoggerUsername},
                    {"Log FTP IP address", FTPLoggerIP},
                    {"Return only first FTP profile", FTPFirstProfileOnly},
                    {"Show mail message preview", ShowPreview},
                    {"Record chat to debug log", RecordChatToDebugLog},
                    {"Show SSH banner", SSHBanner},
                    {"Enable RPC", RPCEnabled},
                    {"RPC Port", RPCPort},
                    {"Show file details in FTP list", FtpShowDetailsInList},
                    {"Username prompt style for FTP", FtpUserPromptStyle},
                    {"Password prompt style for FTP", FtpPassPromptStyle},
                    {"Use first FTP profile", FtpUseFirstProfile},
                    {"Add new connections to FTP speed dial", FtpNewConnectionsToSpeedDial},
                    {"Try to validate secure FTP certificates", FtpTryToValidateCertificate},
                    {"Show FTP MOTD on connection", FtpShowMotd},
                    {"Always accept invalid FTP certificates", FtpAlwaysAcceptInvalidCerts},
                    {"Username prompt style for mail", Mail_UserPromptStyle},
                    {"Password prompt style for mail", Mail_PassPromptStyle},
                    {"IMAP prompt style for mail", Mail_IMAPPromptStyle},
                    {"SMTP prompt style for mail", Mail_SMTPPromptStyle},
                    {"Automatically detect mail server", Mail_AutoDetectServer},
                    {"Enable mail debug", Mail_Debug},
                    {"Notify for new mail messages", Mail_NotifyNewMail},
                    {"GPG password prompt style for mail", Mail_GPGPromptStyle},
                    {"Send IMAP ping interval", Mail_ImapPingInterval},
                    {"Send SMTP ping interval", Mail_SmtpPingInterval},
                    {"Mail text format", Mail_TextFormat.ToString},
                    {"Automatically start remote debug on startup", RDebugAutoStart},
                    {"Remote debug message format", RDebugMessageFormat},
                    {"RSS feed URL prompt style", RSSFeedUrlPromptStyle},
                    {"Auto refresh RSS feed", RSSRefreshFeeds},
                    {"Auto refresh RSS feed interval", RSSRefreshInterval},
                    {"Show file details in SFTP list", SFTPShowDetailsInList},
                    {"Username prompt style for SFTP", SFTPUserPromptStyle},
                    {"Add new connections to SFTP speed dial", SFTPNewConnectionsToSpeedDial},
                    {"Ping timeout", PingTimeout},
                    {"Show extensive adapter info", ExtensiveAdapterInformation},
                    {"Show general network information", GeneralNetworkInformation},
                    {"Download percentage text", DownloadPercentagePrint},
                    {"Upload percentage text", UploadPercentagePrint}
            }
            ConfigurationObject.Add("Network", NetworkConfig)

            'The Screensaver Section
            Dim ScreensaverConfig As New JObject From {
                    {"Screensaver", DefSaverName},
                    {"Screensaver Timeout in ms", ScrnTimeout},
                    {"Enable screensaver debugging", ScreensaverDebug},
                    {"Ask for password after locking", PasswordLock}
            }

            'Disco config json object
            Dim ColorMixConfig As New JObject From {
                    {"Activate 255 Color Mode", ColorMix255Colors},
                    {"Activate True Color Mode", ColorMixTrueColor},
                    {"Delay in Milliseconds", ColorMixDelay},
                    {"Background color", If(New Color(ColorMixBackgroundColor).Type = ColorType.TrueColor, ColorMixBackgroundColor.EncloseByDoubleQuotes, ColorMixBackgroundColor)},
                    {"Minimum red color level", ColorMixMinimumRedColorLevel},
                    {"Minimum green color level", ColorMixMinimumGreenColorLevel},
                    {"Minimum blue color level", ColorMixMinimumBlueColorLevel},
                    {"Minimum color level", ColorMixMinimumColorLevel},
                    {"Maximum red color level", ColorMixMaximumRedColorLevel},
                    {"Maximum green color level", ColorMixMaximumGreenColorLevel},
                    {"Maximum blue color level", ColorMixMaximumBlueColorLevel},
                    {"Maximum color level", ColorMixMaximumColorLevel}
            }
            ScreensaverConfig.Add("ColorMix", ColorMixConfig)

            'Disco config json object
            Dim DiscoConfig As New JObject From {
                    {"Activate 255 Color Mode", Disco255Colors},
                    {"Activate True Color Mode", DiscoTrueColor},
                    {"Delay in Milliseconds", DiscoDelay},
                    {"Use Beats Per Minute", DiscoUseBeatsPerMinute},
                    {"Cycle Colors", DiscoCycleColors},
                    {"Minimum red color level", DiscoMinimumRedColorLevel},
                    {"Minimum green color level", DiscoMinimumGreenColorLevel},
                    {"Minimum blue color level", DiscoMinimumBlueColorLevel},
                    {"Minimum color level", DiscoMinimumColorLevel},
                    {"Maximum red color level", DiscoMaximumRedColorLevel},
                    {"Maximum green color level", DiscoMaximumGreenColorLevel},
                    {"Maximum blue color level", DiscoMaximumBlueColorLevel},
                    {"Maximum color level", DiscoMaximumColorLevel}
            }
            ScreensaverConfig.Add("Disco", DiscoConfig)

            'GlitterColor config json object
            Dim GlitterColorConfig As New JObject From {
                    {"Activate 255 Color Mode", GlitterColor255Colors},
                    {"Activate True Color Mode", GlitterColorTrueColor},
                    {"Delay in Milliseconds", GlitterColorDelay},
                    {"Minimum red color level", GlitterColorMinimumRedColorLevel},
                    {"Minimum green color level", GlitterColorMinimumGreenColorLevel},
                    {"Minimum blue color level", GlitterColorMinimumBlueColorLevel},
                    {"Minimum color level", GlitterColorMinimumColorLevel},
                    {"Maximum red color level", GlitterColorMaximumRedColorLevel},
                    {"Maximum green color level", GlitterColorMaximumGreenColorLevel},
                    {"Maximum blue color level", GlitterColorMaximumBlueColorLevel},
                    {"Maximum color level", GlitterColorMaximumColorLevel}
            }
            ScreensaverConfig.Add("GlitterColor", GlitterColorConfig)

            'Lines config json object
            Dim LinesConfig As New JObject From {
                    {"Activate 255 Color Mode", Lines255Colors},
                    {"Activate True Color Mode", LinesTrueColor},
                    {"Delay in Milliseconds", LinesDelay},
                    {"Line character", LinesLineChar},
                    {"Background color", If(New Color(LinesBackgroundColor).Type = ColorType.TrueColor, LinesBackgroundColor.EncloseByDoubleQuotes, LinesBackgroundColor)},
                    {"Minimum red color level", LinesMinimumRedColorLevel},
                    {"Minimum green color level", LinesMinimumGreenColorLevel},
                    {"Minimum blue color level", LinesMinimumBlueColorLevel},
                    {"Minimum color level", LinesMinimumColorLevel},
                    {"Maximum red color level", LinesMaximumRedColorLevel},
                    {"Maximum green color level", LinesMaximumGreenColorLevel},
                    {"Maximum blue color level", LinesMaximumBlueColorLevel},
                    {"Maximum color level", LinesMaximumColorLevel}
            }
            ScreensaverConfig.Add("Lines", LinesConfig)

            'Dissolve config json object
            Dim DissolveConfig As New JObject From {
                    {"Activate 255 Color Mode", Dissolve255Colors},
                    {"Activate True Color Mode", DissolveTrueColor},
                    {"Background color", If(New Color(DissolveBackgroundColor).Type = ColorType.TrueColor, DissolveBackgroundColor.EncloseByDoubleQuotes, DissolveBackgroundColor)},
                    {"Minimum red color level", DissolveMinimumRedColorLevel},
                    {"Minimum green color level", DissolveMinimumGreenColorLevel},
                    {"Minimum blue color level", DissolveMinimumBlueColorLevel},
                    {"Minimum color level", DissolveMinimumColorLevel},
                    {"Maximum red color level", DissolveMaximumRedColorLevel},
                    {"Maximum green color level", DissolveMaximumGreenColorLevel},
                    {"Maximum blue color level", DissolveMaximumBlueColorLevel},
                    {"Maximum color level", DissolveMaximumColorLevel}
            }
            ScreensaverConfig.Add("Dissolve", DissolveConfig)

            'BouncingBlock config json object
            Dim BouncingBlockConfig As New JObject From {
                    {"Activate 255 Color Mode", BouncingBlock255Colors},
                    {"Activate True Color Mode", BouncingBlockTrueColor},
                    {"Delay in Milliseconds", BouncingBlockDelay}
            }
            ScreensaverConfig.Add("BouncingBlock", BouncingBlockConfig)

            'ProgressClock config json object
            Dim ProgressClockConfig As New JObject From {
                    {"Activate 255 Color Mode", ProgressClock255Colors},
                    {"Activate True Color Mode", ProgressClockTrueColor},
                    {"Cycle Colors", ProgressClockCycleColors},
                    {"Ticks to change color", ProgressClockCycleColorsTicks},
                    {"Color of Seconds Bar", ProgressClockSecondsProgressColor},
                    {"Color of Minutes Bar", ProgressClockMinutesProgressColor},
                    {"Color of Hours Bar", ProgressClockHoursProgressColor},
                    {"Color of Information", ProgressClockProgressColor}
            }
            ScreensaverConfig.Add("ProgressClock", ProgressClockConfig)

            'Lighter config json object
            Dim LighterConfig As New JObject From {
                    {"Activate 255 Color Mode", Lighter255Colors},
                    {"Activate True Color Mode", LighterTrueColor},
                    {"Delay in Milliseconds", LighterDelay},
                    {"Max Positions Count", LighterMaxPositions}
            }
            ScreensaverConfig.Add("Lighter", LighterConfig)

            'Wipe config json object
            Dim WipeConfig As New JObject From {
                    {"Activate 255 Color Mode", Wipe255Colors},
                    {"Activate True Color Mode", WipeTrueColor},
                    {"Delay in Milliseconds", WipeDelay},
                    {"Wipes to change direction", WipeWipesNeededToChangeDirection}
            }
            ScreensaverConfig.Add("Wipe", WipeConfig)

            'Matrix config json object
            Dim MatrixConfig As New JObject From {
                    {"Delay in Milliseconds", MatrixDelay}
            }
            ScreensaverConfig.Add("Matrix", MatrixConfig)

            'GlitterMatrix config json object
            Dim GlitterMatrixConfig As New JObject From {
                    {"Delay in Milliseconds", GlitterMatrixDelay},
                    {"Background color", If(New Color(GlitterMatrixBackgroundColor).Type = ColorType.TrueColor, GlitterMatrixBackgroundColor.EncloseByDoubleQuotes, GlitterMatrixBackgroundColor)},
                    {"Foreground color", If(New Color(GlitterMatrixForegroundColor).Type = ColorType.TrueColor, GlitterMatrixForegroundColor.EncloseByDoubleQuotes, GlitterMatrixForegroundColor)}
            }
            ScreensaverConfig.Add("GlitterMatrix", GlitterMatrixConfig)

            'BouncingText config json object
            Dim BouncingTextConfig As New JObject From {
                    {"Activate 255 Color Mode", BouncingText255Colors},
                    {"Activate True Color Mode", BouncingTextTrueColor},
                    {"Delay in Milliseconds", BouncingTextDelay},
                    {"Text Shown", BouncingTextWrite}
            }
            ScreensaverConfig.Add("BouncingText", BouncingTextConfig)

            'Fader config json object
            Dim FaderConfig As New JObject From {
                    {"Delay in Milliseconds", FaderDelay},
                    {"Fade Out Delay in Milliseconds", FaderFadeOutDelay},
                    {"Text Shown", FaderWrite},
                    {"Max Fade Steps", FaderMaxSteps}
            }
            ScreensaverConfig.Add("Fader", FaderConfig)

            'FaderBack config json object
            Dim FaderBackConfig As New JObject From {
                    {"Delay in Milliseconds", FaderBackDelay},
                    {"Fade Out Delay in Milliseconds", FaderBackFadeOutDelay},
                    {"Max Fade Steps", FaderBackMaxSteps}
            }
            ScreensaverConfig.Add("FaderBack", FaderBackConfig)

            'BeatFader config json object
            Dim BeatFaderConfig As New JObject From {
                    {"Activate 255 Color Mode", BeatFader255Colors},
                    {"Activate True Color Mode", BeatFaderTrueColor},
                    {"Delay in Beats Per Minute", BeatFaderDelay},
                    {"Cycle Colors", BeatFaderCycleColors},
                    {"Beat Color", BeatFaderBeatColor},
                    {"Max Fade Steps", BeatFaderMaxSteps}
            }
            ScreensaverConfig.Add("BeatFader", BeatFaderConfig)

            'Typo config json object
            Dim TypoConfig As New JObject From {
                    {"Delay in Milliseconds", TypoDelay},
                    {"Write Again Delay in Milliseconds", TypoWriteAgainDelay},
                    {"Text Shown", TypoWrite},
                    {"Minimum writing speed in WPM", TypoWritingSpeedMin},
                    {"Maximum writing speed in WPM", TypoWritingSpeedMax},
                    {"Probability of typo in percent", TypoMissStrikePossibility},
                    {"Probability of miss in percent", TypoMissPossibility}
            }
            ScreensaverConfig.Add("Typo", TypoConfig)

            'Marquee config json object
            Dim MarqueeConfig As New JObject From {
                    {"Activate 255 Color Mode", Marquee255Colors},
                    {"Activate True Color Mode", MarqueeTrueColor},
                    {"Delay in Milliseconds", MarqueeDelay},
                    {"Text Shown", MarqueeWrite},
                    {"Always Centered", MarqueeAlwaysCentered},
                    {"Use Console API", MarqueeUseConsoleAPI}
            }
            ScreensaverConfig.Add("Marquee", MarqueeConfig)

            'Linotypo config json object
            Dim LinotypoConfig As New JObject From {
                    {"Delay in Milliseconds", LinotypoDelay},
                    {"New Screen Delay in Milliseconds", LinotypoNewScreenDelay},
                    {"Text Shown", LinotypoWrite},
                    {"Minimum writing speed in WPM", LinotypoWritingSpeedMin},
                    {"Maximum writing speed in WPM", LinotypoWritingSpeedMax},
                    {"Probability of typo in percent", LinotypoMissStrikePossibility},
                    {"Column Count", LinotypoTextColumns},
                    {"Line Fill Threshold", LinotypoEtaoinThreshold},
                    {"Line Fill Capping Probability in percent", LinotypoEtaoinCappingPossibility},
                    {"Line Fill Type", LinotypoEtaoinType},
                    {"Probability of miss in percent", LinotypoMissPossibility}
            }
            ScreensaverConfig.Add("Linotypo", LinotypoConfig)

            'Typewriter config json object
            Dim TypewriterConfig As New JObject From {
                    {"Delay in Milliseconds", TypewriterDelay},
                    {"New Screen Delay in Milliseconds", TypewriterNewScreenDelay},
                    {"Text Shown", TypewriterWrite},
                    {"Minimum writing speed in WPM", TypewriterWritingSpeedMin},
                    {"Maximum writing speed in WPM", TypewriterWritingSpeedMax}
            }
            ScreensaverConfig.Add("Typewriter", TypewriterConfig)

            'FlashColor config json object
            Dim FlashColorConfig As New JObject From {
                    {"Activate 255 Color Mode", FlashColor255Colors},
                    {"Activate True Color Mode", FlashColorTrueColor},
                    {"Delay in Milliseconds", FlashColorDelay}
            }
            ScreensaverConfig.Add("FlashColor", FlashColorConfig)

            'SpotWrite config json object
            Dim SpotWriteConfig As New JObject From {
                    {"Delay in Milliseconds", SpotWriteDelay},
                    {"New Screen Delay in Milliseconds", SpotWriteNewScreenDelay},
                    {"Text Shown", SpotWriteWrite}
            }
            ScreensaverConfig.Add("SpotWrite", SpotWriteConfig)

            'Ramp config json object
            Dim RampConfig As New JObject From {
                    {"Activate 255 Color Mode", Ramp255Colors},
                    {"Activate True Color Mode", RampTrueColor},
                    {"Delay in Milliseconds", RampDelay},
                    {"Next ramp interval", RampDelay}
            }
            ScreensaverConfig.Add("Ramp", RampConfig)

            'Add a screensaver config json object to Screensaver section
            ConfigurationObject.Add("Screensaver", ScreensaverConfig)

            'Misc Section
            Dim MiscConfig As New JObject From {
                    {"Show Time/Date on Upper Right Corner", CornerTimeDate},
                    {"Marquee on startup", StartScroll},
                    {"Long Time and Date", LongTimeDate},
                    {"Preferred Unit for Temperature", PreferredUnit},
                    {"Enable text editor autosave", TextEdit_AutoSaveFlag},
                    {"Text editor autosave interval", TextEdit_AutoSaveInterval},
                    {"Wrap list outputs", WrapListOutputs},
                    {"Draw notification border", DrawBorderNotification},
                    {"Blacklisted mods", BlacklistedModsString}
            }
            ConfigurationObject.Add("Misc", MiscConfig)

            'Save Config
            File.WriteAllText(GetKernelPath(KernelPathType.Configuration), JsonConvert.SerializeObject(ConfigurationObject, Formatting.Indented))
            EventManager.RaiseConfigSaved()
            Return True
        Catch ex As Exception
            EventManager.RaiseConfigSaveError(ex)
            If DebugMode = True Then
                WStkTrc(ex)
                Throw New Exceptions.ConfigException(DoTranslation("There is an error trying to create configuration: {0}."), ex, ex.Message)
            Else
                Throw New Exceptions.ConfigException(DoTranslation("There is an error trying to create configuration."), ex)
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Configures the kernel according to the kernel configuration file
    ''' </summary>
    ''' <returns>True if successful; False if unsuccessful</returns>
    ''' <exception cref="Exceptions.ConfigException"></exception>
    Public Function ReadConfig() As Boolean
        Try
            'Parse configuration. NOTE: Question marks between parentheses are for nullable types.
            InitializeConfigToken()
            Wdbg(DebugLevel.I, "Config loaded with {0} sections", ConfigToken.Count)

            '----------------------------- Important configuration -----------------------------
            'Language
            LangChangeCulture = If(ConfigToken("General")?("Change Culture when Switching Languages"), False)
            If LangChangeCulture Then CurrentCult = New CultureInfo(If(ConfigToken("General")?("Culture") IsNot Nothing, ConfigToken("General")("Culture").ToString, "en-US"))
            SetLang(If(ConfigToken("General")?("Language"), "eng"))

            'Colored Shell
            Dim UncoloredDetected As Boolean = ConfigToken("Shell")?("Colored Shell") IsNot Nothing AndAlso Not ConfigToken("Shell")("Colored Shell").ToObject(Of Boolean)
            If UncoloredDetected Then
                Wdbg(DebugLevel.W, "Detected uncolored shell. Removing colors...")
                ApplyThemeFromResources("LinuxUncolored")
                ColoredShell = False
            End If

            '----------------------------- General configuration -----------------------------
            'Colors Section
            Wdbg(DebugLevel.I, "Loading colors...")
            If ColoredShell Then
                'We use New Color() to parse entered color. This is to ensure that the kernel can use the correct VT sequence.
                UserNameShellColor = New Color(If(ConfigToken("Colors")?("User Name Shell Color"), ConsoleColors.Green)).PlainSequence
                HostNameShellColor = New Color(If(ConfigToken("Colors")?("Host Name Shell Color"), ConsoleColors.DarkGreen)).PlainSequence
                ContKernelErrorColor = New Color(If(ConfigToken("Colors")?("Continuable Kernel Error Color"), ConsoleColors.Yellow)).PlainSequence
                UncontKernelErrorColor = New Color(If(ConfigToken("Colors")?("Uncontinuable Kernel Error Color"), ConsoleColors.Red)).PlainSequence
                NeutralTextColor = New Color(If(ConfigToken("Colors")?("Text Color"), ConsoleColors.Gray)).PlainSequence
                LicenseColor = New Color(If(ConfigToken("Colors")?("License Color"), ConsoleColors.White)).PlainSequence
                BackgroundColor = New Color(If(ConfigToken("Colors")?("Background Color"), ConsoleColors.Black)).PlainSequence
                InputColor = New Color(If(ConfigToken("Colors")?("Input Color"), ConsoleColors.White)).PlainSequence
                ListEntryColor = New Color(If(ConfigToken("Colors")?("List Entry Color"), ConsoleColors.DarkYellow)).PlainSequence
                ListValueColor = New Color(If(ConfigToken("Colors")?("List Value Color"), ConsoleColors.DarkGray)).PlainSequence
                StageColor = New Color(If(ConfigToken("Colors")?("Kernel Stage Color"), ConsoleColors.Green)).PlainSequence
                ErrorColor = New Color(If(ConfigToken("Colors")?("Error Text Color"), ConsoleColors.Red)).PlainSequence
                WarningColor = New Color(If(ConfigToken("Colors")?("Warning Text Color"), ConsoleColors.Yellow)).PlainSequence
                OptionColor = New Color(If(ConfigToken("Colors")?("Option Color"), ConsoleColors.DarkYellow)).PlainSequence
                BannerColor = New Color(If(ConfigToken("Colors")?("Banner Color"), ConsoleColors.Green)).PlainSequence
                NotificationTitleColor = New Color(If(ConfigToken("Colors")?("Notification Title Color"), ConsoleColors.White)).PlainSequence
                NotificationDescriptionColor = New Color(If(ConfigToken("Colors")?("Notification Description Color"), ConsoleColors.Gray)).PlainSequence
                NotificationProgressColor = New Color(If(ConfigToken("Colors")?("Notification Progress Color"), ConsoleColors.DarkYellow)).PlainSequence
                NotificationFailureColor = New Color(If(ConfigToken("Colors")?("Notification Failure Color"), ConsoleColors.Red)).PlainSequence
                QuestionColor = New Color(If(ConfigToken("Colors")?("Question Color"), ConsoleColors.Yellow)).PlainSequence
                SuccessColor = New Color(If(ConfigToken("Colors")?("Success Color"), ConsoleColors.Green)).PlainSequence
                UserDollarColor = New Color(If(ConfigToken("Colors")?("User Dollar Color"), ConsoleColors.Gray)).PlainSequence
                TipColor = New Color(If(ConfigToken("Colors")?("Tip Color"), ConsoleColors.Gray)).PlainSequence
                SeparatorTextColor = New Color(If(ConfigToken("Colors")?("Separator Text Color"), ConsoleColors.White)).PlainSequence
                SeparatorColor = New Color(If(ConfigToken("Colors")?("Separator Color"), ConsoleColors.Gray)).PlainSequence
                ListTitleColor = New Color(If(ConfigToken("Colors")?("List Title Color"), ConsoleColors.White)).PlainSequence
                DevelopmentWarningColor = New Color(If(ConfigToken("Colors")?("Development Warning Color"), ConsoleColors.Yellow)).PlainSequence
                StageTimeColor = New Color(If(ConfigToken("Colors")?("Stage Time Color"), ConsoleColors.Gray)).PlainSequence
                ProgressColor = New Color(If(ConfigToken("Colors")?("Progress Color"), ConsoleColors.DarkYellow)).PlainSequence
                BackOptionColor = New Color(If(ConfigToken("Colors")?("Back Option Color"), ConsoleColors.DarkRed)).PlainSequence
                LowPriorityBorderColor = New Color(If(ConfigToken("Colors")?("Low Priority Border Color"), ConsoleColors.White)).PlainSequence
                MediumPriorityBorderColor = New Color(If(ConfigToken("Colors")?("Medium Priority Border Color"), ConsoleColors.Yellow)).PlainSequence
                HighPriorityBorderColor = New Color(If(ConfigToken("Colors")?("High Priority Border Color"), ConsoleColors.Red)).PlainSequence
                LoadBack()
            End If

            'General Section
            Wdbg(DebugLevel.I, "Parsing general section...")
            SetRootPassword = If(ConfigToken("General")?("Change Root Password"), False)
            If SetRootPassword = True Then RootPassword = ConfigToken("General")?("Set Root Password to")
            Maintenance = If(ConfigToken("General")?("Maintenance Mode"), False)
            ArgsOnBoot = If(ConfigToken("General")?("Prompt for Arguments on Boot"), False)
            CheckUpdateStart = If(ConfigToken("General")?("Check for Updates on Startup"), True)
            If Not String.IsNullOrWhiteSpace(ConfigToken("General")?("Custom Startup Banner")) Then CustomBanner = ConfigToken("General")?("Custom Startup Banner")
            ShowAppInfoOnBoot = If(ConfigToken("General")?("Show app information during boot"), True)
            ParseCommandLineArguments = If(ConfigToken("General")?("Parse command-line arguments"), True)
            ShowStageFinishTimes = If(ConfigToken("General")?("Show stage finish times"), False)
            StartKernelMods = If(ConfigToken("General")?("Start kernel modifications on boot"), True)
            ShowCurrentTimeBeforeLogin = If(ConfigToken("General")?("Show current time before login"), True)
            NotifyFaultsBoot = If(ConfigToken("General")?("Notify for any fault during boot"), True)

            'Login Section
            Wdbg(DebugLevel.I, "Parsing login section...")
            ClearOnLogin = If(ConfigToken("Login")?("Clear Screen on Log-in"), False)
            ShowMOTD = If(ConfigToken("Login")?("Show MOTD on Log-in"), True)
            ShowAvailableUsers = If(ConfigToken("Login")?("Show available usernames"), True)
            If Not String.IsNullOrWhiteSpace(ConfigToken("Login")?("Host Name")) Then HostName = ConfigToken("Login")?("Host Name")
            If Not String.IsNullOrWhiteSpace(ConfigToken("Login")?("MOTD Path")) And TryParsePath(ConfigToken("Login")?("MOTD Path")) Then MOTDFilePath = ConfigToken("Login")?("MOTD Path")
            If Not String.IsNullOrWhiteSpace(ConfigToken("Login")?("MAL Path")) And TryParsePath(ConfigToken("Login")?("MAL Path")) Then MALFilePath = ConfigToken("Login")?("MAL Path")
            UsernamePrompt = If(ConfigToken("Login")?("Username prompt style"), "")
            PasswordPrompt = If(ConfigToken("Login")?("Password prompt style"), "")
            ShowMAL = If(ConfigToken("Login")?("Show MAL on Log-in"), True)

            'Shell Section
            Wdbg(DebugLevel.I, "Parsing shell section...")
            SimHelp = If(ConfigToken("Shell")?("Simplified Help Command"), False)
            CurrDir = If(ConfigToken("Shell")?("Current Directory"), GetOtherPath(OtherPathType.Home))
            PathsToLookup = If(Not String.IsNullOrEmpty(ConfigToken("Shell")?("Lookup Directories")), ConfigToken("Shell")?("Lookup Directories").ToString.ReleaseDoubleQuotes, Environ("PATH"))
            ShellPromptStyle = If(ConfigToken("Shell")?("Prompt Style"), "")
            FTPShellPromptStyle = If(ConfigToken("Shell")?("FTP Prompt Style"), "")
            MailShellPromptStyle = If(ConfigToken("Shell")?("Mail Prompt Style"), "")
            SFTPShellPromptStyle = If(ConfigToken("Shell")?("SFTP Prompt Style"), "")
            RSSShellPromptStyle = If(ConfigToken("Shell")?("RSS Prompt Style"), "")
            TextEdit_PromptStyle = If(ConfigToken("Shell")?("Text Edit Prompt Style"), "")
            ZipShell_PromptStyle = If(ConfigToken("Shell")?("Zip Shell Prompt Style"), "")
            Test_PromptStyle = If(ConfigToken("Shell")?("Test Shell Prompt Style"), "")
            JsonShell_PromptStyle = If(ConfigToken("Shell")?("JSON Shell Prompt Style"), "")
            ProbeInjectedCommands = If(ConfigToken("Shell")?("Probe injected commands"), True)

            'Filesystem Section
            Wdbg(DebugLevel.I, "Parsing filesystem section...")
            DebugQuota = If(Integer.TryParse(ConfigToken("Filesystem")?("Debug Size Quota in Bytes"), 0), ConfigToken("Filesystem")?("Debug Size Quota in Bytes"), 1073741824)
            FullParseMode = If(ConfigToken("Filesystem")?("Size parse mode"), False)
            HiddenFiles = If(ConfigToken("Filesystem")?("Show Hidden Files"), False)
            SortMode = If(ConfigToken("Filesystem")?("Filesystem sort mode") IsNot Nothing, If([Enum].TryParse(ConfigToken("Filesystem")?("Filesystem sort mode"), SortMode), SortMode, FilesystemSortOptions.FullName), FilesystemSortOptions.FullName)
            SortDirection = If(ConfigToken("Filesystem")?("Filesystem sort direction") IsNot Nothing, If([Enum].TryParse(ConfigToken("Filesystem")?("Filesystem sort direction"), SortDirection), SortDirection, FilesystemSortDirection.Ascending), FilesystemSortDirection.Ascending)
            ShowFilesystemProgress = If(ConfigToken("Filesystem")?("Show progress on filesystem operations"), True)
            ShowFileDetailsList = If(ConfigToken("Filesystem")?("Show file details in list"), True)

            'Hardware Section
            Wdbg(DebugLevel.I, "Parsing hardware section...")
            QuietHardwareProbe = If(ConfigToken("Hardware")?("Quiet Probe"), False)
            FullHardwareProbe = If(ConfigToken("Hardware")?("Full Probe"), True)
            VerboseHardwareProbe = If(ConfigToken("Hardware")?("Verbose Probe"), False)

            'Network Section
            Wdbg(DebugLevel.I, "Parsing network section...")
            DebugPort = If(Integer.TryParse(ConfigToken("Network")?("Debug Port"), 0), ConfigToken("Network")?("Debug Port"), 3014)
            DownloadRetries = If(Integer.TryParse(ConfigToken("Network")?("Download Retry Times"), 0), ConfigToken("Network")?("Download Retry Times"), 3)
            UploadRetries = If(Integer.TryParse(ConfigToken("Network")?("Upload Retry Times"), 0), ConfigToken("Network")?("Upload Retry Times"), 3)
            ShowProgress = If(ConfigToken("Network")?("Show progress bar while downloading or uploading from ""get"" or ""put"" command"), True)
            FTPLoggerUsername = If(ConfigToken("Network")?("Log FTP username"), False)
            FTPLoggerIP = If(ConfigToken("Network")?("Log FTP IP address"), False)
            FTPFirstProfileOnly = If(ConfigToken("Network")?("Return only first FTP profile"), False)
            ShowPreview = If(ConfigToken("Network")?("Show mail message preview"), False)
            RecordChatToDebugLog = If(ConfigToken("Network")?("Record chat to debug log"), True)
            SSHBanner = If(ConfigToken("Network")?("Show SSH banner"), False)
            RPCEnabled = If(ConfigToken("Network")?("Enable RPC"), True)
            RPCPort = If(Integer.TryParse(ConfigToken("Network")?("RPC Port"), 0), ConfigToken("Network")?("RPC Port"), 12345)
            FtpShowDetailsInList = If(ConfigToken("Network")?("Show file details in FTP list"), True)
            FtpUserPromptStyle = If(ConfigToken("Network")?("Username prompt style for FTP"), "")
            FtpPassPromptStyle = If(ConfigToken("Network")?("Password prompt style for FTP"), "")
            FtpUseFirstProfile = If(ConfigToken("Network")?("Use first FTP profile"), True)
            FtpNewConnectionsToSpeedDial = If(ConfigToken("Network")?("Add new connections to FTP speed dial"), True)
            FtpTryToValidateCertificate = If(ConfigToken("Network")?("Try to validate secure FTP certificates"), True)
            FtpShowMotd = If(ConfigToken("Network")?("Show FTP MOTD on connection"), True)
            FtpAlwaysAcceptInvalidCerts = If(ConfigToken("Network")?("Always accept invalid FTP certificates"), True)
            Mail_UserPromptStyle = If(ConfigToken("Network")?("Username prompt style for mail"), "")
            Mail_PassPromptStyle = If(ConfigToken("Network")?("Password prompt style for mail"), "")
            Mail_IMAPPromptStyle = If(ConfigToken("Network")?("IMAP prompt style for mail"), "")
            Mail_SMTPPromptStyle = If(ConfigToken("Network")?("SMTP prompt style for mail"), "")
            Mail_AutoDetectServer = If(ConfigToken("Network")?("Automatically detect mail server"), True)
            Mail_Debug = If(ConfigToken("Network")?("Enable mail debug"), False)
            Mail_NotifyNewMail = If(ConfigToken("Network")?("Notify for new mail messages"), True)
            Mail_GPGPromptStyle = If(ConfigToken("Network")?("GPG password prompt style for mail"), True)
            Mail_ImapPingInterval = If(Integer.TryParse(ConfigToken("Network")?("Send IMAP ping interval"), 0), ConfigToken("Network")?("Send IMAP ping interval"), 30000)
            Mail_SmtpPingInterval = If(Integer.TryParse(ConfigToken("Network")?("Send SMTP ping interval"), 0), ConfigToken("Network")?("Send SMTP ping interval"), 30000)
            Mail_TextFormat = If(ConfigToken("Network")?("Mail text format") IsNot Nothing, If([Enum].TryParse(ConfigToken("Network")?("Mail text format"), Mail_TextFormat), Mail_TextFormat, TextFormat.Plain), TextFormat.Plain)
            RDebugAutoStart = If(ConfigToken("Network")?("Automatically start remote debug on startup"), True)
            RDebugMessageFormat = If(ConfigToken("Network")?("Remote debug message format"), "")
            RSSFeedUrlPromptStyle = If(ConfigToken("Network")?("RSS feed URL prompt style"), "")
            RSSRefreshFeeds = If(ConfigToken("Network")?("Auto refresh RSS feed"), True)
            RSSRefreshInterval = If(Integer.TryParse(ConfigToken("Network")?("Auto refresh RSS feed interval"), 0), ConfigToken("Network")?("Auto refresh RSS feed interval"), 60000)
            SFTPShowDetailsInList = If(ConfigToken("Network")?("Show file details in SFTP list"), True)
            SFTPUserPromptStyle = If(ConfigToken("Network")?("Username prompt style for SFTP"), "")
            SFTPNewConnectionsToSpeedDial = If(ConfigToken("Network")?("Add new connections to SFTP speed dial"), True)
            PingTimeout = If(Integer.TryParse(ConfigToken("Network")?("Ping timeout"), 0), ConfigToken("Network")?("Ping timeout"), 60000)
            ExtensiveAdapterInformation = If(ConfigToken("Network")?("Show extensive adapter info"), True)
            GeneralNetworkInformation = If(ConfigToken("Network")?("Show general network information"), True)
            DownloadPercentagePrint = If(ConfigToken("Network")?("Download percentage text"), "")
            UploadPercentagePrint = If(ConfigToken("Network")?("Upload percentage text"), "")

            'Screensaver Section
            DefSaverName = If(ConfigToken("Screensaver")?("Screensaver"), "matrix")
            ScrnTimeout = If(Integer.TryParse(ConfigToken("Screensaver")?("Screensaver Timeout in ms"), 0), ConfigToken("Screensaver")?("Screensaver Timeout in ms"), 300000)
            ScreensaverDebug = If(ConfigToken("Screensaver")?("Enable screensaver debugging"), False)
            PasswordLock = If(ConfigToken("Screensaver")?("Ask for password after locking"), True)

            'Screensaver-specific settings go below:
            '> ColorMix
            ColorMix255Colors = If(ConfigToken("Screensaver")?("ColorMix")?("Activate 255 Color Mode"), False)
            ColorMixTrueColor = If(ConfigToken("Screensaver")?("ColorMix")?("Activate True Color Mode"), True)
            ColorMixDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("ColorMix")?("Delay in Milliseconds"), 1)
            ColorMixBackgroundColor = New Color(If(ConfigToken("Screensaver")?("ColorMix")?("Background color"), ConsoleColors.Red)).PlainSequence
            ColorMixMinimumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Minimum red color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Minimum red color level"), 0)
            ColorMixMinimumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Minimum green color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Minimum green color level"), 0)
            ColorMixMinimumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Minimum blue color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Minimum blue color level"), 0)
            ColorMixMinimumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Minimum color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Minimum color level"), 0)
            ColorMixMaximumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Maximum red color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Maximum red color level"), 255)
            ColorMixMaximumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Maximum green color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Maximum green color level"), 255)
            ColorMixMaximumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Maximum blue color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Maximum blue color level"), 255)
            ColorMixMaximumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("ColorMix")?("Maximum color level"), 0), ConfigToken("Screensaver")?("ColorMix")?("Maximum color level"), 255)

            '> Disco
            Disco255Colors = If(ConfigToken("Screensaver")?("Disco")?("Activate 255 Color Mode"), False)
            DiscoTrueColor = If(ConfigToken("Screensaver")?("Disco")?("Activate True Color Mode"), True)
            DiscoCycleColors = If(ConfigToken("Screensaver")?("Disco")?("Cycle Colors"), False)
            DiscoDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Disco")?("Delay in Milliseconds"), 100)
            DiscoUseBeatsPerMinute = If(ConfigToken("Screensaver")?("Disco")?("Use Beats Per Minute"), False)
            DiscoMinimumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Minimum red color level"), 0), ConfigToken("Screensaver")?("Disco")?("Minimum red color level"), 0)
            DiscoMinimumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Minimum green color level"), 0), ConfigToken("Screensaver")?("Disco")?("Minimum green color level"), 0)
            DiscoMinimumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Minimum blue color level"), 0), ConfigToken("Screensaver")?("Disco")?("Minimum blue color level"), 0)
            DiscoMinimumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Minimum color level"), 0), ConfigToken("Screensaver")?("Disco")?("Minimum color level"), 0)
            DiscoMaximumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Maximum red color level"), 0), ConfigToken("Screensaver")?("Disco")?("Maximum red color level"), 255)
            DiscoMaximumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Maximum green color level"), 0), ConfigToken("Screensaver")?("Disco")?("Maximum green color level"), 255)
            DiscoMaximumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Maximum blue color level"), 0), ConfigToken("Screensaver")?("Disco")?("Maximum blue color level"), 255)
            DiscoMaximumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Disco")?("Maximum color level"), 0), ConfigToken("Screensaver")?("Disco")?("Maximum color level"), 255)

            '> GlitterColor
            GlitterColor255Colors = If(ConfigToken("Screensaver")?("GlitterColor")?("Activate 255 Color Mode"), False)
            GlitterColorTrueColor = If(ConfigToken("Screensaver")?("GlitterColor")?("Activate True Color Mode"), True)
            GlitterColorDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Delay in Milliseconds"), 1)
            GlitterColorMinimumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Minimum red color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Minimum red color level"), 0)
            GlitterColorMinimumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Minimum green color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Minimum green color level"), 0)
            GlitterColorMinimumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Minimum blue color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Minimum blue color level"), 0)
            GlitterColorMinimumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Minimum color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Minimum color level"), 0)
            GlitterColorMaximumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Maximum red color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Maximum red color level"), 255)
            GlitterColorMaximumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Maximum green color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Maximum green color level"), 255)
            GlitterColorMaximumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Maximum blue color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Maximum blue color level"), 255)
            GlitterColorMaximumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterColor")?("Maximum color level"), 0), ConfigToken("Screensaver")?("GlitterColor")?("Maximum color level"), 255)

            '> GlitterMatrix
            GlitterMatrixDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("GlitterMatrix")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("GlitterMatrix")?("Delay in Milliseconds"), 1)
            GlitterMatrixBackgroundColor = New Color(If(ConfigToken("Screensaver")?("GlitterMatrix")?("Background color"), ConsoleColors.Black)).PlainSequence
            GlitterMatrixForegroundColor = New Color(If(ConfigToken("Screensaver")?("GlitterMatrix")?("Foreground color"), ConsoleColors.Green)).PlainSequence

            '> Lines
            Lines255Colors = If(ConfigToken("Screensaver")?("Lines")?("Activate 255 Color Mode"), False)
            LinesTrueColor = If(ConfigToken("Screensaver")?("Lines")?("Activate True Color Mode"), True)
            LinesDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Lines")?("Delay in Milliseconds"), 500)
            LinesLineChar = If(ConfigToken("Screensaver")?("Lines")?("Line character"), "-")
            LinesBackgroundColor = New Color(If(ConfigToken("Screensaver")?("Lines")?("Background color"), ConsoleColors.Black)).PlainSequence
            LinesMinimumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Minimum red color level"), 0), ConfigToken("Screensaver")?("Lines")?("Minimum red color level"), 0)
            LinesMinimumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Minimum green color level"), 0), ConfigToken("Screensaver")?("Lines")?("Minimum green color level"), 0)
            LinesMinimumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Minimum blue color level"), 0), ConfigToken("Screensaver")?("Lines")?("Minimum blue color level"), 0)
            LinesMinimumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Minimum color level"), 0), ConfigToken("Screensaver")?("Lines")?("Minimum color level"), 0)
            LinesMaximumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Maximum red color level"), 0), ConfigToken("Screensaver")?("Lines")?("Maximum red color level"), 255)
            LinesMaximumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Maximum green color level"), 0), ConfigToken("Screensaver")?("Lines")?("Maximum green color level"), 255)
            LinesMaximumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Maximum blue color level"), 0), ConfigToken("Screensaver")?("Lines")?("Maximum blue color level"), 255)
            LinesMaximumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Lines")?("Maximum color level"), 0), ConfigToken("Screensaver")?("Lines")?("Maximum color level"), 255)

            '> Dissolve
            Dissolve255Colors = If(ConfigToken("Screensaver")?("Dissolve")?("Activate 255 Color Mode"), False)
            DissolveTrueColor = If(ConfigToken("Screensaver")?("Dissolve")?("Activate True Color Mode"), True)
            DissolveBackgroundColor = New Color(If(ConfigToken("Screensaver")?("Dissolve")?("Background color"), ConsoleColors.Black)).PlainSequence
            DissolveMinimumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Minimum red color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Minimum red color level"), 0)
            DissolveMinimumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Minimum green color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Minimum green color level"), 0)
            DissolveMinimumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Minimum blue color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Minimum blue color level"), 0)
            DissolveMinimumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Minimum color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Minimum color level"), 0)
            DissolveMaximumRedColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Maximum red color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Maximum red color level"), 255)
            DissolveMaximumGreenColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Maximum green color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Maximum green color level"), 255)
            DissolveMaximumBlueColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Maximum blue color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Maximum blue color level"), 255)
            DissolveMaximumColorLevel = If(Integer.TryParse(ConfigToken("Screensaver")?("Dissolve")?("Maximum color level"), 0), ConfigToken("Screensaver")?("Dissolve")?("Maximum color level"), 255)

            '> BouncingBlock
            BouncingBlock255Colors = If(ConfigToken("Screensaver")?("BouncingBlock")?("Activate 255 Color Mode"), False)
            BouncingBlockTrueColor = If(ConfigToken("Screensaver")?("BouncingBlock")?("Activate True Color Mode"), True)
            BouncingBlockDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("BouncingBlock")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("BouncingBlock")?("Delay in Milliseconds"), 10)

            '> BouncingText
            BouncingText255Colors = If(ConfigToken("Screensaver")?("BouncingText")?("Activate 255 Color Mode"), False)
            BouncingTextTrueColor = If(ConfigToken("Screensaver")?("BouncingText")?("Activate True Color Mode"), True)
            BouncingTextDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("BouncingText")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("BouncingText")?("Delay in Milliseconds"), 10)
            BouncingTextWrite = If(ConfigToken("Screensaver")?("BouncingText")?("Text Shown"), "Kernel Simulator")

            '> ProgressClock
            ProgressClock255Colors = If(ConfigToken("Screensaver")?("ProgressClock")?("Activate 255 Color Mode"), False)
            ProgressClockTrueColor = If(ConfigToken("Screensaver")?("ProgressClock")?("Activate True Color Mode"), True)
            ProgressClockCycleColors = If(ConfigToken("Screensaver")?("ProgressClock")?("Cycle Colors"), True)
            ProgressClockSecondsProgressColor = If(ConfigToken("Screensaver")?("ProgressClock")?("Color of Seconds Bar"), 4)
            ProgressClockMinutesProgressColor = If(ConfigToken("Screensaver")?("ProgressClock")?("Color of Minutes Bar"), 5)
            ProgressClockHoursProgressColor = If(ConfigToken("Screensaver")?("ProgressClock")?("Color of Hours Bar"), 6)
            ProgressClockProgressColor = If(ConfigToken("Screensaver")?("ProgressClock")?("Color of Information"), 7)
            ProgressClockCycleColorsTicks = If(Integer.TryParse(ConfigToken("Screensaver")?("ProgressClock")?("Ticks to change color"), 0), ConfigToken("Screensaver")?("ProgressClock")?("Ticks to change color"), 20)

            '> Lighter
            Lighter255Colors = If(ConfigToken("Screensaver")?("Lighter")?("Activate 255 Color Mode"), False)
            LighterTrueColor = If(ConfigToken("Screensaver")?("Lighter")?("Activate True Color Mode"), True)
            LighterDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Lighter")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Lighter")?("Delay in Milliseconds"), 100)
            LighterMaxPositions = If(Integer.TryParse(ConfigToken("Screensaver")?("Lighter")?("Max Positions Count"), 0), ConfigToken("Screensaver")?("Lighter")?("Max Positions Count"), 10)

            '> Wipe
            Wipe255Colors = If(ConfigToken("Screensaver")?("Wipe")?("Activate 255 Color Mode"), False)
            WipeTrueColor = If(ConfigToken("Screensaver")?("Wipe")?("Activate True Color Mode"), True)
            WipeDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Wipe")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Wipe")?("Delay in Milliseconds"), 10)
            WipeWipesNeededToChangeDirection = If(Integer.TryParse(ConfigToken("Screensaver")?("Wipe")?("Wipes to change direction"), 0), ConfigToken("Screensaver")?("Wipe")?("Wipes to change direction"), 10)

            '> Fader
            FaderDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Fader")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Fader")?("Delay in Milliseconds"), 50)
            FaderFadeOutDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Fader")?("Fade Out Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Fader")?("Fade Out Delay in Milliseconds"), 3000)
            FaderWrite = If(ConfigToken("Screensaver")?("Fader")?("Text Shown"), "Kernel Simulator")
            FaderMaxSteps = If(Integer.TryParse(ConfigToken("Screensaver")?("Fader")?("Max Fade Steps"), 0), ConfigToken("Screensaver")?("Fader")?("Max Fade Steps"), 25)

            '> FaderBack
            FaderBackDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("FaderBack")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("FaderBack")?("Delay in Milliseconds"), 50)
            FaderBackFadeOutDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("FaderBack")?("Fade Out Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("FaderBack")?("Fade Out Delay in Milliseconds"), 3000)
            FaderBackMaxSteps = If(Integer.TryParse(ConfigToken("Screensaver")?("FaderBack")?("Max Fade Steps"), 0), ConfigToken("Screensaver")?("FaderBack")?("Max Fade Steps"), 25)

            '> BeatFader
            BeatFader255Colors = If(ConfigToken("Screensaver")?("BeatFader")?("Activate 255 Color Mode"), False)
            BeatFaderTrueColor = If(ConfigToken("Screensaver")?("BeatFader")?("Activate True Color Mode"), True)
            BeatFaderCycleColors = If(ConfigToken("Screensaver")?("BeatFader")?("Cycle Colors"), True)
            BeatFaderBeatColor = If(ConfigToken("Screensaver")?("BeatFader")?("Beat Color"), 17)
            BeatFaderDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("BeatFader")?("Delay in Beats Per Minute"), 0), ConfigToken("Screensaver")?("BeatFader")?("Delay in Beats Per Minute"), 120)
            BeatFaderMaxSteps = If(Integer.TryParse(ConfigToken("Screensaver")?("BeatFader")?("Max Fade Steps"), 0), ConfigToken("Screensaver")?("BeatFader")?("Max Fade Steps"), 25)

            '> Typo
            TypoDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Typo")?("Delay in Milliseconds"), 50)
            TypoWriteAgainDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Write Again Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Typo")?("Write Again Delay in Milliseconds"), 3000)
            TypoWrite = If(ConfigToken("Screensaver")?("Typo")?("Text Shown"), "Kernel Simulator")
            TypoWritingSpeedMin = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Minimum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Typo")?("Minimum writing speed in WPM"), 50)
            TypoWritingSpeedMax = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Maximum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Typo")?("Maximum writing speed in WPM"), 80)
            TypoMissStrikePossibility = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Probability of typo in percent"), 0), ConfigToken("Screensaver")?("Typo")?("Probability of typo in percent"), 20)
            TypoMissPossibility = If(Integer.TryParse(ConfigToken("Screensaver")?("Typo")?("Probability of miss in percent"), 0), ConfigToken("Screensaver")?("Typo")?("Probability of miss in percent"), 10)

            '> Marquee
            Marquee255Colors = If(ConfigToken("Screensaver")?("Marquee")?("Activate 255 Color Mode"), False)
            MarqueeTrueColor = If(ConfigToken("Screensaver")?("Marquee")?("Activate True Color Mode"), True)
            MarqueeWrite = If(ConfigToken("Screensaver")?("Marquee")?("Text Shown"), "Kernel Simulator")
            MarqueeDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Marquee")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Marquee")?("Delay in Milliseconds"), 10)
            MarqueeAlwaysCentered = If(ConfigToken("Screensaver")?("Marquee")?("Always Centered"), True)
            MarqueeUseConsoleAPI = If(ConfigToken("Screensaver")?("Marquee")?("Use Console API"), False)

            '> Matrix
            MatrixDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Matrix")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Matrix")?("Delay in Milliseconds"), 1)

            '> Linotypo
            LinotypoDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Linotypo")?("Delay in Milliseconds"), 50)
            LinotypoNewScreenDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("New Screen Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Linotypo")?("New Screen Delay in Milliseconds"), 3000)
            LinotypoWrite = If(ConfigToken("Screensaver")?("Linotypo")?("Text Shown"), "Kernel Simulator")
            LinotypoWritingSpeedMin = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Minimum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Linotypo")?("Minimum writing speed in WPM"), 50)
            LinotypoWritingSpeedMax = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Maximum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Linotypo")?("Maximum writing speed in WPM"), 80)
            LinotypoMissStrikePossibility = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Probability of typo in percent"), 0), ConfigToken("Screensaver")?("Linotypo")?("Probability of typo in percent"), 1)
            LinotypoTextColumns = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Column Count"), 0), ConfigToken("Screensaver")?("Linotypo")?("Column Count"), 3)
            LinotypoEtaoinThreshold = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Line Fill Threshold"), 0), ConfigToken("Screensaver")?("Linotypo")?("Line Fill Threshold"), 5)
            LinotypoEtaoinCappingPossibility = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Line Fill Capping Probability in percent"), 0), ConfigToken("Screensaver")?("Linotypo")?("Line Fill Capping Probability in percent"), 5)
            LinotypoEtaoinType = If(ConfigToken("Screensaver")?("Linotypo")?("Line Fill Type") IsNot Nothing, If([Enum].TryParse(ConfigToken("Screensaver")?("Linotypo")?("Line Fill Type"), LinotypoEtaoinType), LinotypoEtaoinType, FillType.EtaoinPattern), FillType.EtaoinPattern)
            LinotypoMissPossibility = If(Integer.TryParse(ConfigToken("Screensaver")?("Linotypo")?("Probability of miss in percent"), 0), ConfigToken("Screensaver")?("Linotypo")?("Probability of miss in percent"), 10)

            '> Typewriter
            TypewriterDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Typewriter")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Typewriter")?("Delay in Milliseconds"), 50)
            TypewriterNewScreenDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Typewriter")?("New Screen Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Typewriter")?("New Screen Delay in Milliseconds"), 3000)
            TypewriterWrite = If(ConfigToken("Screensaver")?("Typewriter")?("Text Shown"), "Kernel Simulator")
            TypewriterWritingSpeedMin = If(Integer.TryParse(ConfigToken("Screensaver")?("Typewriter")?("Minimum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Typewriter")?("Minimum writing speed in WPM"), 50)
            TypewriterWritingSpeedMax = If(Integer.TryParse(ConfigToken("Screensaver")?("Typewriter")?("Maximum writing speed in WPM"), 0), ConfigToken("Screensaver")?("Typewriter")?("Maximum writing speed in WPM"), 80)

            '> FlashColor
            FlashColor255Colors = If(ConfigToken("Screensaver")?("FlashColor")?("Activate 255 Color Mode"), False)
            FlashColorTrueColor = If(ConfigToken("Screensaver")?("FlashColor")?("Activate True Color Mode"), True)
            FlashColorDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("FlashColor")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("FlashColor")?("Delay in Milliseconds"), 1)

            '> SpotWrite
            SpotWriteDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("SpotWrite")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("SpotWrite")?("Delay in Milliseconds"), 50)
            SpotWriteNewScreenDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("SpotWrite")?("New Screen Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("SpotWrite")?("New Screen Delay in Milliseconds"), 3000)
            SpotWriteWrite = If(ConfigToken("Screensaver")?("SpotWrite")?("Text Shown"), "Kernel Simulator")

            '> Ramp
            Ramp255Colors = If(ConfigToken("Screensaver")?("Ramp")?("Activate 255 Color Mode"), False)
            RampTrueColor = If(ConfigToken("Screensaver")?("Ramp")?("Activate True Color Mode"), True)
            RampDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Ramp")?("Delay in Milliseconds"), 0), ConfigToken("Screensaver")?("Ramp")?("Delay in Milliseconds"), 20)
            RampNextRampDelay = If(Integer.TryParse(ConfigToken("Screensaver")?("Ramp")?("Next ramp interval"), 0), ConfigToken("Screensaver")?("Ramp")?("Next ramp interval"), 250)

            'Misc Section
            Wdbg(DebugLevel.I, "Parsing misc section...")
            CornerTimeDate = If(ConfigToken("Misc")?("Show Time/Date on Upper Right Corner"), False)
            StartScroll = If(ConfigToken("Misc")?("Marquee on startup"), True)
            LongTimeDate = If(ConfigToken("Misc")?("Long Time and Date"), True)
            PreferredUnit = If(ConfigToken("Misc")?("Preferred Unit for Temperature") IsNot Nothing, If([Enum].TryParse(ConfigToken("Misc")?("Preferred Unit for Temperature"), PreferredUnit), PreferredUnit, UnitMeasurement.Metric), UnitMeasurement.Metric)
            TextEdit_AutoSaveFlag = If(ConfigToken("Misc")?("Enable text editor autosave"), True)
            TextEdit_AutoSaveInterval = If(Integer.TryParse(ConfigToken("Misc")?("Text editor autosave interval"), 0), ConfigToken("Misc")?("Text editor autosave interval"), 60)
            WrapListOutputs = If(ConfigToken("Misc")?("Wrap list outputs"), False)
            DrawBorderNotification = If(ConfigToken("Misc")?("Draw notification border"), False)
            BlacklistedModsString = If(ConfigToken("Misc")?("Blacklisted mods"), "")

            'Check to see if the config needs fixes
            RepairConfig()

            'Raise event and return true
            EventManager.RaiseConfigRead()
            Return True
        Catch nre As NullReferenceException
            'Rare, but repair config if an NRE is caught.
            Wdbg(DebugLevel.E, "Error trying to read config: {0}", nre.Message)
            RepairConfig()
        Catch ex As Exception
            EventManager.RaiseConfigReadError(ex)
            WStkTrc(ex)
            NotifyConfigError = True
            Wdbg(DebugLevel.E, "Error trying to read config: {0}", ex.Message)
            Throw New Exceptions.ConfigException(DoTranslation("There is an error trying to read configuration: {0}."), ex, ex.Message)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Main loader for configuration file
    ''' </summary>
    Sub InitializeConfig()
        'Make a config file if not found
        If Not File.Exists(GetKernelPath(KernelPathType.Configuration)) Then
            Wdbg(DebugLevel.E, "No config file found. Creating...")
            CreateConfig()
        End If

        'Load and read config
        Try
            ReadConfig()
        Catch cex As Exceptions.ConfigException
            W(cex.Message, True, ColTypes.Error)
            WStkTrc(cex)
        End Try
    End Sub

    ''' <summary>
    ''' Initializes the config token
    ''' </summary>
    Sub InitializeConfigToken()
        ConfigToken = JObject.Parse(File.ReadAllText(GetKernelPath(KernelPathType.Configuration)))
    End Sub

End Module
