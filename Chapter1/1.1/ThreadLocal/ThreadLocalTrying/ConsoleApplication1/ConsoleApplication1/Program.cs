using System;
using System.Threading;


namespace ConsoleApplication1
{
    public static class Program
    {
        public static void Mytarget()
        {
            Console.WriteLine("target");  
        }
        public static void Mytarget1(string a)
        {
            Console.WriteLine("target1: {0}", a);
        }

        public static ThreadLocal<string> _field = 
            new ThreadLocal<string>(()=>
            {
                return Thread.CurrentThread.Name + "no-name";
            });

        public static ThreadLocal<int> _field1 =
         new ThreadLocal<int>(() =>
         {
             return Thread.CurrentThread.ManagedThreadId;
         });

        static void Main(string[] args)
        {
            //1.
            ThreadStart ts = new ThreadStart(Mytarget);            
            Thread t = new Thread(ts);
            t.Start();


            //2.
            new Thread(() =>
                {
                    Mytarget1("2");
                }).Start();

            //3.            
            new Thread( (a) => 
            {

                Console.WriteLine(""); 
                Mytarget1("3");
            }).Start();


            new Thread(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("A: {0}", _field.Value);                        
                    }

                }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < _field1.Value; i++)
                {
                    Console.WriteLine("B: {0}", i);
                }
            }).Start();

            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
        }
    }
}
