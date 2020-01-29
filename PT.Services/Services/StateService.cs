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
    public class StateService : IStateService
    {
        #region Private Variable
        private readonly StateRepository _repositoryState;
        #endregion

        #region Constructor
        public StateService()
        {
            _repositoryState = new StateRepository();
        }
        #endregion

        public List<sp_tblState_pagination_select_Result> GetSp_TblState_Paginations(int start, int length, string search)
        {

            return _repositoryState.GetSp_TblState_Paginations(start, length, search);
        }
    }
}
