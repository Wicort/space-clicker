using Items;
using Services;

namespace Assets.Services
{
    public interface IItemService : IService
    {
        Item GetCommonItemByType(ItemType moduleItemType);
        Item GetItemInfo(string itemId);
        Item GetRandomItemByRarity(ItemRarity rarity);
        Item GetItemByTypeAndRariry(ItemType moduleItemType, ItemRarity rarity);
        void SetTestItemsList();
    }
}