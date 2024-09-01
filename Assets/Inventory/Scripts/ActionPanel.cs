using Inventory;
using System;
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public static Action<string> OnEquipButtonClicked;

    private string _itemId = null;

    private void OnEnable()
    {
        InventorySlotView.OnInventoryButtonClicked += OpenActionPanel;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OpenActionPanel(string itemId)
    {
        _itemId = itemId;
        gameObject.SetActive(true);
    }

    public void Init(string itemId)
    {
        _itemId = itemId;
    }

    public void OnEquipButtonClick()
    {
        if (_itemId == null) return;

        OnEquipButtonClicked?.Invoke(_itemId);
        _itemId = null;
        OnCloseButtonClick();
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void OnGetUpgradeButtonClick()
    {

    }
}
