using Services;

namespace Items
{
    public interface IItemService : IService
    {
        Item GetCommonItemByType(ItemType moduleItemType);
        Item GetItemInfo(string itemId);
        Item GetRandomItemByRarity(ItemRarity rarity);
        void SetTestItemsList();
    }
}