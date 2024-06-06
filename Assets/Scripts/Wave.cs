using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Game/Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> EnemiePrefabs;
    public GameObject BossPrefab;
}
