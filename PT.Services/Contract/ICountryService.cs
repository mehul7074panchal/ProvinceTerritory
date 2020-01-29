using PT.DataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Services.Contract
{
    public interface ICountryService
    {
        List<sp_tblCountry_pagination_select_Result> GetSp_TblCountry_Paginations(int start, int length, string search);

    }
}
