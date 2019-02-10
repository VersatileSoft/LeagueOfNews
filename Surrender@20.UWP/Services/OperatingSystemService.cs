using Surrender_20.Core.Interface;

namespace Surrender_20.UWP.Services
{
    public class OperatingSystemService : IOperatingSystemService
    {
        public SystemType GetSystemType()
        {
            return SystemType.UWP;
        }
    }
}