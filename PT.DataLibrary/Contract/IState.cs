using PT.DataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.DataLibrary.Contract
{
    public interface IState
    {
        List<sp_tblState_pagination_select_Result> GetSp_TblState_Paginations(int start, int length, string search);

    }
}
