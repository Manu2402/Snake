using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aiv.Draw;

namespace Snake
{
    class Apple
    {
        private Vector2 position;
        // private Color color;
        private int halfWidth;
        private int halfHeight;

        private Sprite sprite;
        private Animation animation;
        private string[] fileNames;
        private float fps;
        
        public Apple() 
        { 
            // color = ColorsFactory.GetColor(Colors.Red);

            fps = 8; // Number of FPS.
            fileNames = new string[2];
            for (int i = 0; i < fileNames.Length; i++) // Load textures path.
            {
                fileNames[i] = $"Assets/apple_{i + 1}.png";
            }
            
            animation = new Animation(fileNames, fps);
            sprite = animation.CurrentSprite;
            
            halfWidth = (int)(sprite.Width * 0.5f);
            halfHeight = (int)(sprite.Height * 0.5f);
            
            Update();
        }
        
        public float X { get { return position.x; } set { position.x = value; } }
        public float Y { get { return position.y; } set { position.y = value; } }

        public void Update()
        {
            Random r = new Random();
            
            // Loop that applies spawn coordinates, must conform to the representation model:
            // (grid of 30x30 "pixels").
            do 
            {
                position.x = r.Next(halfWidth + Gfx.distFromBorder, Gfx.Window.Width - halfWidth - Gfx.distFromBorder);
                position.y = r.Next(halfHeight + Gfx.distFromBorder, Gfx.Window.Height - halfHeight - Gfx.distFromBorder);
            } while (position.x % 15 != 0 || position.x % 30 == 0 || position.y % 15 != 0 || position.y % 30 == 0);
            
            position.x += Gfx.distFromBorder;
            position.y += Gfx.distFromBorder;
        }

        public void UpdateFrame() 
        {
            animation.Update();
            sprite = animation.CurrentSprite;
        }

        public void Draw()
        {
            // Sets the pivot to the center of the sprite.
            float positionCenterX = position.x - halfWidth;
            float positionCenterY = position.y - halfHeight;
            
            Gfx.DrawSprite(sprite, (int)positionCenterX, (int)positionCenterY);
        }
    }
}
