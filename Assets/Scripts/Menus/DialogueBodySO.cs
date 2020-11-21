using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue Object/New Dialogue")]
public class DialogueBodySO : ScriptableObject
{
    public string dialogueLocation;
    public float dialogueLength;

    public List<string> dialogueList = new List<string>();

    public string dialogueTextOne;
    public string dialogueTextTwo;
    public string dialogueTextThree;
}
