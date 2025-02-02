using System;
using System.Collections.Generic;

namespace Assets.SpaceArena.Scripts.Infrastructure.Localization
{
    public partial class EnLocalizationService : ILocalizationService
    {
        private Dictionary<string, ItemLocalization> _itemLoc = new Dictionary<string, ItemLocalization> 
        {
            { "CommonGun", new ItemLocalization("Standard Laser","Basic laser module for dealing with minor threats.") },
            { "UNCOMMONGun", new ItemLocalization("Pulse Emitter", "Enhanced laser with improved accuracy and rate of fire.") },
            { "RAREGun", new ItemLocalization("Plasma Cannon", "Powerful plasma shot that pierces light armor.") },
            { "EPICGun", new ItemLocalization("Quantum Disintegrator", "High-tech weapon that disintegrates targets at the molecular level.") },
            { "LEGENDARYGun", new ItemLocalization("Gravity Cannon", "Space-warping weapon that deals massive damage.") },
            { "MYTHICALGun", new ItemLocalization("Star Destroyer", "Legendary weapon capable of destroying an entire ship with one shot.") },

            { "CommonDrone", new ItemLocalization("Sentinel Drone", "Simple drone for combat support.") },
            { "UNCOMMONDrone", new ItemLocalization("Assault Drone", "Drone with improved rate of fire and accuracy.") },
            { "RAREDrone", new ItemLocalization("Plasma Drone", "Drone that fires plasma charges, dealing moderate damage.") },
            { "EPICDrone", new ItemLocalization("Ghost Drone", "Stealth drone that attacks enemies from unexpected angles.") },
            { "LEGENDARYDrone", new ItemLocalization("Destroyer", "Powerful drone capable of annihilating enemies with barrage fire.") },
            { "MYTHICALDrone", new ItemLocalization("Archangel", "Legendary drone that deals devastating damage and repairs allies' shields.") },
            
            { "CommonTouret", new ItemLocalization("Standard Turret", "Basic turret for ship defense.") },
            { "UNCOMMONTouret", new ItemLocalization("Auto Turret", "Turret with improved rate of fire and accuracy.") },
            { "RARETouret", new ItemLocalization("Plasma Turret", "Turret that fires plasma charges, dealing moderate damage.") },
            { "EPICTouret", new ItemLocalization("Ion Turret", "Turret that paralyzes enemies with ion discharges.") },
            { "LEGENDARYTouret", new ItemLocalization("Thunderstrike", "Powerful turret that fires energy blasts, obliterating enemies.") },
            { "MYTHICALTouret", new ItemLocalization("Doomsday", "Legendary turret capable of annihilating an entire fleet in seconds.") },            
        };

        public string GetItemName(string itemId)
        {
            return _itemLoc[itemId].Name;
        }

        public string GetItemDescription(string itemId)
        {
            return _itemLoc[itemId].Description;
        }

        public string GetUIByKey(string key)
        {
            return key;
        }
    }
}
