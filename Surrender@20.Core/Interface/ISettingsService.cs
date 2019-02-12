using Surrender_20.Core.Service;

namespace Surrender_20.Core.Interface
{
    public interface ISettingsService
    {
        /// <summary>
        /// Returns <c>NewsfeedNavigationParameter</c> using the given <c>Pages</c> parameter
        /// </summary>
        NewsfeedNavigationParameter this[Pages PropertyName] { get; set; }
    }

    public enum Pages //Change name to Page (items should be called singular, unless they represent a list)
    {
        SurrenderHome,
        PBE,
        Releases,
        RedPosts,
        Rotations,
        ESports,
        Official,
        Dev,
        Settings
    }
}