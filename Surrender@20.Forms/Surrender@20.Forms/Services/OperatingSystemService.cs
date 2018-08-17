﻿using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Surrender_20.Forms.Services
{
    public class OperatingSystemService : IOperatingSystemService
    {
        public SystemType GetSystemType()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.UWP: return SystemType.UWP;
                case Device.Android: return SystemType.Android;
                case Device.iOS: return SystemType.iOS;
                case Device.macOS: return SystemType.iOS;
                default: return SystemType.Unsupported;
            }
        }
    }
}