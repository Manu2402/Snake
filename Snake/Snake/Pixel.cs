using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Pixel
    {
        private int size;
        private Vector2 position;
        private Color color;
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

        public void Translate(float x, float y)
        {
            position.x += x;
            position.y += y;
        }

        public void Draw()
        {
            // Pivot on left-top corner.
            Gfx.DrawRectWithoutCorners((int)position.x, (int)position.y, size, size, color);
        }

        public void DrawRect()
        {
            // Pivot on left-top corner.
            Gfx.DrawRect((int)position.x, (int)position.y, size, size, color); 
        }
    }
}
