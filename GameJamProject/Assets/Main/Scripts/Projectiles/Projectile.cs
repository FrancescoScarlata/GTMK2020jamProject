using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("The tag for the object pooler")]
    public string tagForSpawn;
    public string allyTag = "Player";

    public float projMovSpeed = 2;
    public Rigidbody2D rgbd;
    [Header("The sound object made by the player. Only here for the player")]
    public GameObject effectWhenTouchingSomething;
    public AudioClip hitClip;

    protected Vector2 direction;
    protected float damage;
    protected Vector2 startingPosition;
    protected float timeOfSpawn;
    //protected 

    void Start()
    {
        startingPosition = transform.position;
        timeOfSpawn = Time.time;
    }

    void OnEnable()
    {
        startingPosition = transform.position;
        timeOfSpawn = Time.time;
    }

    private void Update()
    {
        if (timeOfSpawn + 5 < Time.time)
            Impact();
        else
        {
            rgbd.velocity = transform.up * projMovSpeed;
        }

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
        if (enemy!=null && collider.transform.tag != allyTag)
        {
            enemy.GetDamage(damage);
            Impact();
        }
            
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
        if(effectWhenTouchingSomething)
            GameObject.Instantiate(effectWhenTouchingSomething,transform.position,Quaternion.identity);
        if (hitClip)
            SoundEffectManager.instance.PlaySFX(hitClip);
        ObjectPooler.instance.PlaceInPool(tagForSpawn, gameObject);

    }

}
