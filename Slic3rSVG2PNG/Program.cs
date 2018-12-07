using System;
using System.Threading;
using System.IO;
using System.Globalization;

namespace Slic3rSVG2PNG
{
    class Program
    {
        private static string   INPUT     = Path.GetFullPath("print.svg");
        private static string   OUTPUT    = Path.GetFullPath("out");
        private static double   PPU       = 0.0;

        private static CultureInfo customCulture;

        static void Main(string[] args)
        {
            ParseArgs(args);

            //switch to international decimal separator
            customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            //log
            Out("Program run with: ");
            Out("...input file: " + INPUT);
            Out("...output path: " + OUTPUT);
            Out("...pixel per unit ratio: " + PPU);
        }

        private static void ParseArgs(string[] args)
        {
            for(int i = 0; i < args.Length - 1; i += 2)
            {
                string par = args[i];
                string val = args[i + 1];

                switch(par)
                {
                    case "--input":
                        INPUT = Path.GetFullPath(val);
                        break;
                    case "--output":
                        OUTPUT = Path.GetFullPath(val);
                        break;
                    case "--ppu":
                        Out(val);
                        PPU = double.Parse(val.Trim(), NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
                        break;
                }
            }
        }

        public static void Out(string s)
        {
            Console.WriteLine(s);
        }
    }
}
