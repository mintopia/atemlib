using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace SwitcherLib
{
    public partial class Switcher
    { 
    public static uint[] CreateGraphic(int left, int right,
                    int width, int height, int alpha)
    {

            //Goal is to have this create a standard lower 3rd where script specifies size, color and text.
            // Create a writeable bitmap (which is a valid WPF Image Source

            Bitmap bitmap = new Bitmap(1280, 720);
            Graphics gr = Graphics.FromImage(bitmap);
            Rectangle pgRect = new Rectangle(left, right, left+width, right+height);
            // Create an array of pixels to contain pixel color values
            uint[] pixels = new uint[width * height];

            int red;
            int green;
            int blue;


            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int i = width * y + x;

                    red = 0;
                    green = 255 * y / height;
                    blue = 255 * (width - x) / width;
                    alpha = 255;

                    pixels[i] = (uint)((blue << 24) + (green << 16) + (red << 8) + alpha);
                    bitmap.SetPixel(x, y, Color.Red);
                }
            }

            // apply pixels to bitmap

            return pixels;

        }
    }
}
