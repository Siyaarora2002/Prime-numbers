using System;
using System.Threading;

namespace PrimeNumber
{
    public class PrimeNumber
    {
        private CancellationTokenSource cancellationTokenSource;
        private ManualResetEvent manualResetEvent;
        private bool paused;
        private bool restart;

        public PrimeNumber(CancellationTokenSource cancellationTokenSource, ManualResetEvent manualResetEvent)
        {
            this.cancellationTokenSource = cancellationTokenSource;
            this.manualResetEvent = manualResetEvent;
            this.paused = false;
            this.restart = false;
        }

        public void GeneratePrimeNumbers()
        {
            int number = 0;

            while (true)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                    break;

                if (!paused)
                {
                    if (IsPrime(number))
                    {
                        Console.Write($"{number} ");
                        Thread.Sleep(1000);
                    }

                    number++;

                    if (restart)
                    {
                        number = 1;
                        restart = false;
                    }
                }

                manualResetEvent.WaitOne();
            }
        }


        private bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        public void PauseGeneration()
        {
            paused = true;
            Console.WriteLine("Printing has been Paused");
        }

        public void ResumeGeneration()
        {
            paused = false;
            manualResetEvent.Set();
            Console.WriteLine("Printing has been Restarted");
        }

        public void ResetGeneration()
        {
            paused = false;
            restart = true;
            manualResetEvent.Set();
            Console.WriteLine("Printing Pime Numbers from the beginning");
        }

        public void QuitGeneration()
        {
            cancellationTokenSource.Cancel();
            Console.WriteLine("Quitting Application");
            Console.WriteLine("Thread is interrupted");
        }
    }
}
