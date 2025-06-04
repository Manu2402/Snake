using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    // Static class, timer shared with other objects.
    static class Timer
    {
        public static float currentTime; //T ime at instant "x".
        public static float alarmTime; // Time in which the timer is to sound.
        public static bool alarm; // Timer sound.
        public static bool run; // Timer execution.

        public static void RunTimer()
        {
            if (run)
            {
                currentTime += 20f * Gfx.Window.DeltaTime;
                
                // Sets alarm to true when the value reaches the set value.
                if (currentTime >= alarmTime)
                {
                    alarm = true;
                }
            }
        }

        public static void SetTimer()
        {
            run = true;
            alarmTime = 0.5f;
            currentTime = 0f;
        }

        public static void CheckTimer()
        {
            // If it sounds, i stop the “ringer” and i reset it.
            if (alarm)
            {
                alarm = false;  
                currentTime = 0f;
            }
        }
    }
}
