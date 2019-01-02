using Surrender_20.Core.Service;

namespace Surrender_20.Core.Interface
{

    public interface ISettingsService
    {
        NewsfeedNavigationParameter this[Setting PropertyName] { get; set; } //Should return object, cast later in code
    }

    public enum Setting
    {
        Home,
        PBE,
        Releases,
        RedPosts,
        Rotations,
        ESports,
        Official
    }
}
