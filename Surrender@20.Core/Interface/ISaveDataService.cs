namespace Surrender_20.Core.Interface
{
    public interface ISaveDataService
    {
        void SaveLastPostTitle(Pages page, string lastPostTitle);
        string GetLastPostTitle(Pages page);
        int GetCheckNewPostsFrequency();
    }
}