using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IDamageble
{
    public EnemyInfos myInfo;
    public Rigidbody2D rgbd2D;
    public AudioClip deathCLip;

    protected Vector2 dir;
    protected float myHP;
    protected CharacterController controller;

    // 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myHP = myInfo.HP;
        if (controller == null)
            controller = CharacterController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(CharacterController.instance.transform.position, transform.position) > myInfo.aggroRange)
        {
            rgbd2D.velocity = Vector2.zero;
            return;
        }
            
        if (myHP > 0)
            DoSomething();
        else
        {
            this.enabled = false;
            Die();
           
        }
            
    }


    public virtual void DoSomething() { 
    }

    public void GetDamage(float damage)
    {
        myHP -= damage;
    }

    public void Die()
    {
        Debug.Log("Im dead!");
        if (deathCLip)
            SoundEffectManager.instance.PlaySFX(deathCLip);
        Destroy(gameObject);
    }

}
