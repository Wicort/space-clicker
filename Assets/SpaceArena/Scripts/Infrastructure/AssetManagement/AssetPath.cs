using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.AssetManagement
{
    public static class AssetPath
    {
        //sprites
        public static string CommonGunPath = "gun/gunCommon";
        public static string UncommonGunPath = "gun/gunUncommon";
        public static string RareGunPath = "gun/gunRare";
        public static string EpicGunPath = "gun/gunEpic";
        public static string LegGunPath = "gun/gunLegendary";
        public static string MythGunPath = "gun/gunMyth";

        public static string CommonDronePath = "drone/droneCommon";
        public static string UncommonDronePath = "drone/droneUncommon";
        public static string RareDronePath = "drone/droneRare";
        public static string EpicDronePath = "drone/droneEpic";
        public static string LegDronePath = "drone/droneLeg";
        public static string MythDronePath = "drone/droneMyth";

        public static string CommonTouretPath = "touret/touretCommon";
        public static string UncommonTouretPath = "touret/touretUncommon";
        public static string RareTouretPath = "touret/touretRare";
        public static string EpicTouretPath = "touret/touretEpic";
        public static string LegTouretPath = "touret/touretLeg";
        public static string MythTouretPath = "touret/touretMyth";

        //configs
        public static string LightSpaceShipBuilderConfigPath = "configs/SpaceShipBuilderConfig/LightSpaceShipBuilderConfig";
        public static string MediumSpaceShipBuilderConfigPath = "configs/SpaceShipBuilderConfig/MediumSpaceShipBuilderConfig";
        public static string HeavySpaceShipBuilderConfigPath = "configs/SpaceShipBuilderConfig/HeavySpaceShipBuilderConfig";

        //prefabs
        public static string ArenaPlayerPrefabPath = "prefabs/Arena/Player";
        public static List<string> ArenaEnemyPrefabPathList = new List<string>
        {
            "prefabs/Arena/Enemy1",
        };
        public static string BulletPrefabPath = "prefabs/Arena/Bullet";
    }
}
