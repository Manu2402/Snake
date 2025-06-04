using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //Rain that is activated every 10 points made
    
    class Rain 
    {
        private Pixel[] sprites;
        private int numPixels;
        private int pixelSize;
        private Vector2 position;
        private Vector2 positionStart;
        private Color color;

        public Rain()
        {
            positionStart = new Vector2(0f, -20f);
            numPixels = 420;
            sprites = new Pixel[numPixels];
            pixelSize = 5;
            color = ColorsFactory.GetColor(Colors.LightBlue);
            
            for (int i = 0; i < numPixels; i++)
            {
                if (i % (numPixels * 0.5f) == 0)
                {
                    position = positionStart;
                }
                
                position.x += 10f;
                sprites[i] = new Pixel(position, pixelSize, color);
            }
        }

        // Activate them when they leave the screen.
        public void SetRainTrue()
        {
            for (int i = 0; i < numPixels; i++)
            {
                if (!sprites[i].IsAlive)
                {
                    sprites[i].Y = positionStart.y;
                    sprites[i].IsAlive = true;
                }
            }
        }

        // Take them back to the starting place when they are about to leave the screen.
        public void SetRainStartCollision()
        {
            for (int i = 0; i < numPixels; i++)
            {
                if (sprites[i].Y + sprites[i].Size > Gfx.Window.Height)
                {
                    sprites[i].IsAlive = false;
                }
            }
        }

        // Add with a random number the speed of the rain fall.
        public void Update()
        {
            float velocity;
            
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].IsAlive)
                {
                    velocity = RandomGenerator.GetRandomFloat();
                    sprites[i].Y += velocity * Game.Gravity * Gfx.Window.DeltaTime;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].IsAlive)
                {
                    sprites[i].DrawRect();
                }
            }
        }
    }
}
