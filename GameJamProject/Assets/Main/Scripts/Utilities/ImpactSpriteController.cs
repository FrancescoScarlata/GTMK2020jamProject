using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImpactSpriteController : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeDisappearAnddestroy());
    }

    IEnumerator WaitBeforeDisappearAnddestroy()
    {
        // wait the time of the animation
        yield return new WaitForSeconds(1.5f);
        float currTime = 0;
        Color alphaZeroColor = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b,0 );
        Color maxRendColor = mySpriteRenderer.color;
        while (currTime < 1)
        {
            mySpriteRenderer.color = Color.Lerp(maxRendColor, alphaZeroColor, currTime);
            currTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);


    }

}
