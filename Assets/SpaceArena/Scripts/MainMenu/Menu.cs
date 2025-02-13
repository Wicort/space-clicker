using Michsky.MUIP;
using Services;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Action OnStartGameButtonClicked;
    public GameObject SettingsPanel;

    public SwitchManager SoundSwitcher;
    public SwitchManager MusicSwitcher;

    private void Start()
    {
        SettingsPanel.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Sound.instance.PlayButtonClick();
        Application.Quit();
    }

    public void OnStartButonClick()
    {
        Sound.instance.PlayButtonClick();
        OnStartGameButtonClicked?.Invoke();
    }

    public void OnSettingsButtonClick()
    {
        Sound.instance.PlayButtonClick();

        if (Sound.instance.IsSoundMute())
            SoundSwitcher.SetOff();
        else
            SoundSwitcher.SetOn();

        if (Music.instance.IsMusicOff())
            MusicSwitcher.SetOff();
        else
            if (MusicSwitcher.isOn == false) MusicSwitcher.SetOn();

        SettingsPanel.SetActive(true);
    }

    public void OnCloseSettingsButtonClick()
    {
        Sound.instance.PlayButtonClick();
        SettingsPanel.SetActive(false);
    }

    public void SoundOn()
    {
        Sound.instance.SoundOn();
    }
    public void SoundOff()
    {
        Sound.instance.SoundOff();
    }

    public void MusicOn()
    {
        Music.instance.MusicOn();
    }

    public void MusicOff()
    {
        Music.instance.MusicOff();
    }
}
