using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manage the single  audiosource for the sfx for the sound effect manager
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioSourceWithTime : MonoBehaviour
{
    public AudioSource source;
    [HideInInspector]
    public bool isFinished=true;


    public void Play(AudioClip clipsfx)
    {
        StartCoroutine(PlayAndWait(clipsfx));
    }

    /// <summary>
    /// public volume that will be changed by the sound effext manager
    /// </summary>
    /// <param name="newValue"></param>
    public void ChangeVolume(float newValue)
    {
        source.volume = newValue;
    }


    IEnumerator PlayAndWait(AudioClip clipSfx)
    {
        isFinished = false;
        source.PlayOneShot(clipSfx);
        yield return new WaitForSeconds(clipSfx.length);
        isFinished = true;
    }
}
