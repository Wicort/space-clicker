using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Action OnStartGameButtonClicked;

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnStartButonClick()
    {
        Debug.Log("OnStartGameButtonClicked");
        OnStartGameButtonClicked?.Invoke();
        //SceneManager.LoadScene("Clicker");
    }

    public void OnSettingsButtonClick()
    {
        SceneManager.LoadScene("Settings");
    }
}
