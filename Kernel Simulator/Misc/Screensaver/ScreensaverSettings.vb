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

Public Module ScreensaverSettings

    '-> Color Settings
    ''' <summary>
    ''' [ColorMix] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public ColorMix255Colors As Boolean
    ''' <summary>
    ''' [ColorMix] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public ColorMixTrueColor As Boolean = True
    ''' <summary>
    ''' [Disco] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Disco255Colors As Boolean
    ''' <summary>
    ''' [Disco] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public DiscoTrueColor As Boolean = True
    ''' <summary>
    ''' [GlitterColor] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public GlitterColor255Colors As Boolean
    ''' <summary>
    ''' [GlitterColor] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public GlitterColorTrueColor As Boolean = True
    ''' <summary>
    ''' [Lines] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Lines255Colors As Boolean
    ''' <summary>
    ''' [Lines] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public LinesTrueColor As Boolean = True
    ''' <summary>
    ''' [Dissolve] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Dissolve255Colors As Boolean
    ''' <summary>
    ''' [Dissolve] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public DissolveTrueColor As Boolean = True
    ''' <summary>
    ''' [BouncingBlock] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public BouncingBlock255Colors As Boolean
    ''' <summary>
    ''' [BouncingBlock] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public BouncingBlockTrueColor As Boolean = True
    ''' <summary>
    ''' [BouncingText] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public BouncingText255Colors As Boolean
    ''' <summary>
    ''' [BouncingText] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public BouncingTextTrueColor As Boolean = True
    ''' <summary>
    ''' [ProgressClock] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public ProgressClock255Colors As Boolean
    ''' <summary>
    ''' [ProgressClock] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public ProgressClockTrueColor As Boolean = True
    ''' <summary>
    ''' [Lighter] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Lighter255Colors As Boolean
    ''' <summary>
    ''' [Lighter] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public LighterTrueColor As Boolean = True
    ''' <summary>
    ''' [Wipe] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Wipe255Colors As Boolean
    ''' <summary>
    ''' [Wipe] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public WipeTrueColor As Boolean = True
    ''' <summary>
    ''' [Disco] Enable color cycling
    ''' </summary>
    Public DiscoCycleColors As Boolean
    ''' <summary>
    ''' [ProgressClock] Enable color cycling (uses RNG. If disabled, uses the <see cref="ProgressClockSecondsProgressColor"/>, <see cref="ProgressClockMinutesProgressColor"/>, and <see cref="ProgressClockHoursProgressColor"/> colors.)
    ''' </summary>
    Public ProgressClockCycleColors As Boolean = True
    ''' <summary>
    ''' [ProgressClock] The color of seconds progress bar. It can be 1-16, 1-255, or "1-255;1-255;1-255".
    ''' </summary>
    Public ProgressClockSecondsProgressColor As String = 4
    ''' <summary>
    ''' [ProgressClock] The color of minutes progress bar. It can be 1-16, 1-255, or "1-255;1-255;1-255".
    ''' </summary>
    Public ProgressClockMinutesProgressColor As String = 5
    ''' <summary>
    ''' [ProgressClock] The color of hours progress bar. It can be 1-16, 1-255, or "1-255;1-255;1-255".
    ''' </summary>
    Public ProgressClockHoursProgressColor As String = 6
    ''' <summary>
    ''' [ProgressClock] The color of date information. It can be 1-16, 1-255, or "1-255;1-255;1-255".
    ''' </summary>
    Public ProgressClockProgressColor As String = 7
    ''' <summary>
    ''' [HackUserFromAD] Sets the console foreground color to green to represent "Hacker Mode"
    ''' </summary>
    Public HackUserFromADHackerMode As Boolean = True
    ''' <summary>
    ''' [AptErrorSim] Sets the console foreground color to green to represent "Hacker Mode"
    ''' </summary>
    Public AptErrorSimHackerMode As Boolean = False

    '-> Delays
    ''' <summary>
    ''' [BouncingBlock] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public BouncingBlockDelay As Integer = 10
    ''' <summary>
    ''' [BouncingText] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public BouncingTextDelay As Integer = 10
    ''' <summary>
    ''' [ColorMix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public ColorMixDelay As Integer = 1
    ''' <summary>
    ''' [Disco] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public DiscoDelay As Integer = 100
    ''' <summary>
    ''' [GlitterColor] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public GlitterColorDelay As Integer = 1
    ''' <summary>
    ''' [GlitterMatrix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public GlitterMatrixDelay As Integer = 1
    ''' <summary>
    ''' [Lines] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public LinesDelay As Integer = 500
    ''' <summary>
    ''' [Matrix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public MatrixDelay As Integer = 1
    ''' <summary>
    ''' [ProgressClock] If color cycling is enabled, how many ticks before changing colors? 1 tick = 0.5 seconds
    ''' </summary>
    Public ProgressClockCycleColorsTicks As Long = 20
    ''' <summary>
    ''' [Lighter] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public LighterDelay As Integer = 100
    ''' <summary>
    ''' [Fader] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public FaderDelay As Integer = 50
    ''' <summary>
    ''' [Fader] How many milliseconds to wait before fading the text out?
    ''' </summary>
    Public FaderFadeOutDelay As Integer = 3000
    ''' <summary>
    ''' [Typo] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public TypoDelay As Integer = 50
    ''' <summary>
    ''' [Typo] How many milliseconds to wait before writing the text again?
    ''' </summary>
    Public TypoWriteAgainDelay As Integer = 3000
    ''' <summary>
    ''' [Wipe] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public WipeDelay As Integer = 10

    '-> Texts
    ''' <summary>
    ''' [BouncingText] Text for Bouncing Text
    ''' </summary>
    Public BouncingTextWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Fader] Text for Fader
    ''' </summary>
    Public FaderWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Typo] Text for Typo
    ''' </summary>
    Public TypoWrite As String = "Kernel Simulator"

    '-> Misc
    ''' <summary>
    ''' [Lighter] How many positions to write before starting to blacken them?
    ''' </summary>
    Public LighterMaxPositions As Integer = 10
    ''' <summary>
    ''' [Fader] How many fade steps to do?
    ''' </summary>
    Public FaderMaxSteps As Integer = 25
    ''' <summary>
    ''' [Typo] Minimum writing speed in WPM
    ''' </summary>
    Public TypoWritingSpeedMin As Integer = 50
    ''' <summary>
    ''' [Typo] Maximum writing speed in WPM
    ''' </summary>
    Public TypoWritingSpeedMax As Integer = 80
    ''' <summary>
    ''' [Typo] Possibility that the writer made a typo in percent
    ''' </summary>
    Public TypoMissStrikePossibility As Integer = 60
    ''' <summary>
    ''' [Wipe] How many wipes needed to change direction?
    ''' </summary>
    Public WipeWipesNeededToChangeDirection As Integer = 10

End Module
