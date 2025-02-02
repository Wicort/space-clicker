using Services;

namespace Assets.SpaceArena.Scripts.Infrastructure.Localization
{
    public interface ILocalizationService : IService
    {
        string GetItemName(string itemId);
        string GetItemDescription(string itemId);
        string GetUIByKey(string key);
    }
}
