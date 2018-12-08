using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using Svg;
using System.Diagnostics;
using System.IO;

namespace Slic3rSVG2Photon
{
    public class Slic3rSVGReader
    {
        public static List<Bitmap> ReadToBitmaps(string file)
        {
            Program.Out("\nReading input file...");

            var svgFile = new XmlDocument();
            var bitmaps = new List<Bitmap>();
            
            /*try
            {*/
                //load svg file and read it's dimensions
                svgFile.Load(file);
                XmlNode root = svgFile.DocumentElement;
                double svgWidth = double.Parse(root.Attributes["width"].Value);
                double svgHeight = double.Parse(root.Attributes["height"].Value);

                //check if dimensions out of limits
                if (svgWidth > Program.BED_X || svgHeight > Program.BED_Y)
                {
                    Program.Out("Process stopped. Svg size exceeds BED dimensions. Please, make sure that svg is portrait (height >= width).");
                    return bitmaps;
                }
                
                //this is the template string for creating individual svgs from <g> objects
                string svgRoot = "<svg width=\"" + Program.BED_X + "\" height=\"" + Program.BED_Y + "\">";
            
                //calculating bounds to place image in the center of screen
                float minX = (float)((Program.BED_X - svgWidth) / 2);
                float minY = (float)((Program.BED_Y - svgHeight) / 2);
            
                //for each child node create individual svg and convert it to bitmap
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode g = root.ChildNodes[i];
                    if(g.Name == "g")
                    {
                        var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgRoot + g.OuterXml + "</svg>");
                        svgDocument.ShapeRendering = SvgShapeRendering.GeometricPrecision;

                        svgDocument.ViewBox = new SvgViewBox(-minX, -minY, Program.BED_X, Program.BED_Y);
                        Bitmap bmp = svgDocument.Draw(Program.SCREEN_X, Program.SCREEN_Y);

                        bmp.Save(Path.GetFullPath(@"img.png"));
                        break;
                    }
                    
                }
            /*}
            catch (Exception e)
            {
                Program.Out("Error loading file: " + e.Message);
            }*/

            return bitmaps;
        }
    }
}
