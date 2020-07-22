using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable with the lines to be shown line by line
/// </summary>
[CreateAssetMenu(fileName = "NewLines", menuName = "Dialogues/Lines")]
public class ListOfLinesScriptable : ScriptableObject
{
    [TextArea(1,5)]
    public List<string> lines;

}
