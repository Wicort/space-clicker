using Assets.SpaceArena.SaveSystem.Scripts;
using Services;

public interface ISaveSystem : IService
{
    void SaveGame();
    GameData LoadGame();

    void SaveSettings(SettingsData data);
}
