using System.Collections.Generic;

namespace PT.DataLibrary.Contract
{
    public interface IReposatory<T>
    {
        List<T> SelectAll(T TModel);

        int? Insert(T TModel);

        int? Update(T TModel);

        int? Delete(T TModel);
    }
}
