using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages the sound effects in the game
/// </summary>
public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    [Header("The prefab of the sound effect")]
    public AudioSourceWithTime audioPrefab;

    protected float currentVolume=1;

    protected List<AudioSourceWithTime> mySources=new List<AudioSourceWithTime>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentVolume = PlayerPrefs.GetFloat("volume");
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method called to play some sfx
    /// </summary>
    /// <param name="clip">the clip passed to play</param>
    public void PlaySFX(AudioClip clip)
    {
        if (mySources.Count < 1)
        {
            AddSource(clip);
        }
        else
        {
            foreach (AudioSourceWithTime source in mySources)
            {
                if(source.isFinished)
                {
                    source.Play(clip);
                    return;
                }
            }
            // if we are here it means that no one was available
            AddSource(clip);
        }
    }

    /// <summary>
    /// Method called to add a source because no other sources are available to play
    /// </summary>
    /// <param name="clip"></param>
    protected void AddSource(AudioClip clip)
    {
        AudioSourceWithTime aS = Instantiate(audioPrefab);
        aS.transform.SetParent(transform);
        mySources.Add(aS);
        aS.ChangeVolume(currentVolume);
        aS.Play(clip);
    }

    /// <summary>
    /// Method called to change all the volumes in the sound effects
    /// </summary>
    /// <param name="newVolume"></param>
    public void ChangeVolume(float newVolume)
    {
        currentVolume = newVolume;
        foreach(AudioSourceWithTime source in mySources)
        {
            source.ChangeVolume(currentVolume);
        }
    }
}
