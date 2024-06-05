public class Game
{
    public int Level;
    public float Currency;
    public bool IsBossFailed;
    public Game()
    {
        Level = 0;
        Currency = 0;
        IsBossFailed = false;
    }

    public void NextLevel()
    {
        Level++;
    }

    public void AddCurrency(float value)
    {
        if (value <= 0) return;

        Currency += value;
    }
}
