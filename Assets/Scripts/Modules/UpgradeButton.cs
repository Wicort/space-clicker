using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private Text _price;
    [SerializeField]
    private Button _upgradeButton;

    public int Id => _id;


    public static Action<int> OnModuleUpgraded;

    public void Init(int id, Sprite icon, string name, string description, string price, bool isBtnEnabled)
    {
        _id = id;
        _icon.sprite = icon;
        _name.text = name;
        _description.text = description;
        _price.text = price;
        SetInterractable(isBtnEnabled);
    }

    public void OnUpgradeButtonClick()
    {
        if (_id == 0) return;

        OnModuleUpgraded?.Invoke(_id);
    }

    public void SetInterractable(bool isBtnEnabled)
    {
        _upgradeButton.interactable = isBtnEnabled;
    }
}
