using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ModuleData", menuName = "Game/Module")]
    public class Module : ScriptableObject
    {
        public int Id;
        public AttackType Type = AttackType.AUTOCLICK;
        [SerializeField]
        private string Name;
        [SerializeField]
        private string Description;
        [SerializeField]
        private ItemType ItemType;
        [SerializeField]
        private Sprite Icon;
        public Module RequiredUpgrade;
        public int RequiredLevel;
        public float StartValue;
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
            return Description;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }

        public ItemType GetItemType()
        {
            return ItemType;
        }

        public string GetName()
        {
            return Name;
        }

        public void Remove()
        {
            throw new System.NotImplementedException();
        }

    }
}