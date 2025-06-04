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
        public static float Gravity;

        public static void Init()
        {
            Apple = new Apple();
            Snake = new SnakeBody();
            Field = new Field();
            Rain = new Rain();
            Gravity = 5; //5px/s^2
            Console.Write("Welcome to Snake! This is not an official game, it is a project created solely for educational purposes as an application of concepts. " +
                "This version of Snake is a hodgepodge of simple and poorly optimized operations and prototypes, so there is nothing to expect. " +
                "The rules are the same as the originals: you control a snake, and you have to make it eat as much fruit as possible. The more fruit it eats, the more the snake " +
                "stretches. The goal is to eat as many apples as possible while avoiding running into walls or into the snake itself. " +
                "The only difference is that since there is no HUD for counting points (only the console), " +
                "for every 10 apples eaten you will receive a special \"warning\" (nothing much, a shower of pixels). That said, have fun! \n" +
                "Oh and before i forget, save your score on a sheet of paper or Excel, it doesn't matter, because this game has no serialization system :)" +
                "\nCommands:\nW, A, S, D for movement (Up, Left, Down, Right)");
        }

        public static void GameOver()
        {
            // This closes the window, stops the game loop, and ends the game displaying the final score.
            Gfx.Window.Close();
        }

        public static void Play()
        {
            while (Gfx.Window.IsOpened)
            {
                //INPUT
                Snake.Input();

                //UPDATE
                Timer.RunTimer();
                Snake.Update();
                
                // Check the collisions of the snake's head with the apple.
                Snake.CheckCollisionSnake(Apple);
                // If the snake collides with itself, the game ends.
                if (Snake.CheckSnakeBodyCollision()) GameOver();
                
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

                Gfx.Window.Blit();
            }
            
            Console.WriteLine($"\nYou lose!\nYour score: {Score.ScorePT}\n");
            Console.ReadLine();
        }
    }
}
