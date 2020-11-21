using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogueTriggerLocation;

    public GameObject dialogueObject;
    public GameObject dialogueTriggerObject;

    public void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            dialogueObject.SetActive(true);
            dialogueTriggerObject.SetActive(false);
        }
    }
}
