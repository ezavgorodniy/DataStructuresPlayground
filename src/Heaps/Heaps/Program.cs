using System;

namespace Heaps
{
    class Program
    {
        static void Main(string[] args)
        {
            var minIntHeap = new MinIntHeap();

            for (int i = 0; i < 30; i++)
            {
                minIntHeap.Push(i);
            }

            while (true)
            {
                Console.WriteLine(minIntHeap.Pop());
                Console.ReadLine();
            }
        }
    }
}
