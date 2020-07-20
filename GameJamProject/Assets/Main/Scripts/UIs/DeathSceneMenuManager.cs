using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneMenuManager : MonoBehaviour
{
    public AudioSource deathAudioSource;

    private void Start()
    {
        deathAudioSource.volume = 0.5f * PlayerPrefs.GetFloat("volume");    
    }
    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currLevel"));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
