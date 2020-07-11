using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Weapons/Chainsaw")]
public class Chainsaw : _Weapon
{
    public float timePreRotation;
    public float timeForRotation;
    public float degreeRotation;


    protected float charRot;
    protected float timeToRotate = 0;

    public override IEnumerator RecoilEffect(CharacterController controller)
    {
        //Debug.Log("Recoil effect for chainsaw active!");
        controller.SetIsDoindRecoil(true);
        controller.rigidBody2D.velocity = Vector2.zero;
        controller.anim.SetTrigger("AttackTrigger");
        SoundEffectManager.instance.PlaySFX(attackClip);
        yield return new WaitForSeconds(timePreRotation);
        // get z axe of the player
        charRot = controller.transform.rotation.eulerAngles.z;
        CameraShake.instance.ExecuteShake();
        timeToRotate = 0;
        
        while (timeToRotate <= 1)
        {
            controller.transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(charRot, charRot + degreeRotation, timeToRotate));
            timeToRotate += Time.deltaTime / timeForRotation;
            yield return null;
        }
        
        controller.transform.rotation = Quaternion.Euler(0, 0, charRot + degreeRotation);
        controller.SetIsDoindRecoil(false);
    }

}
