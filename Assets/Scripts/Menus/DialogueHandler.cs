using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public DialogueBodySO dialogueBody;
    public bool lastText = false;

    public GameObject dialogueObject;
    public Text dialogueTextBox;
    public float dialogueLength;
    public List<string> dialogueStringList = new List<string>();
    public Text dialogueButtonText;

    public string currentDialogue;
    public string nextDialogue;

    public void Start()
    {
        dialogueLength = dialogueBody.dialogueLength;

        dialogueTextBox.text = dialogueBody.dialogueTextOne;
    }

    public void Update()
    {
        if (dialogueBody.dialogueTextOne != null)
        {
            if (!dialogueStringList.Contains(dialogueBody.dialogueTextOne))
            {
                dialogueStringList.Add(dialogueBody.dialogueTextOne);
            }

            if (currentDialogue != dialogueBody.dialogueTextOne)
            {
                currentDialogue = dialogueBody.dialogueTextOne;
            }
        }
        else if (dialogueBody.dialogueTextTwo != null)
        {
            if (!dialogueStringList.Contains(dialogueBody.dialogueTextTwo))
            {
                dialogueStringList.Add(dialogueBody.dialogueTextTwo);
            }
        }
        else if (dialogueBody.dialogueTextThree != null)
        {
            if (!dialogueStringList.Contains(dialogueBody.dialogueTextThree))
            {
                dialogueStringList.Add(dialogueBody.dialogueTextThree);
            }
        }

        switch (dialogueLength)
        {
            case 3:
                nextDialogue = dialogueBody.dialogueTextTwo;

                break;

            case 2:
                nextDialogue = dialogueBody.dialogueTextThree;
                break;

            case 1:
                lastText = true;
                nextDialogue = null;

                break;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            NextDialogue();
        }

        if(!lastText)
        {
            dialogueButtonText.text = "Next";
        }
        else if(lastText)
        {
            dialogueButtonText.text = "Close";
        }
    }

    public void DialogueTrigger()
    {
        dialogueObject.SetActive(true);
    }

    public void NextDialogue()
    {
        currentDialogue = nextDialogue;
        dialogueTextBox.text = currentDialogue;
        dialogueLength -= 1;

        if(nextDialogue == null)
        {
            dialogueObject.SetActive(false);
        }
    }
}
