using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnStartButonClick()
    {
        SceneManager.LoadScene("Clicker");
    }

    public void OnSettingsButtonClick()
    {
        SceneManager.LoadScene("Settings");
    }
}
