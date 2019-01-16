using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Interface
{
    public interface INotificationService
    {
        void ShowNewPostNotification(Newsfeed newsfeed);
    }
}
