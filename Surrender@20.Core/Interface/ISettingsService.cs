using Surrender_20.Core.Service;

namespace Surrender_20.Core.Interface
{

    public interface ISettingsService
    {
        NewsfeedNavigationParameter this[Pages PropertyName] { get; set; } //Should return object, cast later in code
    }

    public enum Pages
    {
        SurrenderHome,
        PBE,
        Releases,
        RedPosts,
        Rotations,
        ESports,
        Official,
        Settings
    }
}
