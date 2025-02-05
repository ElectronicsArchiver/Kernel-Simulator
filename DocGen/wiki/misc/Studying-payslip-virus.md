# Studying Payslip virus

## The May 2019 Payslip virus, and its usage of some of KS's features

On May 17, 2019, there was a new virus compiled using VB.NET, called Payslip, that manages to obtain some of KS's features, like the flagging system, `TextWriterColor`, etc. on its source code to avoid detection. Most of you have found viruses on shady sites, so you need to avoid these kinds of sites. We are here to study its disassembly of Payslip.

According to `ILDASM.EXE`, there are about 6 namespaces, called `Flags`, `Kernel`, `KernelTools`, `TextWriterColor`, `Translate`, and `xGd`. As you can see below, there are 24 flags. So, it's based on an older version of KS.

![Study of Flags](https://i.imgur.com/eeY3agy.png)

The rest of the pictures are for remaining namespaces and what's inside:

![Kernel](https://i.imgur.com/1MKQvGS.png)
![KernelTools](https://i.imgur.com/hurODBE.png)
![TWC](https://i.imgur.com/1L2645q.png)
![Translate](https://i.imgur.com/mDvrnUi.png)

Now, let's use JetBrains' dotPeek for further analysis. When we went to `Flags`, we saw all of the usual flags. Good.

However, all of the subs on the Kernel namespace are not found, except these properties:
```c#
  public static string KernelVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
  public static string[] BootArgs;
  public static string[] AvailableArgs;
  public static string[] availableCMDLineArgs;
  public static string MOTDMessage;
  public static string HName;
  public static string MAL;
  public static string EnvironmentOSType;
  static Kernel() // Generated by JetBrains dotPeek
  {
    string[] strArray1 = new string[Convert.ToInt32(3.41886116991581 + Math.Sqrt(2.5))];
    strArray1[0] = "quiet";
    strArray1[1] = "cmdinject";
label_1:
    int num1 = 322610553;
    string[] strArray2;
    while (true)
    {
      uint num2;
      switch ((num2 = (uint) (num1 ^ 1915212704)) % 9U)
      {
        case 0:
          num1 = (int) num2 * -685199074 ^ 818517677;
          continue;
        case 1:
          strArray1[Convert.ToInt32(5.41421356237309 - Math.Sqrt(2.0))] = "help";
          Kernel.AvailableArgs = strArray1;
          num1 = (int) num2 * -85093751 ^ -1759328859;
          continue;
        case 2:
          goto label_3;
        case 3:
          strArray2[0] = "createConf";
          strArray2[1] = "promptArgs";
          num1 = (int) num2 * 1980815017 ^ -1632831499;
          continue;
        case 4:
          Kernel.EnvironmentOSType = Environment.OSVersion.ToString();
          num1 = (int) num2 * -1749997108 ^ -1153977823;
          continue;
        case 5:
          strArray2 = new string[Convert.ToInt32(2.0 * 1.5)];
          num1 = (int) num2 * 330882450 ^ 1511576150;
          continue;
        case 6:
          goto label_1;
        case 7:
          strArray2[Convert.ToInt32(3.0 - Math.Ceiling(1.0))] = "testMod";
          Kernel.availableCMDLineArgs = strArray2;
          num1 = (int) num2 * 1472650476 ^ 1747686761;
          continue;
        case 8:
          strArray1[Convert.ToInt32(3.0 - Math.Sqrt(1.0))] = "debug";
          strArray1[Convert.ToInt32(5.0 - Math.Round(1.5))] = "maintenance";
          num1 = (int) num2 * -1989761204 ^ -1809828870;
          continue;
        default:
          goto label_11;
      }
    }
label_3:
    return;
label_11:;
  }
```
Not good. In the `KernelTools`, 7 subs and 1 property are found. They represent the full version of these subs in an older version of KS. And now, let's go to writers.
```c#
  public static void Wdbg(string text, params object[] vars)
  {
    int num = Flags.DebugMode ? 1 : 0;
  }

  public static void W(object text, string colorType, params object[] vars)
  {
  }

  public static void Wln(object text, string colorType, params object[] vars)
  {
  }
```
It seems that W() and Wln() writing subs are emptied, where the Wdbg() is filled with the check for debugging mode and set the result to the `num` variable. Nothing else.

The last namespace we're looking at is `Translate`. We have 2 subs and 3 fields. The current language is `eng`, and the languages for the virus version are `chi`, `eng`, `fre`, `ger`, `ind`, `ptg`, and `spa`.

According to [this report](https://www.hybrid-analysis.com/sample/756b94b872cada97c6ebcbc65c47734e3238f171db719d428a42f6ac8bc93e4f/5cde9cce028838e49ee56626) and [this report](https://www.hybrid-analysis.com/sample/756b94b872cada97c6ebcbc65c47734e3238f171db719d428a42f6ac8bc93e4f/5cdf2281028838e98fe56626), in the strings lookup, we have found:

```
---===+++> Welcome to the kernel | Version {0} <+++===---
019 EoflaOE This program comes with ABSOLUTELY NO WARRANTY, not even MERCHANTABILITY or FITNESS for particular purposes. This is free software, and you are welcome to redistribute it under certain conditions; See COPYING file in source code.
[{0}] dpanic: {1} -- Rebooting in {2} seconds...
[{0}] panic: Reboot enabled due to error level being {0}.
[{0}] panic: Time to reboot: {1} seconds, exceeds 1 hour. It is set to 1 minute.
[{0}] panic: {1} -- Press any key to continue using the kernel.
[{0}] panic: {1} -- Press any key to shutdown.
[{0}] panic: {1} -- Rebooting in {2} seconds...
\r\n Kernel Simulator Copyright (C) 2018-2019 EoflaOE\r\n This program comes with ABSOLUTELY NO WARRANTY, not even \r\n MERCHANTABILITY or FITNESS for particular purposes.\r\n This is free software, and you are welcome to redistribute it\r\n under certain conditions; See COPYING file in source code.\r\n
DOUBLE PANIC: Error Type {0} invalid.
DOUBLE PANIC: Kernel bug: {0}
DOUBLE PANIC: Reboot Time exceeds maximum allowed {0} error reboot time. You found a kernel bug.
E PANIC: Reboot Time exceeds maximum allowed {0} error reboot time. You found a kernel bug.
ebooting in {2} seconds...
elcome to the kernel | Version {0} <+++===---
Kernel initialized, version {0}.
ranslating string to {0}: {1}
Translating string to {0}: {1}
{1} -- Press any key to continue using the kernel.
} seconds...
{0} isn't in language list
utting down...
Shutting down...
s, exceeds 1 hour. It is set to 1 minute.
Running on {0}
rnel bug: {0}
Rebooting...
Power management has the argument of {0}
OS: {0}
ment has the argument of {0}
Garbage collector finished
DOUBLE PANIC:
ctor finished
```

So, beware of viruses, and don't go to shady sites.