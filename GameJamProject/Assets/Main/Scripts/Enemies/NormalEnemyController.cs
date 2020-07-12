using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyController : EnemyController
{
    public float attackRange=1f;
    public AudioClip attackClip;
    protected bool isGoingBack = false;
    

    protected override void Start()
    {
        base.Start();
    }


    public override void DoSomething()
    {
        base.DoSomething();
        dir = controller.transform.position - transform.position;
        dir = dir.normalized;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
        if (!isGoingBack)
        {

            dir = controller.transform.position - transform.position;
            if (dir.magnitude > attackRange)
            {
                rgbd2D.velocity = transform.up * myInfo.movSpeed;
            }
            else
            {
                if (!isGoingBack)
                    rgbd2D.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("Collision in Enemy!");
            StartCoroutine(WaitInTrigger());
            SoundEffectManager.instance.PlaySFX(attackClip);
            CharacterController.instance.GetDamage(myInfo.damage);
            rgbd2D.AddForce(-transform.up * myInfo.movSpeed * 2, ForceMode2D.Impulse);
        }
           
    }


    IEnumerator WaitInTrigger()
    {
        isGoingBack = true;
        //GetComponent<Collider2D>().isTrigger = true;
      
        yield return new WaitForSeconds(1);
        isGoingBack = false;
        //GetComponent<Collider2D>().isTrigger = false;
    }

}
