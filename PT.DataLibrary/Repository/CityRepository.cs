using PT.DataLibrary.Contract;
using PT.DataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.DataLibrary.Repository
{
    public class CityRepository : ICity
    {
        #region Private Variable

        private readonly PTContext _Context;

        #endregion

        #region Constructor
        public CityRepository()
        {
            _Context = new PTContext();

        }

        #endregion


        /// <summary>
        /// get States next/pre top n list 
        /// </summary>
        /// <param name="start">starting posting</param>
        /// <param name="length">number of States fetched</param>
        /// <returns>list of States</returns>
        public List<sp_tblCity_pagination_select_Result> GetSp_TblCity_Paginations(int start, int length, string search)
        {
            return _Context.sp_tblCity_pagination_select(start, length, search).ToList();
        }
    }
}
