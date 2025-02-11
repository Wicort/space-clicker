using System;

namespace Assets.SpaceArena.SaveSystem.Scripts
{
    [Serializable]
    public class SettingsData
    {
        public float SoundVolume;
        public bool IsSoundMute;
        public bool IsMusicMute;

        public SettingsData()
        {
            SoundVolume = 1;
            IsSoundMute = false;

            IsMusicMute = true;
        }
    }
}
