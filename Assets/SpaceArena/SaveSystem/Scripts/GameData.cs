﻿using Assets.SpaceArena.SaveSystem.Scripts;
using Items;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    private bool _isBossFailed;
    private int _level;
    private float _currency;

    public int Module0Lvl;
    public ItemRarity Module0Rarity;
    public int Module1Lvl;
    public ItemRarity Module1Rarity;
    public int Module2Lvl;
    public ItemRarity Module2Rarity;

    public DateTime LastPlayedTime;

    public int Level => _level;
    public float Currency => _currency;
    
    public List<ActiveUpgrade> Modules;
    public bool DroneIsReady;

    public bool IsBossFailed => _isBossFailed;

    public SettingsData Settings;
    public GameData()
    {
        _level = 0;
        _currency = 0;
        _isBossFailed = false;
        Settings = new SettingsData();
    }

    public void NextLevel()
    {
        if (!_isBossFailed) _level++;
    }

    public void SetLevel(int value)
    {
        if (value < 0)
        {
            _level = 0;
            _isBossFailed = false;
        }
        else 
            _level = value;
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
