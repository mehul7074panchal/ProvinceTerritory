using PT.DataLibrary.Contract;
using PT.DataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.DataLibrary.Repository
{
    public class CountryRepository : ICountry
    {

        #region Private Variable

        private readonly PTContext _Context;

        #endregion

        #region Constructor
        public CountryRepository()
        {
            _Context = new PTContext();

        }

        #endregion


        /// <summary>
        /// get countries next/pre top n list 
        /// </summary>
        /// <param name="start">starting posting</param>
        /// <param name="length">number of country fetched</param>
        /// <returns>list of countryies</returns>
        public List<sp_tblCountry_pagination_select_Result> GetSp_TblCountry_Paginations(int start, int length,string search)
        {
            return _Context.sp_tblCountry_pagination_select(start, length, search).ToList();
        }
    }
}
