﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("The tag for the object pooler")]
    public string tagForSpawn;

    public float projMovSpeed = 2;
    public Rigidbody2D rgbd;
    [Header("The sound object made by the player. Only here for the player")]
    public GameObject effectWhenTouchingSomething;

    protected Vector2 direction;
    protected float damage;
    protected Vector2 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void OnEnable()
    {
        startingPosition = transform.position;
    }


    public void SetProjInfos(Vector2 dir,float projSpeed,float dmg)
    {
        direction = dir;
        projMovSpeed = projSpeed;
        damage = dmg;
        rgbd.velocity = direction * projMovSpeed;
    }


    private void OnTriggerEnter2D (Collider2D collider)
    {
        IDamageble enemy = collider.transform.GetComponent<IDamageble>();
        if (enemy!=null && collider.transform.tag!="Player")
            enemy.GetDamage(damage);
        Debug.Log($"Collision: {collider.transform.name}");
        if(collider.transform.tag!= transform.tag)
            Impact();
    }


    /// <summary>
    /// Method called to do all the actions during the impact, like the effect for the player project, the sound effect of the impact and the impact vfx
    /// </summary>
    protected void Impact()
    {
        gameObject.SetActive(false);
        //ObjectPooler.instance.PlaceInPool(tagForSpawn, gameObject);

       // if (projImpactSFX)
       //     SoundEffectManager.instance.PlaySFX(projImpactSFX);
    }

}
