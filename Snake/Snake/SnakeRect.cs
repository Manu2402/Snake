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
        private float speed; //Velocità
        private bool keyPressed; //Booleano per la pressione del tasto senza "tenere premuto"
        private char charPressed; //Controlli sulla gestione dell'input (carattere premuto)
        private char prevCharPressed;

        public SnakeRect(char charPressed = ' ')
        {
            X = (int)(Gfx.Window.Width * 0.5f) - 15; //Posizione iniziale
            Y = (int)(Gfx.Window.Height * 0.5f) - 15;
            halfWidth = 15;
            halfHeigth = 15;
            color = ColorsFactory.GetColor(Colors.Green);
            speed = halfWidth * 2; //Velocità (si sposta di 30px, in modo da rimanere conforme alla griglia virtuale)
            keyPressed = false;
            this.charPressed = charPressed;
            prevCharPressed = ' ';
        }

        //Properties per le coordinate ed il colore
        public float X { get { return position.x; } set { position.x = value; } }
        public float Y { get { return position.y; } set { position.y = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public char PrevCharPressed { get { return prevCharPressed; } }
        public char CharPressed { get { return charPressed; } set { charPressed = value; } }

        public char CheckInput()
        {
            //Se premo un tasto, non premendo gli altri, e non ho ancora premuto effettivamente su un tasto, ritorno quel tasto
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
            switch (character) //Sposto effettivamente il serpente
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
            if (prevCharPressed != oppositeChar) //Se ho premuto il tasto di direzione opposta, continuo il movimento precedente
            {
                DoMovement(ref charPressed); //Altrimenti mi muovo in base al tasto premuto
                keyPressed = false; //Setto a false perchè ho premuto il tasto
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
            switch (charPressed) //In base a che tasto ho premuto, mi sposto di conseguenza
            {
                case 'W':
                    temp = UpdateMovement('S'); //Richiamo il metodo che applica il movimento, passando il tasto di direzione opposta come parametro
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
            if (temp) prevCharPressed = charPressed; //Se il movimento è avvenuto correttamente, salvo il tasto
            //precedente in modo da creare il movimento continuo anche senza tenere premuto il tasto di direzione
        }

        public bool CheckCollision(Apple apple)
        {
            if (apple != null)
            {
                if (position.x == apple.X && position.y == apple.Y) //Collisione mela in base alla posizione precisa
                {
                    apple.Update();
                    return true;
                }
            }
            return false;
        }

        public void Input()
        {
            charPressed = CheckInput(); //Ottengo l'input di direzione
        }

        public void Update(ref int i)
        {
            if (Timer.alarm) Movement(); //Se il timer ha suonato, mi muovo
        }

        public void Draw()
        {
            //Come con la mela
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

