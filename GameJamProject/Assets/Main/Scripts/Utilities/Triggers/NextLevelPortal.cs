using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : TriggerPortal
{
    


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void Activate()
    {
        Debug.Log("Activate portal");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
}
