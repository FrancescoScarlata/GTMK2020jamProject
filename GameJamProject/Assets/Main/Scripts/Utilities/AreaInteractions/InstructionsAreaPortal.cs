using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class in case it is needed a trigger that gives a suggestion when it reacheas an area
/// </summary>
public class InstructionsAreaPortal : AreaPortal
{

    public string instructionToShow;

    protected override void Activate()
    {
        //InGameUIManager.instance.
    }

    protected override void Deactivate()
    {
        
    }

}
