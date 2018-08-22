using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
