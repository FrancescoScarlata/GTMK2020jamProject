using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the portals.
/// This script has not a require component for the collider to give the freedom to add the needed collider for the case.
/// </summary>
public abstract class TriggerPortal : MonoBehaviour {

    [Header("select which direction the portal is passed by the player to be activated")]
    public Direction directionOfActivation;

    /// <summary>
    /// Checks where is the player respect the portal and the direction wanted for the activation.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Vector3 playerPos = collision.transform.position;
            switch (directionOfActivation)
            {
                case Direction.up:
                    if (playerPos.y > this.transform.position.y)
                        Activate();
                    break;

                case Direction.down:
                    if (playerPos.y < this.transform.position.y)
                        Activate();
                    break;

                case Direction.left:
                    if (playerPos.x < this.transform.position.x)
                        Activate();
                    break;

                case Direction.right:
                    if (playerPos.x > this.transform.position.x)
                        Activate();
                    break;
            }
        }
    }

    /// <summary>
    /// The method called to do something when the portal is activated
    /// </summary>
    protected abstract void Activate();
}


public enum Direction
{
    up,
    down,
    left,
    right
}