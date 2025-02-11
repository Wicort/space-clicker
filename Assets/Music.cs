using Assets.SpaceArena.SaveSystem.Scripts;
using Services;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance { get; private set; }

    private AudioSource musicSource;

    [SerializeField] private AudioClip _music;

    private ISaveSystem _saveSystem;
    private GameData _gameData;
    private SettingsData _settings;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = GetComponent<AudioSource>();
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

        _gameData = _saveSystem.LoadGame();
        _settings = _gameData.Settings;
        if (_settings.IsMusicMute == false)
        {
            MusicOn();
        }
        else
            MusicOff();
    }

    public bool IsMusicOff()
    {
        return _settings.IsMusicMute;
    }

    public void MusicOn()
    {
        _settings.IsMusicMute = false;
        musicSource.clip = _music;
        musicSource.Play();
        _saveSystem.SaveSettings(_settings);
    }

    public void MusicOff()
    {
        _settings.IsMusicMute = true;
        musicSource.Pause();
        _saveSystem.SaveSettings(_settings);
    }
}
