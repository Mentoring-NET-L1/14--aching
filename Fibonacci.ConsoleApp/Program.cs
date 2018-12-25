using System;
using Fibonacci.Caching;

namespace FibonacciConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintFibonacci(new FibonacciNumbers());
            PrintFibonacci(new RedisFibonacciNumbers());
            Console.ReadLine();
        }

        private static void PrintFibonacci(IFibonacciNumbers numbers)
        {
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine(numbers.Get(i));
            }
        }
    }
}
