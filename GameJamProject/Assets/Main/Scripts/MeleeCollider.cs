using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour,IDamageble
{
    CharacterController controller;


    public void GetDamage(float damage)
    {
        // nothing, it just intersects the bullet
        Debug.Log("Should enter here in meleeCollider");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller == null)
            controller = CharacterController.instance;
        if(collision.tag!="Player")
        {
            if (collision.GetComponent<IDamageble>()!=null)
            {
                collision.GetComponent<IDamageble>().GetDamage(controller.weapons[controller.currWeaponIndex].damage);
            }
        }
    }

}
