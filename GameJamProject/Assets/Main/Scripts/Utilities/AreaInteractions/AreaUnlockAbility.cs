using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUnlockAbility : AreaPortal
{
    [Header("Weapon index in the order of the Char COntroller (0=chainsaw)")]
    public int weaponToUnlock;

    protected override void Activate()
    {
        CharacterController.instance.UnlockWeapon(weaponToUnlock);
    }

    protected override void Deactivate()
    {
        
    }

}
