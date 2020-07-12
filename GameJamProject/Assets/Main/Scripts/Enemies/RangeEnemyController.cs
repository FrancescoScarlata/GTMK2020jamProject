using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    public float attackRange = 15f;
    public Projectile projectilePrefab;
    public AudioClip attackClip;
    public float attackSpeed = 2;
    public float projSpeed;
    public Transform alienSpawn;

    protected float timeLastAttack;
    protected GameObject myProjectile;
    protected Projectile instancedProj;
    protected float distance;
    protected float angle;

    public override void DoSomething()
    {
        base.DoSomething();
        dir = controller.transform.position - transform.position;

        distance = dir.magnitude;
        dir = dir.normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0,angle);
        if (distance > attackRange)
        {
            rgbd2D.velocity = transform.up * myInfo.movSpeed;
        }
        else
        {
            rgbd2D.velocity = Vector2.zero;
            if (timeLastAttack + attackSpeed < Time.time)
            {
                timeLastAttack = Time.time;
                myProjectile = ObjectPooler.instance.SpawnFromPool(projectilePrefab.tagForSpawn, alienSpawn.position, Quaternion.Euler(0, 0, angle));
                instancedProj = myProjectile.GetComponent<Projectile>();
                myProjectile.SetActive(true);
                instancedProj.SetProjInfos(controller.transform.up, projSpeed, myInfo.damage);
                SoundEffectManager.instance.PlaySFX(attackClip);
            }    
        }
        
    }
    

}
