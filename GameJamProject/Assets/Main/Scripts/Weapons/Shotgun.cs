using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGun", menuName = "Weapons/Shotgun")]
public class Shotgun : _Weapon
{
    public float projSpeed = 5;
    public int numOfProjectiles = 4;
    public int spreadAngle;
    public float timePreShoot;
    public float timeForRecoilAfterShooting;

    GameObject myProjectile;
    Projectile instancedProj;
    Vector2 playerDir;
    float angle;
    public override IEnumerator RecoilEffect(CharacterController controller)
    {
       
        //Debug.Log("Recoil effect for gun active!");
        controller.SetIsDoindRecoil(true);
        controller.rigidBody2D.velocity = Vector2.zero;
        // has to shoot 4 projectiles
        SoundEffectManager.instance.PlaySFX(attackClip);
        for (int i=0; i< (int)(numOfProjectiles/2)+1; i++)
        {
            if (i == 0)
            {
                angle = Mathf.Atan2(controller.transform.up.y, controller.transform.up.x) * Mathf.Rad2Deg - 90;
                myProjectile = ObjectPooler.instance.SpawnFromPool(projectilePrefab.GetComponent<Projectile>().tagForSpawn,
                    controller.shotgunSpawnProjectile.position,Quaternion.Euler(0,0,angle));
                instancedProj = myProjectile.GetComponent<Projectile>();
                myProjectile.SetActive(true);
                instancedProj.SetProjInfos(controller.transform.up, projSpeed, damage);
            }
            else
            {
                playerDir = controller.transform.up;
                if (playerDir.x * playerDir.y <= 0)
                {
                    playerDir = new Vector2(playerDir.x + (spreadAngle / (360f * i)), playerDir.y + (spreadAngle / (360f * i)));
                }
                else
                {
                    playerDir = new Vector2(playerDir.x + (spreadAngle / (360f * i)), playerDir.y - (spreadAngle / (360f * i)));
                }

                angle = Mathf.Atan2(playerDir.y,playerDir.x) * Mathf.Rad2Deg - 90;
                myProjectile = ObjectPooler.instance.SpawnFromPool(projectilePrefab.GetComponent<Projectile>().tagForSpawn,
                   controller.shotgunSpawnProjectile.position, Quaternion.Euler(0, 0, angle));
                instancedProj = myProjectile.GetComponent<Projectile>();
                myProjectile.SetActive(true);
                instancedProj.SetProjInfos(playerDir, projSpeed, damage);


               
                instancedProj = myProjectile.GetComponent<Projectile>();
                playerDir = controller.transform.up;
                if (playerDir.x * playerDir.y <= 0)
                {
                    playerDir = new Vector2(playerDir.x - (spreadAngle / (360f * i)), playerDir.y - (spreadAngle / (360f * i)));
                }
                else
                {
                    playerDir = new Vector2(playerDir.x - (spreadAngle / (360f * i)), playerDir.y - (spreadAngle / (360f * i)));
                }
                angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90;
                myProjectile = ObjectPooler.instance.SpawnFromPool(projectilePrefab.GetComponent<Projectile>().tagForSpawn,
                   controller.shotgunSpawnProjectile.position, Quaternion.Euler(0, 0, angle));
                myProjectile.SetActive(true);
                instancedProj.SetProjInfos(playerDir, projSpeed, damage);
            }
        }
            // here i have to make the different range of direction
        // shoot particles

        yield return new WaitForSeconds(timePreShoot);
        controller.rigidBody2D.AddForce(-controller.transform.up * distanceRecoil,ForceMode2D.Impulse);
        CameraShake.instance.ExecuteShake();
        yield return new WaitForSeconds(timeForRecoilAfterShooting);
        controller.rigidBody2D.velocity = Vector2.zero;
        controller.SetIsDoindRecoil(false);
    }

}
