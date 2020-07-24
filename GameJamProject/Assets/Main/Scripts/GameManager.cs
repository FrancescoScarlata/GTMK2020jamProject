using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float distanceForEnemyFightMusic=20;
    public SoundtrackManager soundTrackMan;

    //protected CharacterController controller;
    protected Collider2D[] colliders;
    protected WaitForSeconds waitMusic = new WaitForSeconds(2);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += CheckMyLevel;
            StartCoroutine(CheckforEnemies());
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    IEnumerator CheckforEnemies()
    {
        Debug.Log("Check started!");
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
                        //Debug.Log($"collider  name for game manager : {coll.name}");

                        soundTrackMan.Fight();
                        isEnemyInside = true;
                        break;
                    }
                        
                }
                //Debug.Log($"Enemy inside: {isEnemyInside}");
                if (!isEnemyInside)
                {
                    //Debug.Log("No enemy found!");
                    soundTrackMan.OutOfFight();
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


    private void CheckMyLevel(Scene newScene, LoadSceneMode loadMode)
    {
        if (newScene.buildIndex<2)
            Destroy(instance.gameObject);
        else
        {
            PlayerPrefs.SetInt("currLevel", newScene.buildIndex);
        }
    }

    public void GameFinished()
    {
        CharacterController.instance.StopMoving();
        InGameUIManager.instance.StopTimer();
    }








}
