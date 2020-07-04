using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundEffectManager : MonoBehaviour
{
    public AudioSource source;
    [Header("The sfx audio clip")]
    public AudioClip clip;

    public void Start()
    {
        source.clip = clip;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (source.isPlaying)
            {
                source.Stop();
               
            }
            source.clip = clip;
            source.Play();
        }
    }
}
