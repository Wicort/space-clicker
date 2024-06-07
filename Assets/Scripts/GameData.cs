using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int Level;
    public float Currency;
    public bool IsBossFailed;
    public List<ActiveModule> Modules;
    public GameData()
    {
        Level = 0;
        Currency = 0;
        IsBossFailed = false;
    }

    public void NextLevel()
    {
        if (!IsBossFailed)
            Level++;
    }

    public void AddCurrency(float value)
    {
        if (value <= 0) return;

        Currency += value;
    }

    public void SetIsBossFailed(bool value)
    {
        IsBossFailed = value;
    }
}
