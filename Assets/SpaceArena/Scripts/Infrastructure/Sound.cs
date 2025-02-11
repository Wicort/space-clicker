using Assets.SpaceArena.SaveSystem.Scripts;
using Services;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; }

    private AudioSource soundSource;

    [SerializeField] private AudioClip _clickShot;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _inspectButtonClick;

    private ISaveSystem _saveSystem;
    private GameData _gameData;
    private SettingsData _settings;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            soundSource = GetComponent<AudioSource>();
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _saveSystem = AllServices.Container.Single<ISaveSystem>();

        Debug.Log($"_saveSystem = {_saveSystem}");
        _gameData = _saveSystem.LoadGame();
        _settings = _gameData.Settings;
    }

    public bool IsSoundMute()
    {
        return _settings.IsSoundMute;
    }

    public void SoundOn()
    {
        _settings.IsSoundMute = false;
        PlayInspectClick();
        _saveSystem.SaveSettings(_settings);
    }

    public void SoundOff()
    {
        _settings.IsSoundMute = true;
        PlayInspectClick();
        _saveSystem.SaveSettings(_settings);
    }

    public void SetSoundVolume(float volume)
    {
        _settings.SoundVolume = volume;
        soundSource.volume = _settings.SoundVolume;
        PlayInspectClick();
    }

    public void PlayClickShot()
    {
        PlaySound(_clickShot);
    }

    public void PlayButtonClick()
    {
        PlaySound(_buttonClick);
    }

    public void PlayInspectClick()
    {
        PlaySound(_inspectButtonClick);
    }

    private void PlaySound(AudioClip _sound)
    {
        if (_settings.IsSoundMute) return;

        soundSource.PlayOneShot(_sound);
    }
}
