using UnityEngine;
using UnityEngine.UI;

public class ModuleButton : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private Text _price;

    public void Init(Sprite icon, string name, string description, string price)
    {
        _icon.sprite = icon;
        _name.text = name;
        _description.text = description;
        _price.text = price;
    }
}
