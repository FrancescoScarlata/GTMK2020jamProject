using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;


    public TextMeshProUGUI magazineInfo;
    public BarUIManager HPmanager;
    public Image reloadImage;
    public TextMeshProUGUI[] weaponsName;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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

    public void UpdateMagazineInfo(int magazineIndex,int maxMagazine)
    {
        magazineInfo.text= $"{magazineIndex}/{maxMagazine}";
    }

    public void UpdateHP(float curr, float max)
    {
        HPmanager.UpdateBar(curr / max);
    }

}
