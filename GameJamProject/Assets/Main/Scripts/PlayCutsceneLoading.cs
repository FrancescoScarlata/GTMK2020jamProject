using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage the video player of the video when it is loading another scene
/// </summary>
public class PlayCutsceneLoading : PlayVideoScript {

    public LoaderSceneController loaderController;


    protected override void ModificationsPreVideoPlay()
    {
        videoP.isLooping = true;
    }

    protected override bool ConditionsOfPlaying()
    {
        if (loaderController.isLoading == false && videoP.isLooping!=false)
            videoP.isLooping = false;
        Debug.Log("is video playing: " + videoP.isPlaying);
        return base.ConditionsOfPlaying();

    }

    protected override void ModificationsPostVideoPlay()
    {
        loaderController.AllowActivation();
    }

}
