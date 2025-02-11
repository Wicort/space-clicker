using Assets.Services;
using Inventory;
using Items;
using Services;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public static Action<string> OnEquipButtonClicked;
    public static Action<string, int> OnUseUpgradeButtonClicked;

    [SerializeField] private Text _itemNameText;
    [SerializeField] private Text _itemDescriptionText;
    [SerializeField] private Image _itemImage;

    private string _itemId = null;
    private int _itemsCount = 0;

    private void OnEnable()
    {
        InventorySlotView.OnInventoryButtonClicked += OpenActionPanel;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OpenActionPanel(string itemId, int itemsCount)
    {
        if (itemId == null || itemId == "") return;

        _itemId = itemId;
        _itemsCount = itemsCount;
        Item item = AllServices.Container.Single<IItemService>().GetItemInfo(_itemId);
        _itemNameText.text = item.Name;
        _itemDescriptionText.text = item.Description;
        _itemImage.sprite = item.Icon;
        gameObject.SetActive(true);
    }

    public void OnEquipButtonClick()
    {
        if (_itemId == null || _itemId == "") return;

        Sound.instance.PlayInspectClick();

        OnEquipButtonClicked?.Invoke(_itemId);
        _itemId = null;
        OnCloseButtonClick();
    }

    public void OnCloseButtonClick()
    {
        Sound.instance.PlayButtonClick();

        gameObject.SetActive(false);
    }

    public void OnUseUpgradeButtonClick()
    {
        if (_itemId == null || _itemId == "") return;

        Sound.instance.PlayInspectClick();

        OnUseUpgradeButtonClicked?.Invoke(_itemId, _itemsCount);
        _itemId = null;
        OnCloseButtonClick();
    }
}
