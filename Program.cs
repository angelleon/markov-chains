using System;

namespace Markov
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string arg in args)
            {
                Console.WriteLine(arg);
            }
            using (Markov m = new Markov(args))
            {
                m.run();
            }
        }
    }
}
