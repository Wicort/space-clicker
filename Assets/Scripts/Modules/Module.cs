using UnityEngine;

[CreateAssetMenu(fileName = "ModuleData", menuName = "Game/Module")]
public class Module: ScriptableObject
{
    public int Id;
    public ModuleType Type = ModuleType.AUTOCLICK;
    public string Name;
    public Module RequiredUpgrade;
    public int RequiredLevel;
    public float StartValue;
    public float StartPrice;
}
