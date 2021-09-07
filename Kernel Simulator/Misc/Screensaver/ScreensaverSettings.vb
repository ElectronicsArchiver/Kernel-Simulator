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

    '-> ColorMix
    ''' <summary>
    ''' [ColorMix] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public ColorMix255Colors As Boolean
    ''' <summary>
    ''' [ColorMix] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public ColorMixTrueColor As Boolean = True
    ''' <summary>
    ''' [ColorMix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public ColorMixDelay As Integer = 1

    '-> Disco
    ''' <summary>
    ''' [Disco] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Disco255Colors As Boolean
    ''' <summary>
    ''' [Disco] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public DiscoTrueColor As Boolean = True
    ''' <summary>
    ''' [Disco] Enable color cycling
    ''' </summary>
    Public DiscoCycleColors As Boolean
    ''' <summary>
    ''' [Disco] How many milliseconds, or beats per minute, to wait before making the next write?
    ''' </summary>
    Public DiscoDelay As Integer = 100
    ''' <summary>
    ''' [Disco] Whether to use the Beats Per Minute (1/4) to change the writing delay. If False, will use the standard milliseconds delay instead.
    ''' </summary>
    Public DiscoUseBeatsPerMinute As Boolean = False

    '-> GlitterColor
    ''' <summary>
    ''' [GlitterColor] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public GlitterColor255Colors As Boolean
    ''' <summary>
    ''' [GlitterColor] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public GlitterColorTrueColor As Boolean = True
    ''' <summary>
    ''' [GlitterColor] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public GlitterColorDelay As Integer = 1

    '-> Lines
    ''' <summary>
    ''' [Lines] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Lines255Colors As Boolean
    ''' <summary>
    ''' [Lines] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public LinesTrueColor As Boolean = True
    ''' <summary>
    ''' [Lines] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public LinesDelay As Integer = 500

    '-> Dissolve
    ''' <summary>
    ''' [Dissolve] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Dissolve255Colors As Boolean
    ''' <summary>
    ''' [Dissolve] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public DissolveTrueColor As Boolean = True

    '-> BouncingBlock
    ''' <summary>
    ''' [BouncingBlock] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public BouncingBlock255Colors As Boolean
    ''' <summary>
    ''' [BouncingBlock] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public BouncingBlockTrueColor As Boolean = True
    ''' <summary>
    ''' [BouncingBlock] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public BouncingBlockDelay As Integer = 10

    '-> BouncingText
    ''' <summary>
    ''' [BouncingText] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public BouncingText255Colors As Boolean
    ''' <summary>
    ''' [BouncingText] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public BouncingTextTrueColor As Boolean = True
    ''' <summary>
    ''' [BouncingText] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public BouncingTextDelay As Integer = 10
    ''' <summary>
    ''' [BouncingText] Text for Bouncing Text. Shorter is better.
    ''' </summary>
    Public BouncingTextWrite As String = "Kernel Simulator"

    '-> ProgressClock
    ''' <summary>
    ''' [ProgressClock] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public ProgressClock255Colors As Boolean
    ''' <summary>
    ''' [ProgressClock] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public ProgressClockTrueColor As Boolean = True
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
    ''' [ProgressClock] If color cycling is enabled, how many ticks before changing colors? 1 tick = 0.5 seconds
    ''' </summary>
    Public ProgressClockCycleColorsTicks As Long = 20

    '-> Lighter
    ''' <summary>
    ''' [Lighter] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Lighter255Colors As Boolean
    ''' <summary>
    ''' [Lighter] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public LighterTrueColor As Boolean = True
    ''' <summary>
    ''' [Lighter] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public LighterDelay As Integer = 100
    ''' <summary>
    ''' [Lighter] How many positions to write before starting to blacken them?
    ''' </summary>
    Public LighterMaxPositions As Integer = 10

    '-> Wipe
    ''' <summary>
    ''' [Wipe] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Wipe255Colors As Boolean
    ''' <summary>
    ''' [Wipe] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public WipeTrueColor As Boolean = True
    ''' <summary>
    ''' [Wipe] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public WipeDelay As Integer = 10
    ''' <summary>
    ''' [Wipe] How many wipes needed to change direction?
    ''' </summary>
    Public WipeWipesNeededToChangeDirection As Integer = 10

    '-> HackUserFromAD
    ''' <summary>
    ''' [HackUserFromAD] Sets the console foreground color to green to represent "Hacker Mode"
    ''' </summary>
    Public HackUserFromADHackerMode As Boolean = True

    '-> AptErrorSim
    ''' <summary>
    ''' [AptErrorSim] Sets the console foreground color to green to represent "Hacker Mode"
    ''' </summary>
    Public AptErrorSimHackerMode As Boolean

    '-> Marquee
    ''' <summary>
    ''' [Marquee] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public Marquee255Colors As Boolean
    ''' <summary>
    ''' [Marquee] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public MarqueeTrueColor As Boolean = True
    ''' <summary>
    ''' [Marquee] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public MarqueeDelay As Integer = 10
    ''' <summary>
    ''' [Marquee] Text for Marquee. Shorter is better.
    ''' </summary>
    Public MarqueeWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Marquee] Whether the text is always on center.
    ''' </summary>
    Public MarqueeAlwaysCentered As Boolean = True
    ''' <summary>
    ''' [Marquee] Whether to use the Console.Clear() API (slow) or use the line-clearing VT sequence (fast).
    ''' </summary>
    Public MarqueeUseConsoleAPI As Boolean = False

    '-> BeatFader
    ''' <summary>
    ''' [BeatFader] Enable 255 color support. Has a higher priority than 16 color support. Please note that it only works if color cycling is enabled.
    ''' </summary>
    Public BeatFader255Colors As Boolean
    ''' <summary>
    ''' [BeatFader] Enable truecolor support. Has a higher priority than 255 color support. Please note that it only works if color cycling is enabled.
    ''' </summary>
    Public BeatFaderTrueColor As Boolean = True
    ''' <summary>
    ''' [BeatFader] Enable color cycling (uses RNG. If disabled, uses the <see cref="BeatFaderBeatColor"/> color.)
    ''' </summary>
    Public BeatFaderCycleColors As Boolean = True
    ''' <summary>
    ''' [BeatFader] The color of beats. It can be 1-16, 1-255, or "1-255;1-255;1-255".
    ''' </summary>
    Public BeatFaderBeatColor As String = 17
    ''' <summary>
    ''' [BeatFader] How many beats per minute to wait before making the next write?
    ''' </summary>
    Public BeatFaderDelay As Integer = 120
    ''' <summary>
    ''' [BeatFader] How many fade steps to do?
    ''' </summary>
    Public BeatFaderMaxSteps As Integer = 25

    '-> GlitterMatrix
    ''' <summary>
    ''' [GlitterMatrix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public GlitterMatrixDelay As Integer = 1

    '-> Matrix
    ''' <summary>
    ''' [Matrix] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public MatrixDelay As Integer = 1

    '-> Fader
    ''' <summary>
    ''' [Fader] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public FaderDelay As Integer = 50
    ''' <summary>
    ''' [Fader] How many milliseconds to wait before fading the text out?
    ''' </summary>
    Public FaderFadeOutDelay As Integer = 3000
    ''' <summary>
    ''' [FaderBack] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public FaderBackDelay As Integer = 10
    ''' <summary>
    ''' [FaderBack] How many milliseconds to wait before fading the text out?
    ''' </summary>
    Public FaderBackFadeOutDelay As Integer = 3000
    ''' <summary>
    ''' [Fader] Text for Fader. Shorter is better.
    ''' </summary>
    Public FaderWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Fader] How many fade steps to do?
    ''' </summary>
    Public FaderMaxSteps As Integer = 25
    ''' <summary>
    ''' [FaderBack] How many fade steps to do?
    ''' </summary>
    Public FaderBackMaxSteps As Integer = 25

    '-> Typo
    ''' <summary>
    ''' [Typo] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public TypoDelay As Integer = 50
    ''' <summary>
    ''' [Typo] How many milliseconds to wait before writing the text again?
    ''' </summary>
    Public TypoWriteAgainDelay As Integer = 3000
    ''' <summary>
    ''' [Typo] Text for Typo. Longer is better.
    ''' </summary>
    Public TypoWrite As String = "Kernel Simulator"
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
    Public TypoMissStrikePossibility As Integer = 20
    ''' <summary>
    ''' [Typo] Possibility that the writer missed a character in percent
    ''' </summary>
    Public TypoMissPossibility As Integer = 10

    '-> Linotypo
    ''' <summary>
    ''' [Linotypo] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public LinotypoDelay As Integer = 50
    ''' <summary>
    ''' [Linotypo] How many milliseconds to wait before writing the text in the new screen again?
    ''' </summary>
    Public LinotypoNewScreenDelay As Integer = 3000
    ''' <summary>
    ''' [Linotypo] Text for Linotypo. Longer is better.
    ''' </summary>
    Public LinotypoWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Linotypo] Minimum writing speed in WPM
    ''' </summary>
    Public LinotypoWritingSpeedMin As Integer = 50
    ''' <summary>
    ''' [Linotypo] Maximum writing speed in WPM
    ''' </summary>
    Public LinotypoWritingSpeedMax As Integer = 80
    ''' <summary>
    ''' [Linotypo] Possibility that the writer made a typo in percent
    ''' </summary>
    Public LinotypoMissStrikePossibility As Integer = 1
    ''' <summary>
    ''' [Linotypo] The text columns to be printed.
    ''' </summary>
    Public LinotypoTextColumns As Integer = 3
    ''' <summary>
    ''' [Linotypo] How many characters to write before triggering the "line fill"?
    ''' </summary>
    Public LinotypoEtaoinThreshold As Integer = 5
    ''' <summary>
    ''' [Linotypo] Possibility that the Etaoin pattern will be printed in all caps in percent
    ''' </summary>
    Public LinotypoEtaoinCappingPossibility As Integer = 5
    ''' <summary>
    ''' [Linotypo] Line fill pattern type
    ''' </summary>
    Public LinotypoEtaoinType As FillType = FillType.EtaoinPattern
    ''' <summary>
    ''' [Linotypo] Possibility that the writer missed a character in percent
    ''' </summary>
    Public LinotypoMissPossibility As Integer = 10

    '-> Typewriter
    ''' <summary>
    ''' [Typewriter] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public TypewriterDelay As Integer = 50
    ''' <summary>
    ''' [Typewriter] How many milliseconds to wait before writing the text in the new screen again?
    ''' </summary>
    Public TypewriterNewScreenDelay As Integer = 3000
    ''' <summary>
    ''' [Typewriter] Text for Typewriter. Longer is better.
    ''' </summary>
    Public TypewriterWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [Typewriter] Minimum writing speed in WPM
    ''' </summary>
    Public TypewriterWritingSpeedMin As Integer = 50
    ''' <summary>
    ''' [Typewriter] Maximum writing speed in WPM
    ''' </summary>
    Public TypewriterWritingSpeedMax As Integer = 80
    ''' <summary>
    ''' [Typewriter] Shows the typewriter letter column position by showing this key on the bottom of the screen: <code>^</code>
    ''' </summary>
    Public TypewriterShowArrowPos As Boolean = True

    '-> FlashColor
    ''' <summary>
    ''' [FlashColor] Enable 255 color support. Has a higher priority than 16 color support.
    ''' </summary>
    Public FlashColor255Colors As Boolean
    ''' <summary>
    ''' [FlashColor] Enable truecolor support. Has a higher priority than 255 color support.
    ''' </summary>
    Public FlashColorTrueColor As Boolean = True
    ''' <summary>
    ''' [FlashColor] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public FlashColorDelay As Integer = 20

    '-> SpotWrite
    ''' <summary>
    ''' [SpotWrite] How many milliseconds to wait before making the next write?
    ''' </summary>
    Public SpotWriteDelay As Integer = 100
    ''' <summary>
    ''' [SpotWrite] Text for SpotWrite. Longer is better.
    ''' </summary>
    Public SpotWriteWrite As String = "Kernel Simulator"
    ''' <summary>
    ''' [SpotWrite] How many milliseconds to wait before writing the text in the new screen again?
    ''' </summary>
    Public SpotWriteNewScreenDelay As Integer = 3000

End Module
