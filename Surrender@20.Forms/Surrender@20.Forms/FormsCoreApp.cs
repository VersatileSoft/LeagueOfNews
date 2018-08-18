using Surrender_20.Core;
using Surrender_20.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Forms
{
    public class FormsCoreApp : CoreApp
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterAppStart<RootViewModel>();
        }
    }
}
