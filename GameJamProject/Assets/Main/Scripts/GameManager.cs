using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public float distanceForEnemyFightMusic=20;
    public int currentLevel;
    public SoundtrackManager soundTrackMan;

    protected bool isMusicTense = true;
    //protected CharacterController controller;
    protected Collider2D[] colliders;
    protected WaitForSeconds waitMusic = new WaitForSeconds(2);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
            StartCoroutine(CheckforEnemies());
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    IEnumerator CheckforEnemies()
    {
        bool isEnemyInside;
        while (this)
        {
            isEnemyInside = false;
            if (CharacterController.instance != null)
            {
                colliders = Physics2D.OverlapCircleAll(CharacterController.instance.transform.position, distanceForEnemyFightMusic);
                foreach (Collider2D coll in colliders)
                {
                    if (coll.GetComponent<EnemyController>() != null)
                    {
                        //Debug.Log($"collider name: {coll.name}");
                        
                        if (isMusicTense)
                        {
                            soundTrackMan.Fight();
                            isMusicTense = false; 
                        }
                        isEnemyInside = true;
                        break;
                    }
                        
                }
                //Debug.Log($"Enemy inside: {isEnemyInside}");
                if (!isEnemyInside && !isMusicTense)
                {
                    soundTrackMan.OutOfFight();
                    isMusicTense = true;
                }

            }
           
            yield return waitMusic;
        }
       
    }



    public void PlayerDead()
    {
        // does something to retry etc
        SceneManager.LoadScene("DeathScene");
    }


    private void OnLevelWasLoaded(int level)
    {
        if (level == 1 || level == 2)
            Destroy(this.gameObject);
    }

    public void GameFinished()
    {
        CharacterController.instance.StopMoving();
        InGameUIManager.instance.StopTimer();
    }








}
