using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Snake
{
    static class Gfx
    {
        public static Window Window;
        public static int distFromBorder;

        static Gfx()
        {
            Window = new Window(855, 855, "Snake", PixelFormat.RGB);
            distFromBorder = 7;
        }

        public static void PutPixel(int x, int y, Color color)
        {
            if (x < 0 || x > Gfx.Window.Width || y < 0 || y > Gfx.Window.Height) return;
            
            int pixelIndex = (x + y * Gfx.Window.Width) * 3;
            Window.Bitmap[pixelIndex] = color.R;
            Window.Bitmap[pixelIndex + 1] = color.G;
            Window.Bitmap[pixelIndex + 2] = color.B;
        }

        public static void ClearScreen()
        {
            for (int i = 0; i < Window.Bitmap.Length; i++)
            {
                Window.Bitmap[i] = 0;
            }
        }

        // Draw a rectangle given base and height (removing the pixel in the corners).
        public static void DrawRect(int x, int y, int width, int height, Color color) 
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!(i == 0 && j == 0 || i == height - 1 && j == 0 || i == 0 && j == width - 1 || i == height - 1 && j == width - 1))
                    {
                        PutPixel(x + j, y + i, color);
                    }
                }
            }
        }

        // Draw a rectangle given base and height.
        public static void DrawRectWithoutCorners(int x, int y, int width, int height, Color color)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    PutPixel(x + j, y + i, color);
                }
            }
        }

        public static void DrawSprite(Sprite sprite, int spriteX, int spriteY)
        {
            int x, y;
            
            // Scroll the sprite.
            for (int i = 0; i < sprite.Height; i++) 
            {
                for (int j = 0; j < sprite.Width; j++)
                {
                    x = spriteX + i;
                    y = spriteY + j;
                    if (x < 0 || x >= Gfx.Window.Width || y < 0 || y >= Gfx.Window.Height) return;
                    
                    int canvasIndex = (x + y * Gfx.Window.Width) * 3; // R, G, B
                    // Takes a sample of the RGBA values of the window and sprite.
                    int spriteIndex = (i + j * sprite.Width) * 4; //R, G, B, [A] (alpha)
                    byte spriteR = sprite.Bitmap[spriteIndex];
                    byte spriteG = sprite.Bitmap[spriteIndex + 1];
                    byte spriteB = sprite.Bitmap[spriteIndex + 2];
                    byte spriteA = sprite.Bitmap[spriteIndex + 3];
                    byte winR = Gfx.Window.Bitmap[canvasIndex];
                    byte winG = Gfx.Window.Bitmap[canvasIndex + 1];
                    byte winB = Gfx.Window.Bitmap[canvasIndex + 2];
                    float alpha = spriteA / 255f; //[0...1] --> I find the % of transparency.
                    // Apply % transparency to the two objects (window and sprite).
                    byte blendR = (byte)(spriteR * alpha + winR * (1 - alpha));
                    byte blendG = (byte)(spriteG * alpha + winG * (1 - alpha));
                    byte blendB = (byte)(spriteB * alpha + winB * (1 - alpha));
                    // Modify the bitmap vector of the window with the final (blended) colors.
                    Window.Bitmap[canvasIndex] = blendR;
                    Window.Bitmap[canvasIndex + 1] = blendG;
                    Window.Bitmap[canvasIndex + 2] = blendB;
                }
            }
        }
    }
}
