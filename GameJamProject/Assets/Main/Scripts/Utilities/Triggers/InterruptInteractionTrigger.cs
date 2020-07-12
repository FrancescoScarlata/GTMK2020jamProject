using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class InterruptInteractionTrigger : TriggerPortal
{
    protected override void Activate()
    {
        CharacterController.instance.BlockFinalInteration();
    }

}
