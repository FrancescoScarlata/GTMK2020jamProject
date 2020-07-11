using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projMovSpeed = 2;
    public Rigidbody2D rgbd;

    protected Vector2 direction;
    protected float damage;

    public void SetProjInfos(Vector2 dir,float projSpeed,float dmg)
    {
        direction = dir;
        projMovSpeed = projSpeed;
        damage = dmg;
        rgbd.velocity = direction * projMovSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageble enemy = collision.transform.GetComponent<IDamageble>();
        if (enemy!=null)
            enemy.GetDamage(damage);
    }


}
