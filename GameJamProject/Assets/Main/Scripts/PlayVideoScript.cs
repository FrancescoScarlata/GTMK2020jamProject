using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a script for the Raw Image that contains the playerVideo
/// This manages the play of the video. This will be the father that will be ineherited in case of specifications
/// </summary>
public class PlayVideoScript : MonoBehaviour {

    public VideoPlayer videoP;
    public VideoClip cutscene;
    public int nextLevel;
    private RawImage image;

    //To not let to skip when the video is not even on
    //private bool isActivated = false;

    //Audio
    private AudioSource audioSource;

    // Use this for initialization
    protected virtual void Start () {
        videoP=GetComponent<VideoPlayer>();
        image=GetComponent<RawImage>();
        audioSource = GetComponent<AudioSource>();

        // only for this case
        StartVideo(cutscene);
	}

    /// <summary>
    /// This method is called to start a video
    /// </summary>
    public void StartVideo( VideoClip cutscene )
    {
        if (videoP == null)
        {
            videoP = GetComponent<VideoPlayer>();
            audioSource = GetComponent<AudioSource>();
            Debug.Log("audiosource: " + audioSource);
        }
            
        videoP.clip = cutscene;
        StartCoroutine(PlayVideoUntilEnd());
    }

    /// <summary>
    /// This method will load the video and than play it
    /// </summary>

    protected IEnumerator PlayVideoUntilEnd()
    {
        Debug.Log("audiosource inside play video: " + audioSource);
        //Set Audio Output to AudioSource
        videoP.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoP.EnableAudioTrack(0, true);
        videoP.SetTargetAudioSource(0, audioSource);

        videoP.Prepare();

        while (!videoP.isPrepared)
            yield return null;

        image.texture = videoP.texture;
        //isActivated = true;

        ModificationsPreVideoPlay();
        Debug.Log("is video prepared: " + videoP.isPrepared);
        // Play video
        videoP.Play();
        //Play Sound
        audioSource.Play();

        image.enabled = true;
        Debug.Log("is video playing: " + videoP.isPlaying);
        Debug.Log("before while");
        while (ConditionsOfPlaying())
            yield return null;

        Debug.Log("Finished");
        image.enabled=false;
        this.gameObject.SetActive(false);

        ModificationsPostVideoPlay();
        //isActivated = false;


    }

    /// <summary>
    /// this method will be called just before the play will be player and just after the play is prepared and the texture loaded
    /// </summary>
    protected virtual void ModificationsPreVideoPlay()
    {

    }

    /// <summary>
    /// This method will give the condition that may be needed while the video is playing (i.e. skipping etc.
    /// </summary>
    /// <returns></returns>
    protected virtual bool ConditionsOfPlaying()
    {
        Debug.Log(videoP.isPlaying);
        return videoP.isPlaying;
    }

    /// <summary>
    /// this method will be called after the video has finished to play and the image is already be deactivated
    /// </summary>
    protected virtual void ModificationsPostVideoPlay()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
