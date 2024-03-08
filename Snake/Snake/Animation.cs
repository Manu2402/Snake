using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Snake
{
    class Animation
    {
        private Sprite[] frames; //Vettore di sprites
        private int numFrames; //Numero di frame
        private int currentFrame; //Frame corrente
        public float timer; //Timer per l'aggiornamento del frame
        private float frameDuration; //Durata del frame, di base = 1/fps
        private bool loop; //Abilitato se deve ripetere
        private bool isPlaying; //Abilitato se si sta riproducendo
        public Sprite CurrentSprite; //Sprite corrente

        public Animation(string[] sprites, float fps)
        {
            numFrames = sprites.Length; //Assegno il numero di frame
            frames = new Sprite[numFrames];
            for (int i = 0; i < numFrames; i++)
            {
                frames[i] = new Sprite(sprites[i]); //Assegno le sprites
            }
            currentFrame = 0;
            CurrentSprite = frames[currentFrame]; //Faccio partire dalla prima sprite
            if (fps <= 0) fps = 1;
            frameDuration = 1 / fps; //Assegno l'inverso degli fps
            loop = true;
            isPlaying = true;
        }

        public void Update()
        {
            if (isPlaying)
            {
                timer += Gfx.Window.DeltaTime;
                if (timer >= frameDuration)
                {
                    currentFrame++;
                    if (currentFrame >= numFrames)
                    {
                        if (loop) currentFrame = 0;
                        else
                        {
                            isPlaying = false;
                            currentFrame = numFrames - 1;
                        }
                    }
                    timer = 0f;
                    CurrentSprite = frames[currentFrame]; //Aggiorno alla fine il frame
                }
            }
        }

    }
}
