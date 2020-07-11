using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGun", menuName = "Weapons/Rocket")]
public class Rocket : _Weapon
{
    public float projSpeed = 2;
    public float timePreShoot;
    public float timeForRecoilAfterShooting;

    GameObject myProjectile;
    Projectile instancedProj;
    public override IEnumerator RecoilEffect(CharacterController controller)
    {
        //Debug.Log("Recoil effect for gun active!");
        controller.SetIsDoindRecoil(true);
        controller.rigidBody2D.velocity = Vector2.zero;

        myProjectile = GameObject.Instantiate(projectilePrefab, controller.rocketSpawnProjectile.position, Quaternion.identity);
        instancedProj = myProjectile.GetComponent<Projectile>();
        instancedProj.SetProjInfos(controller.transform.up, projSpeed, damage);

        yield return new WaitForSeconds(timePreShoot);
        controller.rigidBody2D.AddForce(-controller.transform.up * distanceRecoil, ForceMode2D.Impulse);

        controller.isKnocked = true;
        controller.rigidBody2D.velocity = Vector2.zero;
        // animation knocked
        yield return new WaitForSeconds(timeForRecoilAfterShooting);
        controller.isKnocked = false;
        // animation back up
        //controller.rigidBody2D.velocity = Vector2.zero;
        controller.SetIsDoindRecoil(false);
    }



}
