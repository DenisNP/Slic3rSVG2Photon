using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;

namespace Slic3rSVG2Photon
{
    public class Slic3rSVGReader
    {
        public static List<Bitmap> ReadToBitmaps(string file)
        {
            Program.Out("\nReading input file...");

            var svg = new XmlDocument();
            try
            {
                svg.Load(file);

            }
            catch (Exception e)
            {
                Program.Out("Error loading file: " + e.Message);
            }
        }
    }
}
