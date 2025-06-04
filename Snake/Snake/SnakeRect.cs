using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Snake
{
    class SnakeRect
    {
        private Vector2 position;
        private Color color;
        private int halfWidth;
        private int halfHeigth;
        private float speed;
        private bool keyPressed; // Boolean for pressing the key without “hold down”.
        private char charPressed; // Controls on input handling (character pressed).
        private char prevCharPressed;

        public SnakeRect(char charPressed = ' ')
        {
            // Starting position.
            X = (int)(Gfx.Window.Width * 0.5f) - 15; 
            Y = (int)(Gfx.Window.Height * 0.5f) - 15;
            
            halfWidth = 15;
            halfHeigth = 15;
            
            color = ColorsFactory.GetColor(Colors.Green);
            
            // Speed: shifts by 30px, so as to remain compliant with the virtual grid.
            speed = halfWidth * 2; 
            
            keyPressed = false;
            this.charPressed = charPressed;
            prevCharPressed = ' ';
        }

        // Properties for coordinates and color.
        public float X { get { return position.x; } set { position.x = value; } }
        public float Y { get { return position.y; } set { position.y = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public char PrevCharPressed { get { return prevCharPressed; } }
        public char CharPressed { get { return charPressed; } set { charPressed = value; } }

        public char CheckInput()
        {
            // If i press one key, not pressing the others, and i haven't actually pressed on a key yet, i return that key.
            if (!keyPressed && Gfx.Window.GetKey(KeyCode.W) && !Gfx.Window.GetKey(KeyCode.A) && !Gfx.Window.GetKey(KeyCode.S) && !Gfx.Window.GetKey(KeyCode.D))
            {
                charPressed = 'W';
            }
            else if (!keyPressed && !Gfx.Window.GetKey(KeyCode.W) && Gfx.Window.GetKey(KeyCode.A) && !Gfx.Window.GetKey(KeyCode.S) && !Gfx.Window.GetKey(KeyCode.D))
            {
                charPressed = 'A';
            }
            else if (!keyPressed && !Gfx.Window.GetKey(KeyCode.W) && !Gfx.Window.GetKey(KeyCode.A) && Gfx.Window.GetKey(KeyCode.S) && !Gfx.Window.GetKey(KeyCode.D))
            {
                charPressed = 'S';
            }
            else if (!keyPressed && !Gfx.Window.GetKey(KeyCode.W) && !Gfx.Window.GetKey(KeyCode.A) && !Gfx.Window.GetKey(KeyCode.S) && Gfx.Window.GetKey(KeyCode.D))
            {
                charPressed = 'D';
            }
            
            return charPressed;
        }

        public void DoMovement(ref char character)
        {
            // I actually move the snake.
            switch (character) 
            {
                case 'W':
                    position.y -= speed;
                    break;
                case 'A':
                    position.x -= speed;
                    break;
                case 'S':
                    position.y += speed;
                    break;
                case 'D':
                    position.x += speed;
                    break;
            }
        }

        public bool UpdateMovement(char oppositeChar)
        {
            // If i pressed the opposite direction key, i continue the previous movement.
            if (prevCharPressed != oppositeChar) 
            {
                // Otherwise i move according to the key pressed.
                DoMovement(ref charPressed); 
                // Set to false because I pressed the key.
                keyPressed = false;
                
                return true;
            }
            else
            {
                DoMovement(ref prevCharPressed);
                keyPressed = false;
                
                return false;
            }
        }

        public void Movement()
        {
            bool temp = false;
            
            // Based on what key i pressed, i move accordingly.
            switch (charPressed)
            {
                // Return the method that applies the motion, passing the opposite direction key as a parameter.
                case 'W':
                    temp = UpdateMovement('S'); 
                    break;
                case 'A':
                    temp = UpdateMovement('D');
                    break;
                case 'S':
                    temp = UpdateMovement('W');
                    break;
                case 'D':
                    temp = UpdateMovement('A');
                    break;
            }
            
            // If the movement was successful, save the previous so as to create the continuous movement
            // even without holding down the direction key.
            if (temp) prevCharPressed = charPressed; 
        }

        public bool CheckCollision(Apple apple)
        {
            if (apple != null)
            {
                // Collision apple based on precise position.
                if (position.x == apple.X && position.y == apple.Y) 
                {
                    apple.Update();
                    
                    return true;
                }
            }
            
            return false;
        }

        public void Input()
        {
            // Obtain direction input.
            charPressed = CheckInput();
        }

        public void Update(ref int i)
        {
            // If the timer has sounded, the snake moves.
            if (Timer.alarm) Movement(); 
        }

        public void Draw()
        {
            float positionCenterX = position.x - halfWidth;
            float positionCenterY = position.y - halfHeigth;
            
            if (position.x < 0 || position.x > Gfx.Window.Width || position.y < 0 || position.y > Gfx.Window.Height) //Se snake esce dalla finestra...
            {
                Game.GameOver();
            }
            else
            {
                Gfx.DrawRect((int)positionCenterX, (int)positionCenterY, halfWidth * 2, halfHeigth * 2, color);
            }
        }
    }
}

