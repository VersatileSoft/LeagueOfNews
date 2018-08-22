using Surrender_20.Forms.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
