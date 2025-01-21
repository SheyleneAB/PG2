using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class BitmapWriter
    {
        private const int drawingFactor = 8;
        private string path = @"C:\Users\elyne\PG2\World";

        public byte[] DrawWorld(int[,] world)
        {
            Color[] cvalues = new Color[] { Color.Green, Color.Red, Color.Yellow, Color.Blue, Color.Cyan, Color.GreenYellow, Color.Gold, Color.Ivory, Color.NavajoWhite };
            Bitmap bm = new Bitmap(world.GetLength(0) * drawingFactor, world.GetLength(1) * drawingFactor);

            using (Graphics g = Graphics.FromImage(bm))
            {
                int delta = drawingFactor / 2;
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    for (int y = 0; y < world.GetLength(1); y++)
                    {
                        int p1x = x * drawingFactor + delta;
                        int p1y = y * drawingFactor + delta;
                        if (world[x, y] >= 0)
                        {
                            Color color = cvalues[world[x, y] % cvalues.Length];
                            Pen pen = new Pen(color, 1);
                            g.DrawRectangle(pen, p1x - delta, p1y - delta, drawingFactor, drawingFactor);
                            if (world[x, y] != 0)
                            {
                                g.FillRectangle(new SolidBrush(color), p1x - delta, p1y - delta, drawingFactor, drawingFactor);
                            }
                        }
                    }
                }
            }

            // Save to file
            string filePath = Path.Combine(path, "world.jpg");
            bm.Save(filePath, ImageFormat.Jpeg);

            // Convert the bitmap to a byte array
            using (MemoryStream ms = new MemoryStream())
            {
                bm.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }

}

