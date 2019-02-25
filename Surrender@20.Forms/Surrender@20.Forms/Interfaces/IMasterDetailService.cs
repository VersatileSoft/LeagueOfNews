using Surrender_20.Core.Interface;
using System;

namespace Surrender_20.Forms.Interfaces
{
    public interface IMasterDetailService
    {
        void MasterPageSelect(NewsWebsite Page);
        event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;
    }

    public class MasterPageSelectArgs
    {
        public NewsWebsite Page { get; set; }
    }
}