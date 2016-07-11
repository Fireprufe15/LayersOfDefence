using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject CreditsScreen;
    public GameObject BuildTowerCanvas;

    public void LoadGame()
    {
        // SceneManager.LoadScene(1);
        BuildTowerCanvas.SetActive(true);
        gameObject.SetActive(false);
        CreditsScreen.SetActive(false);
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
