using System;
using System.Threading;
using System.IO;
using System.Globalization;

namespace Slic3rSVG2Photon
{
    class Program
    {
        private static string   INPUT           = Path.GetFullPath("print.svg");
        public static string    OUTPUT           = Path.GetFullPath("out");
        private static string   CONFIG          = Path.GetFullPath("config");

        public static float     BED_X           = 68.04f;
        public static float     BED_Y           = 120.96f;
        public static float     BED_Z           = 150f;
        public static int       SCREEN_X        = 1440;
        public static int       SCREEN_Y        = 2560;

        public static float     NORMAL_EXP_TIME = 1f;
        public static float     BOTTOM_EXP_TIME = 50f;

        public static bool      PNG_MODE        = true;

        private static CultureInfo customCulture;

        static void Main(string[] args)
        {
            ParseArgs(args);
            ReadConfig();

            //switch to international decimal separator
            customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            //log
            Out("Program run with: ");
            Out("...input file: " + INPUT);
            Out("...output path: " + OUTPUT);

            //Read svg and convert
            ReadFile(INPUT);
        }

        private static void ReadConfig()
        {
            if (!File.Exists(CONFIG)) return;

            var lines = File.ReadLines(CONFIG);
            foreach(var line in lines)
            {
                string[] pars = line.Split('=');
                if(pars.Length == 2)
                {
                    string key = pars[0];
                    string val = pars[1];

                    switch(key)
                    {
                        case "BED_X":
                            BED_X = float.Parse(val, NumberStyles.Any, CultureInfo.InvariantCulture);
                            break;
                        case "BED_Y":
                            BED_Y = float.Parse(val, NumberStyles.Any, CultureInfo.InvariantCulture);
                            break;
                        case "BED_Z":
                            BED_Z = float.Parse(val, NumberStyles.Any, CultureInfo.InvariantCulture);
                            break;
                        case "SCREEN_X":
                            SCREEN_X = int.Parse(val);
                            break;
                        case "SCREEN_Y":
                            SCREEN_Y = int.Parse(val);
                            break;
                    }
                }
            }
        }

        private static void ReadFile(string iNPUT)
        {
            Slic3rSVGReader.ReadToBitmaps(INPUT);
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
                    case "--config":
                        CONFIG = Path.GetFullPath(val);
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
