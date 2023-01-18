using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace B2SWindowPunch
{
    using HWND = IntPtr;
    using HRGN = IntPtr;
    using HGDIOBJ = IntPtr;
    
    internal class Program
    {
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
            private static extern IntPtr GetShellWindow();

        }

            public class Options
        {
            public string destination { get; set; }
            public string cutter { get; set; }

        }

        private static int Main(string[] args)
        {
            string[] cargs = Environment.GetCommandLineArgs();
            Options inputOptions = new Options();

            if (cargs.Length != 3)
            {
                PrintUsage();
                return 1;
            }
            inputOptions.destination = cargs[1].ToString();
            inputOptions.cutter = cargs[2].ToString();
            return RunAndReturnExitCode(inputOptions);
        }
        private static void PrintUsage()
        {
            Console.WriteLine("B2SWindowPunch");
            Console.WriteLine("©2023 Richard Ludwig (Jarr3) and the B2S Team\n");
            Console.WriteLine("Usage: B2SWindowPunch \"Destination regex\" \"Cutter Regex\" \n");
            Console.WriteLine("Destination and the cutter window string has to be valid regular expressions.\n");
            Console.WriteLine("E.g. \n Cut holes in the \"B2S Backglass Server\" form using \"Virtual DMD\" and all \"PUPSCREEN\" forms as two regular expressions:");
            Console.WriteLine("B2SWindowPunch.exe \"^B2S Backglass Server$\" \"^Virtual DMD$|^PUPSCREEN[0-9]+$\"");
        }
        private static int RunAndReturnExitCode(Options options)
        {
            const int RGN_DIFF = 4;

            Dictionary <HWND, string> windows = (Dictionary<HWND, string>)OpenWindowGetter.GetOpenWindows();

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
                IntPtr handle = window.Key;
                string title = window.Value;

                if ( rxDest.IsMatch(title) ) {
                    Console.WriteLine("Destination {0}: {1}", handle, title);
                    GetWindowRect(handle, out RECT VPWR);

                    IntPtr FRegion = CreateRectRgn(0, 0, VPWR.Right, VPWR.Bottom);

                    foreach (KeyValuePair<IntPtr, string> cutwindow in windows)
                    {
                        IntPtr cuthandle = cutwindow.Key;
                        string cuttitle = cutwindow.Value;

                        if ( cuthandle != handle && rxCutter.IsMatch(cuttitle) )
                        {
                            Console.WriteLine("   cutout {0}: {1}", cuthandle, cuttitle);

                            GetWindowRect(cuthandle, out RECT PWR);
                            HRGN Excl = CreateRectRgn(PWR.Left - VPWR.Left, PWR.Top - VPWR.Top, PWR.Right - VPWR.Left, PWR.Bottom - VPWR.Top);
                            CombineRgn(FRegion, FRegion, Excl, RGN_DIFF);
                            DeleteObject(Excl);
                        }

                    }
                    SetWindowRgn(handle, FRegion, true);
                    DeleteObject(FRegion);
                }
            }


            Console.WriteLine("\nListing all available windows:\n");
            foreach (var window in windows.OrderBy(p => p.Value))
            {
                Console.WriteLine($"{window.Value}");
            }

            return 0;
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

    }
}
