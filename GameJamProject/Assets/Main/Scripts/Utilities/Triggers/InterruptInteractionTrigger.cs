using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class InterruptInteractionTrigger : TriggerPortal
{
    public AudioClip finalMusic;
    protected override void Activate()
    {
        CharacterController.instance.BlockFinalInteration();
        GameManager.instance.transform.GetComponentInChildren<SoundtrackManager>().audioSourceTense.clip = finalMusic;
        GameManager.instance.transform.GetComponentInChildren<SoundtrackManager>().audioSourceTense.Play();
    }

}
