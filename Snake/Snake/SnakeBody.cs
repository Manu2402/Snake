using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{

    class SnakeBody
    {
        public SnakeRect[] Snake; //Vettore di pezzi di snake
        public static int counterPieces; //Numero di pezzi, variabile statica

        public SnakeBody()
        {
            Snake = new SnakeRect[784]; //Dimensione statica di 784 rettangoli [considerando la griglia 29x29]
            //Si, non è per niente ottimizzato ma è un progetto inter-scolastico dove utilizzo solo i concetti
            //spiegati fino al momento di sviluppo del progetto.
            counterPieces = 0;
            Snake[counterPieces] = new SnakeRect(); //Testa
            Snake[counterPieces].Color = ColorsFactory.GetColor(Colors.DarkGreen); //Cambio colore testa
            Timer.SetTimer(); //Imposto il timer
        }

        public void Input()
        {
            Snake[0].Input();
        }

        public void Update()
        {
            for (int i = 0; i < Snake.Length; i++)
            {
                if (Snake[i] != null)
                {
                    Snake[i].Update(ref i);
                }
            }
        }

        public void CheckCollisionSnake(Apple apple)
        {
            if (Snake[0].CheckCollision(apple)) //Se avviene la collisione con la testa...
            {
                counterPieces++; //Aumento il numero di pezzi e alloco il nuovo pezzo di corpo
                Snake[counterPieces] = new SnakeRect();
                Score.AddPT(); //Aggiorno il punteggio
            }
            if (counterPieces <= 0) return; //Se possiede dei pezzi, li scorro tutti e li faccio dipendere dal precedente.
            //In questo modo verranno posizionati nel posto giusto. Tutto questo avviene tramite il collegamento dei tasti premuti.
            for (int i = 0; i < counterPieces; i++)
            {
                Snake[i + 1].CharPressed = Snake[i].PrevCharPressed;
                switch (Snake[i + 1].CharPressed)
                {
                    case 'W':
                        Snake[i + 1].X = Snake[i].X;
                        Snake[i + 1].Y = Snake[i].Y + 30;
                        break;
                    case 'A':
                        Snake[i + 1].X = Snake[i].X + 30;
                        Snake[i + 1].Y = Snake[i].Y;
                        break;
                    case 'S':
                        Snake[i + 1].X = Snake[i].X;
                        Snake[i + 1].Y = Snake[i].Y - 30;
                        break;
                    case 'D':
                        Snake[i + 1].X = Snake[i].X - 30;
                        Snake[i + 1].Y = Snake[i].Y;
                        break;
                }
            }
            NotIntoSnake(apple);
        }

        public void NotIntoSnake(Apple apple)
        {
            for (int i = 0; i <= counterPieces; i++)
            {
                if (apple.X == Snake[i].X && apple.Y == Snake[i].Y)
                {
                    apple.Update();
                }
            }
        }

        public bool CollisionWithBody(ref int i, ref int j)
        {
            if (i != j) //Se i pezzi non sono gli stessi, e la loro posizione coincide, ritorna true
            {
                if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckSnakeBodyCollision() //Controlla le collisioni con gli altri pezzi del serpente
        {
            if (counterPieces <= 0) return false; //Se possiede solo la testa, skip
            for (int i = 0; i <= counterPieces; i++)
            {
                for (int j = 0; j <= counterPieces; j++)
                {
                    if (CollisionWithBody(ref i, ref j)) //Viene checkato ogni pezzo
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw()
        {
            for (int i = 0; i < Snake.Length; i++)
            {
                if (Snake[i] != null)
                {
                    Snake[i].Draw();
                }
            }
        }

    }
}
