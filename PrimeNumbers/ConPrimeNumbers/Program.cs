using System;
using System.Threading;

namespace PrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press s to start Printing Pime Numbers");
            Console.WriteLine("Press x to pause Printing Pime Numbers");
            Console.WriteLine("Press a to restart Printing prime numbers");
            Console.WriteLine("Press r to restart Printing prime numbers from the beginning");
            Console.WriteLine("Press q to quit the application");


            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ManualResetEvent manualResetEvent = new ManualResetEvent(true);

            PrimeNumber primeNumber = new PrimeNumber(cancellationTokenSource, manualResetEvent);
            Thread primeThread = new Thread(primeNumber.GeneratePrimeNumbers);
            primeThread.Start();

            ConsoleKeyInfo key;
            do
            {
                
                key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 's':
                        manualResetEvent.Set();
                        break;
                    case 'x':
                        primeNumber.PauseGeneration();
                        break;
                    case 'a':
                        primeNumber.ResumeGeneration();
                        break;
                    case 'r':
                        primeNumber.ResetGeneration();
                        break;
                    case 'q':
                        primeNumber.QuitGeneration();
                        break;
                }
            } while (key.KeyChar != 'q');
        }
    }
}
