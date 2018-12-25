using System.Collections.Generic;

namespace CachingSolutionsSamples
{
    public interface ICache<TModel> where TModel : class
    {
        IEnumerable<TModel> Get(string forUser);

        void Set(string forUser, IEnumerable<TModel> categories);
    }
}
