using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyController : EnemyController
{
    public float attackRange=1f;
    protected bool isGoingBack = false;

    protected override void Start()
    {
        base.Start();
    }


    public override void DoSomething()
    {
        base.DoSomething();
        if (!isGoingBack)
        {

            dir = CharacterController.instance.transform.position - transform.position;
            if (dir.magnitude > attackRange)
            {
                dir = dir.normalized;
                Debug.DrawRay(transform.position, dir);
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
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
            Debug.Log("Collision in Enemy!");
            StartCoroutine(WaitInTrigger());
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
