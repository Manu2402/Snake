using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Field
    {
        private Pixel[] sprite; //Pixel attivi per il disegno del background
        private int pixelSize; //Dimensione del pixel
        private Color colorBorder;

        public Field()
        {
            colorBorder = ColorsFactory.GetColor(Colors.Green);
            byte[] pixels = new byte[Gfx.Window.Width * Gfx.Window.Height];
            for (int i = 0; i < Gfx.Window.Height; i++) //Assegno 1 se il pixel va disegnato, 0 se non va disegnato
            {
                for (int j = 0; j < Gfx.Window.Width; j++)
                {
                    //Coloro il bordo spostandolo leggermente più al centro
                    if (i == 4 && (j > 3 && j < Gfx.Window.Width - 4) || j == 4 && (i > 3 && i < Gfx.Window.Height - 4) || i == Gfx.Window.Height - 5 && (j > 3 && j < Gfx.Window.Width - 4) || j == Gfx.Window.Width - 5 && (i > 3 && i < Gfx.Window.Height - 4)) 
                    {
                        pixels[j + (i * Gfx.Window.Width)] = 1;
                    }
                    else pixels[j + (i * Gfx.Window.Width)] = 0;
                }
            }
            int numPixels = 0; //Calcolo il numero di pixel attivi
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] == 1) numPixels++;
            }
            sprite = new Pixel[numPixels]; //Alloco il numero di pixel attivi nel vettore "sprite"
            int verticalPixels = (int)(Gfx.Window.Height); //Calcolo il numero di pixel per ogni dimensione (x, y)
            int horizontalPixels = (int)(Gfx.Window.Width); //Un pixel avrà dimensione 1*1 px
            pixelSize = Gfx.Window.Height / verticalPixels; //Dim: 2px * 2px
            float startX = 0; //La posizione iniziale sarà a 0, 0
            float startY = 0;
            int index = 0; //Indice che indica l'n-esimo pixel
            for (int i = 0; i < pixels.Length; i++) //Scorro il rettangolo di pixel
            {
                if (i % horizontalPixels == 0 && i != 0) //Se arrivo in fondo al "rettangolo", sposto la Y in basso
                {
                    startY += pixelSize;
                }
                if (pixels[i] != 0) //Se il pixel esiste...
                {
                    float x = startX + pixelSize * (i % horizontalPixels); //X del pixel corrente
                    sprite[index] = new Pixel(new Vector2(x, startY), pixelSize, colorBorder); //Alloco il pixel e lo imposto
                    index++; //Aumento l'indice del vettore "sprite"
                }
            }
        }

        public void Draw()
        {
            //Gfx.DrawRect(0, 0, Gfx.Window.Width, Gfx.Window.Height, color); //Lo disegno (ultimo piano, come sfondo)
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i].Draw();
            }
        }

    }
}
