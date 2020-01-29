using PT.DataLibrary.Data;
using PT.DataLibrary.Repository;
using PT.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Services.Services
{
    public class CityService : ICityService
    {
        #region Private Variable
        private readonly CityRepository _repositoryCity;
        #endregion

        #region Constructor
        public CityService()
        {
            _repositoryCity = new CityRepository();
        }
        #endregion

        public List<sp_tblCity_pagination_select_Result> GetSp_TblCity_Paginations(int start, int length, string search)
        {

            return _repositoryCity.GetSp_TblCity_Paginations(start, length, search);
        }
    }
}
