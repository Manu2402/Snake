using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Rain //Pioggia che si attiva ogni 10 punti effettuati
    {
        private Pixel[] sprites; //Pixel per creare la pioggia
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

        public void SetRainTrue() //Li attivo quando escono dallo schermo
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

        public void SetRainStartCollision() //Li riporto al posto di partenza quando stanno per uscire dallo schermo
        {
            for (int i = 0; i < numPixels; i++)
            {
                if (sprites[i].Y + sprites[i].Size > Gfx.Window.Height)
                {
                    sprites[i].IsAlive = false;
                }
            }
        }

        public void Update() //Aggiorno con un numero randomico la velocità della caduta della pioggia
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
