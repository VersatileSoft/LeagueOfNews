namespace Surrender_20.Core.Interface
{
    public interface IThemeService
    {
        void SetAppTheme(AppTheme appTheme);
    }

    public enum AppTheme
    {
        Light,
        Dark
    }
}
