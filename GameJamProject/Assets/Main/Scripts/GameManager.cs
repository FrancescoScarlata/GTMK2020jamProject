﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public float distanceForEnemyFightMusic=10;
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
            DontDestroyOnLoad(this);
           
            StartCoroutine(CheckforEnemies());
        }
        else
            Destroy(this);
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
                        if (isMusicTense)
                        {
                            soundTrackMan.Fight();
                            isMusicTense = false;
                            isEnemyInside = true;
                            break;
                        }
                }
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

    }


    public void LevelFinished()
    {

    }








}