using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSoundTrackManager : MonoBehaviour
{
    public AudioSource source;
    [Header("The soundtrack audio clip")]
    public AudioClip clip;

    public void Start()
    {
        source.clip = clip;
        source.Play();
    }

}
