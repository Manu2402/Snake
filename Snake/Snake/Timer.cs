using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class Timer //Classe statica, timer condiviso con gli altri oggetti
    {
        public static float currentTime; //Tempo nell'istante x
        public static float alarmTime; //Tempo nella quale il timer deve suonare
        public static bool alarm; //Suono del timer
        public static bool run; //Esecuzione del timer

        public static void RunTimer()
        {
            if (run) //Parte il timer
            {
                currentTime += 20f * Gfx.Window.DeltaTime; //Aggiunge un valore frame independent
                if (currentTime >= alarmTime) //Imposta l'allarme a true quando raggiunge il valore impostato
                {
                    alarm = true;
                }
            }
        }

        public static void SetTimer()
        {
            run = true; //Fa partire il timer
            alarmTime = 0.5f; //Imposta i valori
            currentTime = 0f;
        }

        public static void CheckTimer()
        {
            if (alarm) //Se ha suonato...
            {
                alarm = false; //Spengo la "suoneria" e lo resetto
                currentTime = 0f;
            }
        }

    }
}
