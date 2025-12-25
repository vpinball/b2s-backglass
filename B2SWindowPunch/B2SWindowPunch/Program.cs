using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace B2SWindowPunch
{
    using HWND = IntPtr;
    using HRGN = IntPtr;
    using HGDIOBJ = IntPtr;
    
    internal class Program
    {
        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        /// <summary>Contains functionality to get all the open windows.</summary>
        public static class OpenWindowGetter
        {
            /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
            /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
            public static IDictionary<HWND, string> GetOpenWindows()
            {
                HWND shellWindow = GetShellWindow();
                Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

                EnumWindows(delegate (HWND hWnd, int lParam)
                {
                    if (hWnd == shellWindow) return true;
                    if (!IsWindowVisible(hWnd)) return true;
                    if (IsIconic(hWnd)) return true;

                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder builder = new StringBuilder(length);
                    GetWindowText(hWnd, builder, length + 1);

                    windows[hWnd] = builder.ToString();
                    return true;

                }, 0);

                return windows;
            }
            private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

            [DllImport("USER32.DLL")]
            private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

            [DllImport("USER32.DLL")]
            private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("USER32.DLL")]
            private static extern int GetWindowTextLength(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern bool IsWindowVisible(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern bool IsIconic(HWND hWnd);

            [DllImport("USER32.DLL")]
            private static extern IntPtr GetShellWindow();

        }

            public class Options
        {
            public string destination { get; set; }
            public string cutter { get; set; }
            public int cutterRadius { get; set; }

        }

        private static int Main(string[] args)
        {
            string[] cargs = Environment.GetCommandLineArgs();
            Options inputOptions = new Options();

            if (cargs.Length < 3 || cargs.Length > 4)
            {
                PrintUsage();
                return 1;
            }
            inputOptions.destination = cargs[1].ToString();
            inputOptions.cutter = cargs[2].ToString();
            if (cargs.Length == 4)
            {
                int parsedRadius;
                if (!int.TryParse(cargs[3], out parsedRadius))
                {
                    PrintUsage();
                    return 1;
                }
                inputOptions.cutterRadius = parsedRadius;
            }
            return RunAndReturnExitCode(inputOptions);
        }
        private static void PrintUsage()
        {
            Console.WriteLine("B2SWindowPunch");
            Console.WriteLine("© 2023-2024 Richard Ludwig (Jarr3) and the B2S Team\n");
            Console.WriteLine("Usage: B2SWindowPunch \"Destination regex\" \"Cutter Regex\" [cutterRadius]\n");
            Console.WriteLine("Destination and Cutter window string has to be valid regular expressions. cutterRadius is optional and enables rounded cutouts when > 0.\n");
            Console.WriteLine("E.g. \n Cut holes in the \"B2S Backglass Server\" form using \"Virtual DMD\" and all \"PUPSCREEN\" forms as two regular expressions with 12px radius:");
            Console.WriteLine("B2SWindowPunch.exe \"^B2S Backglass Server$\" \"^Virtual DMD$|^PUPSCREEN[0-9]+$\" 12");
        }
        private static int RunAndReturnExitCode(Options options)
        {
            const int RGN_DIFF = 4;

            Dictionary<HWND, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();

            if (options.destination.Length > 0 && options.cutter.Length > 0)
            {
                Regex rxDest;
                Regex rxCutter;
                try
                {
                    rxDest = new Regex(options.destination, RegexOptions.Compiled);
                    rxCutter = new Regex(options.cutter, RegexOptions.Compiled);
                }
                catch (ArgumentException)
                {
                    PrintUsage();
                    return 1;
                }

                Console.WriteLine($"Current destination: {options.destination}");

                foreach (KeyValuePair<IntPtr, string> window in windows)
                {
                    IntPtr destHandle = window.Key;
                    string destTitle = window.Value;

                    if (rxDest.IsMatch(destTitle))
                    {
                        Screen destScreen = Screen.FromHandle(destHandle);
                        Size destScreenSize = TrueResolution(destScreen.DeviceName);
                        Single destFactor = (float)destScreenSize.Height / destScreen.Bounds.Height;

                        Console.WriteLine($"Destination: {destTitle} on '{destScreen.DeviceName}' ({destFactor * 100} %)");
                        RECT destRect = GetWindowRectWithShadow(destHandle);
                        int destWidth = destRect.Right - destRect.Left;
                        int destHeight = destRect.Bottom - destRect.Top;

                        IntPtr destRegion = CreateRectRgn(0, 0, destWidth, destHeight);

                        foreach (KeyValuePair<IntPtr, string> cutwindow in windows)
                        {
                            IntPtr cutHandle = cutwindow.Key;
                            string cutTitle = cutwindow.Value;

                            if (cutHandle != destHandle && rxCutter.IsMatch(cutTitle))
                            {
                                Screen cutScreen = Screen.FromHandle(cutHandle);
                                if (destScreen.DeviceName == cutScreen.DeviceName)
                                {
                                    Console.WriteLine($"   cutout: {cutTitle}");

                                    RECT cutRect = GetWindowRectWithShadow(cutHandle);
                                    int relativeLeft = cutRect.Left - destRect.Left;
                                    int relativeTop = cutRect.Top - destRect.Top;
                                    int relativeRight = cutRect.Right - destRect.Left;
                                    int relativeBottom = cutRect.Bottom - destRect.Top;

                                    HRGN cutRegion = options.cutterRadius > 0
                                        ? CreateRoundRectRgn(
                                            relativeLeft,
                                            relativeTop,
                                            relativeRight,
                                            relativeBottom,
                                            options.cutterRadius * 2,
                                            options.cutterRadius * 2)
                                        : CreateRectRgn(
                                            relativeLeft,
                                            relativeTop,
                                            relativeRight,
                                            relativeBottom);
                                    CombineRgn(destRegion, destRegion, cutRegion, RGN_DIFF);
                                    DeleteObject(cutRegion);
                                }
                            }
                        }
                        SetWindowRgn(destHandle, destRegion, true);
                        DeleteObject(destRegion);
                    }
                }
            }

            Console.WriteLine("\nListing all available windows:\n");
            foreach (var window in windows.OrderBy(p => p.Value))
            {
                IntPtr windowhandle = window.Key;
                string windowtitle = window.Value;
                RECT PWR = GetWindowRectWithShadow(windowhandle);
                Console.WriteLine($"{windowtitle}: ({PWR.Left},{PWR.Top})-({PWR.Right},{PWR.Bottom})");
            }

            return 0;
        }

        private static RECT GetWindowRectWithShadow(IntPtr handle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, DWMWA_EXTENDED_FRAME_BOUNDS, out rect, Marshal.SizeOf(typeof(RECT)));
            if (result == 0)
            {
                return rect;
            }

            GetWindowRect(handle, out rect);
            return rect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            //public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }


            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(HWND hWnd, HRGN hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CombineRgn(IntPtr hrgnDst, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int iMode);
        [DllImport("gdi32.dll")]
        private static extern IntPtr DeleteObject(HGDIOBJ ho);
        
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        private const int DESKTOPHORZRES = 118;
        private const int DESKTOPVERTRES = 117;

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDCA(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        public string ShortDevice(string device)
        {
            return device.Replace("\\", "").Replace(".\\", "");
        }

        private static string SafeReadRegistry(string keyname, string valuename, string defaultvalue)
        {
            try
            {
                return (string)Registry.CurrentUser.OpenSubKey(keyname)?.GetValue(valuename, defaultvalue) ?? defaultvalue;
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        private static Size TrueResolution(IntPtr hwnd)
        {
            using (Graphics g = Graphics.FromHwnd(hwnd))
            {
                IntPtr hdc = g.GetHdc();
                Size TrueScreenSize = new Size(GetDeviceCaps(hdc, DESKTOPHORZRES), GetDeviceCaps(hdc, DESKTOPVERTRES));
                g.ReleaseHdc(hdc);

                return TrueScreenSize;
            }
        }

        private static Size TrueResolution(string deviceName)
        {
            IntPtr screen = CreateDCA(deviceName, null, null, IntPtr.Zero);
            Size TrueScreenSize = new Size(GetDeviceCaps(screen, DESKTOPHORZRES), GetDeviceCaps(screen, DESKTOPVERTRES));
            DeleteDC(screen);

            return TrueScreenSize;
        }
        private static int UseFactor(int value, Single factor)
        {
            return (int)(value * factor);
        }
    }

}
