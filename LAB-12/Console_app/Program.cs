using System;
using System.Threading;

namespace AlarmClock
{
    // Delegate
    public delegate void AlarmEventHandler();

    // Publisher class
    public class Alarm
    {
        public event AlarmEventHandler raiseAlarm;
        private TimeSpan targetTime;

        public Alarm(TimeSpan targetTime)
        {
            this.targetTime = targetTime;
        }

        public void Start()
        {
            int secondsElapsed = 0;

            while (true)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                if (currentTime.Hours == targetTime.Hours &&
                    currentTime.Minutes == targetTime.Minutes &&
                    currentTime.Seconds == targetTime.Seconds)
                {
                    OnRaiseAlarm();
                    break;
                }

                if (secondsElapsed % 30 == 0)
                {
                    Console.WriteLine($"Current Time: {DateTime.Now:HH:mm:ss}");
                }

                Thread.Sleep(1000);
                secondsElapsed++;
            }
        }

        protected virtual void OnRaiseAlarm()
        {
            if (raiseAlarm != null)
                raiseAlarm();
        }
    }

    // Subscriber class
    public class AlarmHandler
    {
        public void Ring_alarm()
        {
            Console.WriteLine("⏰ Alarm Ringing! Time matched!");
        }
    }

    // Main class
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter alarm time in HH:MM:SS format:");
            string input = Console.ReadLine();

            if (TimeSpan.TryParse(input, out TimeSpan userTime))
            {
                Alarm alarm = new Alarm(userTime);
                AlarmHandler handler = new AlarmHandler();

                // Subscribe to the event
                alarm.raiseAlarm += handler.Ring_alarm;

                Console.WriteLine($"Alarm set for {userTime}. Waiting...");
                alarm.Start();
            }
            else
            {
                Console.WriteLine("Invalid time format. Please enter time as HH:MM:SS.");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
