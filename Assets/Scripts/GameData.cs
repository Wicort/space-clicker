﻿using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    private bool _isBossFailed;
    private int _level;
    private float _currency;


    public int Level => _level;
    public float Currency => _currency;
    
    public List<ActiveUpgrade> Modules;

    public bool IsBossFailed => _isBossFailed;
    public GameData()
    {
        _level = 9;
        _currency = 0;
        _isBossFailed = false;
    }

    public void NextLevel()
    {
        if (!IsBossFailed) _level++;
    }

    public void PrevLevel()
    {
        _level--;
    }

    public void AddCurrency(float value)
    {
        _currency += value;
    }

    public void SetIsBossFailed(bool value)
    {
        _isBossFailed = value;
    }
}
