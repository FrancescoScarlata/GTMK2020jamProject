using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the portals.
/// This script has not a require component for the collider to give the freedom to add the needed collider for the case.
/// </summary>
public abstract class AreaPortal : MonoBehaviour
{

    [Header("select which direction the portal is passed by the player to be activated")]
    public Direction directionOfActivation;


    /// <summary>
    /// Checks when a player is inside the tigger
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate();
        }
    }

    /// <summary>
    /// Checks where is the player respect the portal and the direction wanted for the activation.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Deactivate();
        }
    }

    /// <summary>
    /// The method called to do something when the portal is activated
    /// </summary>
    protected abstract void Activate();

    protected abstract void Deactivate();
}