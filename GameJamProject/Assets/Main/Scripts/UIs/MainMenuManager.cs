using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Security.Cryptography;

public class MainMenuManager : MonoBehaviour
{
    public string firstLevelName="FirstLevel";
    public GameObject creditScene;
    public AudioSource mainMenuAudioSource;
    public Slider volumeSlider;
    public GameObject mainMenuGO;
    [Space(10)]

    public GameObject bestTime;
    public GameObject totTimePlayed;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI totTimePlayedText;

    [Space(10)]
    
    public GameObject afterPlayMenuGO;
    public TextMeshProUGUI descriptionText;
    public ListOfLinesScriptable premiseLines;
    public Button nextLineButton;
    public float timeToTransitions=0.8f;

    protected int currIndexLine=0;


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


    public void PressPlayButton()
    {
        mainMenuGO.SetActive(false);
        afterPlayMenuGO.SetActive(true);
        StartCoroutine(SmoothLineTransition());
        descriptionText.text = premiseLines.lines[currIndexLine++];
    }

    public void NextLine()
    {
        if (currIndexLine < premiseLines.lines.Count)
        {
            StartCoroutine(SmoothLineTransition());
            descriptionText.text = premiseLines.lines[currIndexLine++];
        }
        else
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
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


    /// <summary>
    /// Method called by the volume slider to change the volume of game
    /// </summary>
    /// <param name="value"></param>
    public void OnChangedVolumeValue(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        mainMenuAudioSource.volume = 0.5f * volumeSlider.value;
    }


    IEnumerator SmoothLineTransition()
    {
        nextLineButton.gameObject.SetActive(false);
        float currTime = 0;
        Color maxColor = descriptionText.color;
        Color tmpColor = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, 0);
        descriptionText.color = tmpColor;
        while (currTime < timeToTransitions)
        {
            descriptionText.color = Color.Lerp(tmpColor, maxColor, currTime / timeToTransitions);
            currTime += Time.deltaTime;
            yield return null;
        }
        nextLineButton.gameObject.SetActive(true);

    }


}
