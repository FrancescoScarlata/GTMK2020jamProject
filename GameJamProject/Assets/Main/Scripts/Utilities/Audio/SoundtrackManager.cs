using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public float activeVolume=0.5f;
    public AudioSource audioSourceTense;
    public AudioSource audioSourceFight;
    protected bool isMusicTense;

    private void Start()
    {
        isMusicTense = true;
        audioSourceFight.volume = 0;
        audioSourceTense.volume = activeVolume* PlayerPrefs.GetFloat("volume");
    }

    public void Fight()
    {
        if (isMusicTense)
        {
            isMusicTense = false;
            audioSourceFight.Stop();
            audioSourceFight.Play();
            audioSourceFight.volume = activeVolume * PlayerPrefs.GetFloat("volume");
            audioSourceTense.volume = 0;
        }
       
    }
    

    public void OutOfFight()
    {
        if (!isMusicTense)
        {
            isMusicTense = true;
            audioSourceFight.volume = 0;
            audioSourceTense.Stop();
            audioSourceTense.Play();
            audioSourceTense.volume = activeVolume * PlayerPrefs.GetFloat("volume");
        }
    }

    // TO DO smooth track transition between out of fight and in fight

}
