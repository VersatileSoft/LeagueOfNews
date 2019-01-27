using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Interface
{
    public interface IThemeService
    {
        void SetAppTheme(AppTheme appTheme);
    }

    public enum AppTheme
    {
        Ligt,
        Dark
    }
}
