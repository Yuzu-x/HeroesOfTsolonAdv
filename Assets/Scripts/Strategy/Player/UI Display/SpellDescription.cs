using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDescription : MonoBehaviour
{
    public List<Button> activeButton = new List<Button>();
    public bool activeCharacter = false;

    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("ActivePlayer"))
        {
            activeCharacter = true;
        }
        else
        {
            activeCharacter = false;
        }

        if(activeCharacter)
        {
            GameObject spellPanel = GameObject.FindGameObjectWithTag("SpellPanel");

            foreach (Button buttonChild in spellPanel.GetComponentsInChildren<Button>())
            {
                activeButton.Add(buttonChild);
                PlayerCharacter playerController = GameObject.FindGameObjectWithTag("ActivePlayer").GetComponent<PlayerCharacter>();

                if (playerController.spellOneDescription == null)
                {
                    string thisSpellDescription = playerController.spellOneDescription;
                }
                else if (playerController.spellTwoDescription == null)
                {
                    string thisSpellDescription = playerController.spellTwoDescription;
                }
                else if(playerController.spellThreeDescription == null)
                {
                    string thisSpellDescription = playerController.spellThreeDescription;
                }
                else if(playerController.spellFourDescription == null)
                {
                    string thisSpellDescription = playerController.spellFourDescription;
                }
            }
        }
    }
}
