using System.Threading.Tasks;

namespace LeagueOfNews.Core.Interface
{
    public interface INewPostsService
    {
        Task CheckNewPosts();
    }
}