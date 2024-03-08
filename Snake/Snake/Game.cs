using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class Game
    {
        public static Apple Apple;
        public static SnakeBody Snake;
        public static Field Field;
        public static Rain Rain;
        public static float Gravity; //Gravità

        public static void Init()
        {
            Apple = new Apple(); //Genero la mela
            Snake = new SnakeBody();
            Field = new Field(); //Genero il campo
            Rain = new Rain();
            Gravity = 5; //5px/s^2
            Console.WriteLine("Benvenuto in Snake! Non è un gioco ufficiale, è un progetto creato unicamente a scopo didattico come applicazione dei concetti. " +
                "Questa versione di Snake è un miscuglio di operazioni e prototipi semplici e poco ottimizzati, quindi non c'è nulla da aspettarsi. " +
                "Le regole sono uguali alle originali: controlli un serpente, e devi fargli mangiare più frutta possibile. Più frutta mangia e più il serpente si " +
                "allunga. L'obiettivo è mangiare più mele possibili evitando di andare a sbattere contro i muri o contro il serpente stesso." +
                " L'unica differenza è che, non essendoci HUD per il conteggio dei punti (ma solo la console)," +
                "ogni 10 mele mangiate riceverai un \"avviso\" particolare (nulla di che, una pioggierella di pixel). Detto ciò, buon divertimento! \n" +
                "Ah e prima che mi scordi, salvati il tuo punteggio su un foglio di carta o Excel, poco importa, perchè questo gioco non è dotato di database :)" +
                "\n\nComandi:\nW, A, S, D per il movimento (Su, Sinistra, Giù, Destra)");
        }

        public static void GameOver()
        {
            Gfx.Window.Close(); //Si chiude la finestra, smette di ciclare il game loop e finisce il gioco
            //mostrando il punteggio finale.
        }

        public static void Play()
        {
            while (Gfx.Window.IsOpened)
            {
                //INPUT
                Snake.Input();

                //UPDATE
                Timer.RunTimer(); //Starto il timer (serpente)
                Snake.Update();
                Snake.CheckCollisionSnake(Apple); //Controllo le collisioni della testa del serpente con la mela
                if (Snake.CheckSnakeBodyCollision()) GameOver(); //Se il serpente collide con se stesso, la partita finisce
                Timer.CheckTimer();
                Apple.UpdateFrame();
                if (Score.ScorePT % 10 == 0 && Score.ScorePT != 0)
                {
                    Rain.Update();
                    Rain.SetRainStartCollision();
                }
                else Rain.SetRainTrue();

                //DRAW
                Gfx.ClearScreen();

                Field.Draw();
                Apple.Draw();
                Snake.Draw();
                Rain.Draw();

                Gfx.Window.Blit(); //Mando l'output allo schermo
            }
            Console.WriteLine($"\nHai perso!\nIl tuo PUNTEGGIO è: {Score.ScorePT}\n");
            Console.ReadLine();
        }

    }
}
