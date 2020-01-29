using PT.DataLibrary.Data;
using PT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.DataLibrary.Contract
{
    public interface ICountry
    {
        List<sp_tblCountry_pagination_select_Result> GetSp_TblCountry_Paginations(int start,int length, string search);
    }
}
