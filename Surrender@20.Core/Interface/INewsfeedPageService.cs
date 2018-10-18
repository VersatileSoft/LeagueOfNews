using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface INewsfeedPageService
    {

        Task<string> ParseNewsfeed(Uri Url);

    }
}
