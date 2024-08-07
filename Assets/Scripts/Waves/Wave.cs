using Items;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Game/Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> EnemiePrefabs;
    public GameObject BossPrefab;
    [Range(0,1)] public float DropChance;
    [Range(0, 10)] public int BossDropCount;
    public float BossDropMultiplier;
    public RarityDictionary RarityDropChance = new RarityDictionary();
}
