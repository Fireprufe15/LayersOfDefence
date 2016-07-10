using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject CreditsScreen;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        if (!CreditsScreen.activeInHierarchy)
        {
            CreditsScreen.SetActive(true);
        }
        else
        {
            CreditsScreen.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
