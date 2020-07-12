using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTriggerEvent : TriggerPortal
{

    protected override void Activate()
    {
        GameManager.instance.GameFinished();
    }

}
