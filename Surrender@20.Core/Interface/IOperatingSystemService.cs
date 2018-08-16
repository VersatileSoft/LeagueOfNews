using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Interface
{
    public enum SystemType
    {
        Unsupported = -1,
        UWP,
        Android, 
        iOS
    }

    public interface IOperatingSystemService
    {
        SystemType GetSystemType();
    }
}
