using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using System;

namespace Surrender_20.Forms.Services
{
    public class MasterDetailService : IMasterDetailService
    {
        public event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;

        public void MasterPageSelect(NewsWebsite Page)
        {
            OnMasterPageSelect?.Invoke(this, new MasterPageSelectArgs { Page = Page });
        }
    }
}