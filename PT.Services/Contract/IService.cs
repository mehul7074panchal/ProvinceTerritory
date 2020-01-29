using System.Collections.Generic;

namespace PT.Services.Contract
{
    public interface IService<T>
    {
        List<T> SelectAll(T TModel);

        int? Insert(T TModel);

        int? Update(T TModel);

        int? Delete(T TModel);
    }
}
