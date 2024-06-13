using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModuleData", menuName = "Game/Module")]
public class Module: ScriptableObject
{
    public int Id;
    public ModuleType Type = ModuleType.AUTOCLICK;
    public string Name;
    public string Description;
    public Sprite Icon;
    public Module RequiredUpgrade;
    public int RequiredLevel;
    public float StartValue;
    public float ValueGrowthRate;
    public float StartPrice;
    public Stage CurrentStage;
    public List<Stage> Stages;
}
