using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public List<GameObject> characterList = new List<GameObject>();
    public Stack<GameObject> characterPanels = new Stack<GameObject>();
    public List<GameObject> panelList = new List<GameObject>();
    GameObject[] characters;
    GameObject[] slotChildren;

    string[] characterTags = {"Player", "ActivePlayer", "Enemy"};
    string[] slotTags = { "PlayerHealth", "PlayerActions" };

    void Start()
    {
        foreach (string tag in characterTags)
        {
            characters = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject character in characters)
            {
                characterList.Add(character);
            }
        }

        GameObject[] health = GameObject.FindGameObjectsWithTag("PartySlot");
        foreach(GameObject image in health)
        {
            characterPanels.Push(image);
            panelList.Add(image);
        }
    }

    void Update()
    {
        foreach(GameObject chara in characterList)
        {
            if(chara.gameObject.tag == "Player")
            {
                if (characterPanels.Count > 0)
                {
                    GameObject characterSlot = characterPanels.Peek();
                    CharacterController charaCont = chara.GetComponent<CharacterController>();
                    PanelSlotClaim slotClaim = characterSlot.GetComponent<PanelSlotClaim>();

                    if (!slotClaim.panelClaimed)
                    {
                        slotClaim.panelClaimed = true;

                        foreach (string slotTag in slotTags)
                        {
                            slotChildren = GameObject.FindGameObjectsWithTag(slotTag);

                            foreach (GameObject child in slotChildren)
                            {
                                if (child.tag == "PlayerHealth")
                                {
                                    Image playerHealth = child.GetComponent<Image>();
                                    charaCont.healthImage = playerHealth;
                                }

                                if (child.tag == "PlayerActions")
                                {
                                    Image playerActions = child.GetComponent<Image>();
                                    charaCont.actionPointImage = playerActions;
                                }
                            }
                        } 
                    }
                    else if(slotClaim.panelClaimed)
                    {
                        characterPanels.Pop();
                    }
                }
            }
        }
    }
}
