using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeBody
    {
        public SnakeRect[] Snake; // Array of snake's pieces.
        public static int counterPieces; // Number of pieces.

        public SnakeBody()
        {
            // Static size of 784 rectangles [considering 29x29 grid].
            // Yes, it is not at all optimized but it is an inter-school project where i use only the concepts
            // explained up to the time of project development.
            Snake = new SnakeRect[784]; 
            counterPieces = 0;
            Snake[counterPieces] = new SnakeRect(); // Head.
            Snake[counterPieces].Color = ColorsFactory.GetColor(Colors.DarkGreen); // Change head's color.
            Timer.SetTimer();
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
            // If the collision with the head occurs...
            if (Snake[0].CheckCollision(apple))
            {
                counterPieces++; // Add the number of pieces and allocate the new body piece.
                Snake[counterPieces] = new SnakeRect();
                
                Score.AddPT(); // Update the score.
            }
            
            // If it has pieces, i scroll through them all and make them depend on the previous one.
            // This way they will be placed in the right place. All of this is done by keypress linking.
            if (counterPieces <= 0) return;
            
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
            // If the pieces are not the same, and their positions match, return true.
            if (i != j) 
            {
                if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                {
                    return true;
                }
            }
            
            return false;
        }

        // Checks collisions with other pieces of the snake.
        public bool CheckSnakeBodyCollision()
        {
            // If he possesses only the head, skip.
            if (counterPieces <= 0) return false;
            
            for (int i = 0; i <= counterPieces; i++)
            {
                for (int j = 0; j <= counterPieces; j++)
                {
                    if (CollisionWithBody(ref i, ref j))
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
