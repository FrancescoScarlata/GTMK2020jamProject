using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    CharacterController controller;



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
