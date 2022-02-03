using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan startTimeSpan = TimeSpan.Zero;
            TimeSpan periodTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(args[2]));

            Timer timer = new Timer((e) =>
            {
                KillTask(args);
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        } 



        static void KillTask(string[] args)
        {
            Process[] tasks = Process.GetProcessesByName($"{args[0]}");


            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine("Имя процесса: " + tasks[i]);
                Console.WriteLine("Время запуска процесса: " +  tasks[i].StartTime);
            }

            if (tasks.Length == 0)
            {
                Console.WriteLine($"Процесс {args[0]} не найден");
            }

            foreach (var process in tasks)
            {
                DateTime startTime = process.StartTime;
                TimeSpan timeDiff = DateTime.Now - startTime;
                int timeDiffInt = Convert.ToInt32(timeDiff.TotalMinutes);

                if (timeDiffInt >= Convert.ToInt32(args[1]))
                {
                    process.Kill();
                    Console.WriteLine($"Процесс закрыт {tasks[0]}");
                }
                else
                {
                    Console.WriteLine("Процесс не закрыт, время работы: " + timeDiffInt + " минут(ы)");
                }
            }
        }
    }
}
