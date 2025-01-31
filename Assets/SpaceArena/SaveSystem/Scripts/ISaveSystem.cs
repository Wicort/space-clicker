using Services;

public interface ISaveSystem : IService
{
    void Save(GameData data);
    GameData Load();
}
