using System;

namespace Surrender_20.Forms.Interfaces
{
    public interface IMasterDetailService
    {
        void MasterPageSelect(string Page);
        event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;
    }

    public class MasterPageSelectArgs
    {
        public string Page { get; set; }
    }
}
