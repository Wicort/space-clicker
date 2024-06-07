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

    public void Init(Image icon, Text name, Text description, Text price)
    {
        _icon = icon;
        _name = name;
        _description = description;
        _price = price;
    }
}
