using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class Stage
    {
        public int Id;
        public int RequiredLevel;
        public GameObject Prefab;
    }
}