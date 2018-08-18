using Surrender_20.Core;
using Surrender_20.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20
{
    public class UWPCoreApp : CoreApp
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterAppStart<MainPageViewModel>();
        }
    }
}
