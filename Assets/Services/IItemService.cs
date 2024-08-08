using Items;
using Services;

namespace Assets.Services
{
    public interface IItemService : IService
    {
        Item GetCommonItemByType(ItemType moduleItemType);
        Item GetItemInfo(string itemId);
        Item GetRandomItemByRarity(ItemRarity rarity);
        void SetTestItemsList();
    }
}