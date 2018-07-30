using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface INewsfeedService
    {

        List<Newsfeed> LoadNewsfeeds();

    }
}
