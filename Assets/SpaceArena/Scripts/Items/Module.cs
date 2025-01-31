using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ModuleData", menuName = "Game/Module")]
    public class Module : ScriptableObject
    {
        public int Id;
        public AttackType ModuleAttackType = AttackType.AUTOCLICK;
        public ItemType ModuleItemType;
        public Item _item;
        public Module RequiredUpgrade;
        public int RequiredLevel;
        public float ValueGrowthRate;
        public float StartPrice;
        public Stage CurrentStage;
        public List<Stage> Stages;

        public void Equip()
        {
            throw new System.NotImplementedException();
        }

        public string GetDescription()
        {
            return _item.Description;
        }

        public Sprite GetIcon()
        {
            return _item.Icon;
        }

        public ItemType GetItemType()
        {
            return _item.ItemType;
        }

        public string GetName()
        {
            return _item.Name;
        }

        public string GetId()
        {
            return _item.Id;
        }
        public ItemRarity GetRarity()
        {
            return _item.Rarity;
        }

        public void SetItem(Item item)
        {
            _item = item;
        }

        public float StartValue => _item.StartValue;
    }
}