using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public float activeVolume=0.5f;
    public AudioSource audioSourceTense;
    public AudioSource audioSourceFight;

    private void Start()
    {

        audioSourceFight.volume = 0;
        audioSourceTense.volume = activeVolume;
    }

    public void Fight()
    {
        audioSourceFight.Stop();
        audioSourceFight.Play();
        audioSourceFight.volume =activeVolume*PlayerPrefs.GetFloat("volume");
        audioSourceTense.volume = 0;
    }
    

    public void OutOfFight()
    {
        audioSourceFight.volume = 0;

        audioSourceTense.Stop();
        audioSourceTense.Play();
        audioSourceTense.volume = activeVolume * PlayerPrefs.GetFloat("volume");
    }

    // TO DO smooth track transition between out of fight and in fight

}
