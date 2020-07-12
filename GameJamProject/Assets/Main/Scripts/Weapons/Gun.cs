using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newGun", menuName = "Weapons/Gun")]
public class Gun : _Weapon
{
    public float projSpeed=5;
    public float timePreShoot;
    public float timeForRecoilAfterShooting;

    GameObject myProjectile;
    Projectile instancedProj;
    public override IEnumerator RecoilEffect(CharacterController controller)
    {
        //Debug.Log("Recoil effect for gun active!");
        controller.SetIsDoindRecoil(true);
        controller.rigidBody2D.velocity = Vector2.zero;

        float angle= Mathf.Atan2(controller.transform.up.y,controller.transform.up.x)*Mathf.Rad2Deg-90;
        myProjectile= ObjectPooler.instance.SpawnFromPool(projectilePrefab.GetComponent<Projectile>().tagForSpawn,controller.gunSpawnProjectile.position,Quaternion.Euler(0,0,angle));
        instancedProj = myProjectile.GetComponent<Projectile>();
        myProjectile.SetActive(true);
        instancedProj.SetProjInfos(controller.transform.up,projSpeed,damage);
        SoundEffectManager.instance.PlaySFX(attackClip);
        // shoot particles
        yield return new WaitForSeconds(timePreShoot);
        controller.rigidBody2D.AddForce(-controller.transform.up * distanceRecoil, ForceMode2D.Impulse);
        CameraShake.instance.ExecuteShake();
        yield return new WaitForSeconds(timeForRecoilAfterShooting);
        controller.rigidBody2D.velocity = Vector2.zero;
        controller.SetIsDoindRecoil(false);
    }

}
