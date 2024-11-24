using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLine : MonoBehaviour
{
    public Image image;
    public Text text;

    public Color commonColor;
    public Color uncommonColor;
    public Color rareColor;
    public Color epicColor;
    public Color legendaryColor;
    public Color mythColor;

    public void SetText(string value)
    {
        text.text = value;
    }

    public void SetColor(Color value)
    {
        text.color = value;
    }

    public void Init(Item item)
    {
        SetText(item.Name);
        switch (item.Rarity)
        {
            case ItemRarity.COMMON: SetColor(commonColor); break;
            case ItemRarity.UNCOMMON: SetColor(uncommonColor); break;
            case ItemRarity.RARE: SetColor(rareColor); break;
            case ItemRarity.EPIC: SetColor(epicColor); break;
            case ItemRarity.LEGENDARY: SetColor(legendaryColor); break;
            case ItemRarity.MYTHICAL: SetColor(mythColor); break;
        }
    }
}
