using System.Collections.Generic;

namespace Fibonacci.Caching
{
    public interface IFibonacciNumbers
    {
        int Get(int n);

        IEnumerable<int> GetSequence(int n);
    }
}
