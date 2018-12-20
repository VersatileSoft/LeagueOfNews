namespace Surrender_20.Core.Interface
{
    public enum SystemType
    {
        Unsupported = -1,
        UWP,
        Android, 
        iOS //TFU!!! JEBAĆ
    }

    public interface IOperatingSystemService
    {
        SystemType GetSystemType();
    }
}
