using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public int firstLevel=3;
    public GameObject creditScene;
    public GameObject bestTime;
    public GameObject totTimePlayed;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI totTimePlayedText;
    public Slider volumeSlider;
    public AudioSource mainMenuAudioSource;

    private void Start()
    {
        if (PlayerPrefs.HasKey("bestTime"))
        {
            bestTime.SetActive(true);
            bestTimeText.text = ""+PlayerPrefs.GetFloat("bestTime");
        }
        if (PlayerPrefs.HasKey("totTime"))
        {
            totTimePlayed.SetActive(true);
            totTimePlayedText.text = "" + PlayerPrefs.GetFloat("totTime");
        }
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
            mainMenuAudioSource.volume = 0.5f * volumeSlider.value;
        }
        else
        {
            volumeSlider.value = 1;
            PlayerPrefs.SetFloat("volume", 1);
        }


    }

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



    public void OnChangedVolumeValue(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        mainMenuAudioSource.volume = 0.5f * volumeSlider.value;
    }

}
