using System;
using System.Threading;


namespace ConsoleApplication1
{
    public static class Program
    {
        public static ThreadLocal<int> _field = 
            new ThreadLocal<int>(()=>
            {
                return Thread.CurrentThread.ManagedThreadId;
            });

        static void Main(string[] args)
        {
            new Thread(() =>
                {
                    for (int i = 0; i < _field.Value; i++)
                    {
                        Console.WriteLine("A: {0}", i);
                        //Thread.Sleep(0);
                    }

                }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < _field.Value; i++)
                {
                    Console.WriteLine("B: {0}", i);
                    //Thread.Sleep(0);
                }
            }).Start();

            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
        }
    }
}
