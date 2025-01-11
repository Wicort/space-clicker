using UnityEngine;

namespace Assets.Scripts.Arena.Character
{
    [CreateAssetMenu(fileName = "SpaceShipBuilderConfig", menuName = "Config/SpaceShipBuilderConfig")]
    public class SpaceShipBuilderConfig: ScriptableObject
    {
        public int Velocity;
        public int AngularVelocity;
        public int MaxHealthPoints;
        public int AttackDistance;
        public int AttackValue;
        public float Cooldown;
        public float RandomCoefficient;
        public string BulletPrefabAssetPath = "prefabs/Arena/Bullet";
    }
}
