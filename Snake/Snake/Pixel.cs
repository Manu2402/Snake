using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Pixel
    {
        private int size; //Dimensione del pixel
        private Vector2 position; //Posizione del pixel
        private Color color; //Colore del pixel
        public bool IsAlive;

        public float Y { get { return position.y; } set { position.y = value; } }
        public int Size { get { return size; } }

        public Pixel(Vector2 position, int size, Color color) 
        {
            this.position = position;
            this.size = size;
            this.color = color;
            IsAlive = true;
        }

        public void Translate(float x, float y) //Movimento per il disegno
        {
            position.x += x;
            position.y += y;
        }

        public void Draw()
        {
            //La posizione sarà l'angolo in alto a sinistra
            Gfx.DrawRectWithoutCorners((int)position.x, (int)position.y, size, size, color); //Disegno
        }

        public void DrawRect()
        {
            //La posizione sarà l'angolo in alto a sinistra
            Gfx.DrawRect((int)position.x, (int)position.y, size, size, color); //Disegno
        }

    }
}
