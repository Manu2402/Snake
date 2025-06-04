using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Field
    {
        // Active pixels.
        private Pixel[] sprite; 
        private int pixelSize; 
        private Color colorBorder;

        public Field()
        {
            colorBorder = ColorsFactory.GetColor(Colors.Green);
            byte[] pixels = new byte[Gfx.Window.Width * Gfx.Window.Height];
            for (int i = 0; i < Gfx.Window.Height; i++) //Assegno 1 se il pixel va disegnato, 0 se non va disegnato
            {
                // • 1: Pixel Draw
                // • 0: Pixel Not Draw
                
                for (int j = 0; j < Gfx.Window.Width; j++)
                {
                    // Border. Did with magic numbers.
                    if (i == 4 && (j > 3 && j < Gfx.Window.Width - 4) || j == 4 && (i > 3 && i < Gfx.Window.Height - 4) || i == Gfx.Window.Height - 5 && (j > 3 && j < Gfx.Window.Width - 4) || j == Gfx.Window.Width - 5 && (i > 3 && i < Gfx.Window.Height - 4)) 
                    {
                        pixels[j + (i * Gfx.Window.Width)] = 1;
                    }
                    else pixels[j + (i * Gfx.Window.Width)] = 0;
                }
            }
            
            // Active pixels.
            int numPixels = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] == 1) numPixels++;
            }
            
            sprite = new Pixel[numPixels]; // Memory allocation.
            
            // Calculate the number of pixels for each dimension (x, y).
            int verticalPixels = (int)(Gfx.Window.Height); 
            // A pixel will have size 1px * 1px.
            int horizontalPixels = (int)(Gfx.Window.Width); 
            
            // Size: 2px * 2px
            pixelSize = Gfx.Window.Height / verticalPixels;
            
            float startX = 0; 
            float startY = 0;
            
            int index = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                // If i get to the bottom of the “rectangle” i move the Y to the bottom.
                if (i % horizontalPixels == 0 && i != 0)
                {
                    startY += pixelSize;
                }
                
                if (pixels[i] != 0)
                {
                    // Compute current X.
                    float x = startX + pixelSize * (i % horizontalPixels);
                    sprite[index] = new Pixel(new Vector2(x, startY), pixelSize, colorBorder);
                    index++;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i].Draw();
            }
        }
    }
}
