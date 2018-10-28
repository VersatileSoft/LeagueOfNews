using Surrender_20.Forms.Interfaces;
using System;

namespace Surrender_20.Forms.Services
{
    public class MasterDetailService : IMasterDetailService
    {
        public event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;

        public void MasterPageSelect(string Page)
        {
            OnMasterPageSelect?.Invoke(this, new MasterPageSelectArgs { Page = Page });
        }
    }
}
