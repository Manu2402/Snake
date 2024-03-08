using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Snake
{

    class Apple
    {
        private Vector2 position; //Coordinate
        //private Color color; --> Colore
        private int halfWidth; //Metà larghezza
        private int halfHeight; //Metà altezza

        private Sprite sprite; //Sprite della mela
        private Animation animation; //Animazione
        private string[] fileNames;
        private float fps;
        
        public Apple() 
        { 
            //color = ColorsFactory.GetColor(Colors.Red); --> Setto il colore della mela

            fps = 8; //Dico quanti frame deve durare l'animazione
            fileNames = new string[2];
            for (int i = 0; i < fileNames.Length; i++) //Carico i path delle texture
            {
                fileNames[i] = $"Assets/apple_{i + 1}.png";
            }
            animation = new Animation(fileNames, fps); //Creo l'animation (gestisce il flusso di frame MA NON LI APPLICA)
            sprite = animation.CurrentSprite; //Applico il currentSprite
            halfWidth = (int)(sprite.Width * 0.5f); //Nuove dimensioni
            halfHeight = (int)(sprite.Height * 0.5f);
            Update();
        }
        
        public float X { get { return position.x; } set { position.x = value; } }
        public float Y { get { return position.y; } set { position.y = value; } }

        public void Update()
        {
            Random r = new Random(); //Random number per lo spawn/respawn
            do //Ciclo che applica le coordinate di spawn, devono essere conformi al modello di rappresentazione (griglia di pixel grandi 30x30)
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
            float positionCenterX = position.x - halfWidth; //Imposto il pivot al centro della mela
            float positionCenterY = position.y - halfHeight;

            //Disegno
            //Gfx.DrawRect((int)positionCenterX, (int)positionCenterY, halfWidth * 2, halfHeigth * 2, color);
            Gfx.DrawSprite(sprite, (int)positionCenterX, (int)positionCenterY);
        }

    }

}
