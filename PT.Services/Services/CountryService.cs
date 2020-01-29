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
    public class CountryService : ICountryService
    {

        #region Private Variable
        private readonly CountryRepository _repositoryCountry;
        #endregion

        #region Constructor
        public CountryService()
        {
            _repositoryCountry = new CountryRepository();
        }
        #endregion

        public List<sp_tblCountry_pagination_select_Result> GetSp_TblCountry_Paginations(int start, int length, string search)
        {

            return _repositoryCountry.GetSp_TblCountry_Paginations(start, length,search);
        }
    }
}
