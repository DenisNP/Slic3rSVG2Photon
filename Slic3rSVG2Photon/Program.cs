using System;
using System.Threading;
using System.IO;
using System.Globalization;
using System.Xml;

namespace Slic3rSVG2Photon
{
    class Program
    {
        private static string   INPUT     = Path.GetFullPath("print.svg");
        private static string   OUTPUT    = Path.GetFullPath("out");
        private static string   CONFIG    = Path.GetFullPath("config");

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
            throw new NotImplementedException();
        }

        private static void ReadFile(string iNPUT)
        {
            Out("\nReading input file...");

            var svg = new XmlDocument();
            try
            {
                svg.Load(INPUT);

            }
            catch (Exception e)
            {
                Out("Error loading file: " + e.Message);    
            }
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
