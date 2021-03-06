﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;


    public TextMeshProUGUI magazineInfo;
    public BarUIManager HPmanager;
    public Image reloadImage;
    public TextMeshProUGUI[] weaponsName;
    public TextMeshProUGUI timerText;
    public Button mainMenuButton;
    public GameObject pauseMenuPanel;

    protected float timer;
    protected WaitForSeconds waitBeweenUpdated = new WaitForSeconds(1f);
    protected bool isTimerOn=true;
    protected bool isMenupen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (!PlayerPrefs.HasKey("timer"))
            {
                timer = 0;
            }
            else
            {
                timer = PlayerPrefs.GetFloat("timer");
            }
            if (SceneManager.GetActiveScene().name =="FirstLevel" )
                timer = 0;
            StartCoroutine(UpdateTheTimer());
        } 
        else
            Destroy(gameObject);
    }

    public void UnlockWeapon(int weaponIndex)
    {
        if (weaponIndex + 1 < weaponsName.Length && weaponsName[weaponIndex + 1]!=null)
            weaponsName[weaponIndex+1].transform.parent.parent.gameObject.SetActive(true);
    }

    public void LockWeapon(int weaponIndex)
    {
        if (weaponIndex + 1<weaponsName.Length && weaponsName[weaponIndex + 1] != null)
            weaponsName[weaponIndex+1].transform.parent.parent.gameObject.SetActive(false);
    }

    public void UpdateCurrWeapon(int currWeapon)
    {
        for(int i=0; i<weaponsName.Length; i++)
        {
            if (i == currWeapon + 1)
            {
                weaponsName[i].color = Color.green;
            }
            else
                weaponsName[i].color = Color.white;
        }
    }

    public void StartReloading()
    {
        reloadImage.enabled = true;
    }

    public void StopReloading()
    {
        reloadImage.enabled = false;
    }

    public void UpdateMagazineInfo(int magazineIndex,int maxMagazine, bool show=true)
    {
        if(show)
            magazineInfo.text= $"{magazineIndex}/{maxMagazine}";
        else
            magazineInfo.text = "";
    }

    public void UpdateHP(float curr, float max)
    {
        HPmanager.UpdateBar(curr / max);
    }


    public void StopTimer()
    {
        isTimerOn = false;
        PlayerPrefs.SetFloat("timer", timer);
        if (PlayerPrefs.HasKey("bestTime"))
        {
            if (PlayerPrefs.GetFloat("bestTime") > timer)
            {
                PlayerPrefs.SetFloat("bestTime", timer);
                timerText.color = Color.green;
            }     
        }
        else
        {
            PlayerPrefs.SetFloat("bestTime", timer);
            timerText.color = Color.green;
        }
        StartCoroutine(OpenTheMainMenuButton());
    }


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isMenupen)
            {
                isMenupen = false;
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1;
                isTimerOn = true;
                CharacterController.instance.UnlockInteractions();
            }
            else
            {
                isMenupen = true;
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f;
                isTimerOn = false;
                CharacterController.instance.BlockFinalInteration();
            }
        }

        if (isTimerOn)
        {
            timerText.text = "" + timer;
            timer += Time.deltaTime;
        }
        
    }


    IEnumerator UpdateTheTimer()
    {
        while(isTimerOn)
        {
            PlayerPrefs.SetFloat("timer", timer);
            PlayerPrefs.SetFloat("totTime", PlayerPrefs.GetFloat("totTime") + 1);
            yield return waitBeweenUpdated;
        }
    }

    IEnumerator OpenTheMainMenuButton()
    {
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("currLevel", 2);
        mainMenuButton.gameObject.SetActive(true);
    }



}
