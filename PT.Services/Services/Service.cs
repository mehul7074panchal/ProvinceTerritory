using System.Collections.Generic;
using PT.Services.Contract;
using PT.DataLibrary.Contract;
using PT.DataLibrary.Repository;

namespace PT.Services.Services
{
    public class Service<T> : IService<T>
    {
        #region Private Variable
        private readonly IReposatory<T> _repository;

        #endregion

        #region Constructor
        public Service()
        {
            _repository = new BaseRepoistory<T>();
        }
        #endregion

        #region CRUD Operations
        public int? Delete(T TModel)
        {
            return _repository.Delete(TModel);
        }

        public int? Insert(T TModel)
        {
            return _repository.Insert(TModel);
        }

        public List<T> SelectAll(T TModel)
        {
            return _repository.SelectAll(TModel);
        }

        public int? Update(T TModel)
        {
            return _repository.Update(TModel);
        }
        #endregion
    }
}
