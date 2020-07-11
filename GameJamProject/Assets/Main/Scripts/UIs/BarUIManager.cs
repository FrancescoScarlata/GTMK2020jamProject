using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that manages to update the UI of the health bar
/// </summary>
public class BarUIManager : MonoBehaviour
{
    [Header("The handler that will ger filled over time")]
    public Image handler;

    public bool startFull = true;
    // Start is called before the first frame update
    void Start()
    {
        //shouldn't matter because it will change after anyway
        if(startFull)
            UpdateBar(1, 1);
        else
        {
            UpdateBar(0, 1);
        }
    }

    /// <summary>
    /// Method called to update the value of the bar and change the relative size
    /// </summary>
    /// <param name="currValue"></param>
    /// <param name="maxValue"></param>
    public virtual void UpdateBar(float currValue, float maxValue)
    {
        handler.fillAmount = currValue / maxValue;
    }

    /// <summary>
    /// Method called to update te value of the bar and change the relative size.
    /// </summary>
    /// <param name="currValue">the value used to update, already divided per the max Value</param>
    public virtual void UpdateBar(float currValue)
    {
        handler.fillAmount = currValue;
    }

}
