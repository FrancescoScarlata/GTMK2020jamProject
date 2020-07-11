using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public int firstLevel=3;
    public GameObject creditScene;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenCredits()
    {
        creditScene.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void BackToMainMenu()
    {
        creditScene.SetActive(false);
    }

}
