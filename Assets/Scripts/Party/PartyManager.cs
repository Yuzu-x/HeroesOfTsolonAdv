using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public List<GameObject> members = new List<GameObject>();
    public List<PlayerCharacter> partyList = new List<PlayerCharacter>();
    public GenerationOutcome generatedMember;
    public GameObject panelParent;
    public SceneController sceneController;
    public GameObject partyHandPanel;
    public Canvas partyManagerCanvas;

    public GameObject[] partySlots;
    public GameObject[] recruitAvail;

    void Update()
    {
        DontDestroyOnLoad(this);

        recruitAvail = GameObject.FindGameObjectsWithTag("RecruitTab");

        foreach (GameObject playerPanel in recruitAvail)
        {
            GenerationOutcome panelChar = playerPanel.GetComponentInChildren<GenerationOutcome>();

            if(panelChar)
            {
                PartyMember playerPanelMember = playerPanel.GetComponent<PartyMember>();
                generatedMember = playerPanel.GetComponent<GenerationOutcome>();

                if (playerPanel.transform.parent == panelParent.transform)
                {
                    playerPanelMember.isHired = true;

                    switch (generatedMember.recruitClass)
                    {
                        case GenerationOutcome.RecruitClass.PALADIN:
                            playerPanelMember.memberSO = playerPanelMember.paladinSO;
                            playerPanelMember.paladin.enabled = true;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.KNIGHT:
                            playerPanelMember.memberSO = playerPanelMember.knightSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.ENGINEER:
                            playerPanelMember.memberSO = playerPanelMember.engineerSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.PRIEST:
                            playerPanelMember.memberSO = playerPanelMember.priestSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.WITCH:
                            playerPanelMember.memberSO = playerPanelMember.witchSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.MIASMATIC:
                            playerPanelMember.memberSO = playerPanelMember.miasmaticSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.ASSASSIN:
                            playerPanelMember.memberSO = playerPanelMember.assassinSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.FIGHTER:
                            playerPanelMember.memberSO = playerPanelMember.fighterSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.MAGE:
                            playerPanelMember.memberSO = playerPanelMember.mageSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.NECROMANCER:
                            playerPanelMember.memberSO = playerPanelMember.necromancerSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.RANGER:
                            playerPanelMember.memberSO = playerPanelMember.rangerSO;
                            members.Add(playerPanel);
                            break;

                        case GenerationOutcome.RecruitClass.TAMER:
                            playerPanelMember.memberSO = playerPanelMember.tamerSO;
                            members.Add(playerPanel);
                            break;
                    }
                }
            }
        }

        if(partyHandPanel == null)
        {
            partyHandPanel = GameObject.FindGameObjectWithTag("PartyHandPanel");
        }
        else if(partyHandPanel != null)
        {
            foreach(GameObject member in members)
            {
                member.transform.SetParent(partyHandPanel.transform);
                member.tag = "CharInHand";
                Image handImage;
                foreach(Image image in member.GetComponentsInChildren<Image>())
                {
                    if (image.enabled == false && image.tag != "LogoImage")
                    {
                        handImage = image;
                        image.enabled = true;
                    }
                    else
                    {
                        image.enabled = false;
                    }
                }

            }
            partyManagerCanvas.gameObject.SetActive(false);
        }
    }

    public void LoadStrategyScene(string sceneName)
    {

    }
}
