using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitGeneration : MonoBehaviour
{
    public float recruitGeneration = 0;
    public float totalRecruits = 0;

    public List<GenerationOutcome> available = new List<GenerationOutcome>();
    public List<PlayerCharacter> hired = new List<PlayerCharacter>();

    public GameObject recruitPanel;
    public GameObject partySelect;
    public GenerationOutcome generatedCharacter;

    void Update()
    {
        if (available.Count < 10)
        {
            recruitGeneration = Random.Range(0, 13);

            switch (recruitGeneration)
            {
                case 1:
                    GameObject generatedPa = Instantiate(partySelect);
                    generatedPa.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedPa.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.PALADIN;
                    break;

                case 2:
                    GameObject generatedK = Instantiate(partySelect);
                    generatedK.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedK.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.KNIGHT;
                    break;

                case 3:
                    GameObject generatedE = Instantiate(partySelect);
                    generatedE.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedE.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.ENGINEER;
                    break;

                case 4:
                    GameObject generatedPr = Instantiate(partySelect);
                    generatedPr.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedPr.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.PRIEST;
                    break;

                case 5:
                    GameObject generatedW = Instantiate(partySelect);
                    generatedW.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedW.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.WITCH;
                    break;

                case 6:
                    GameObject generatedMi = Instantiate(partySelect);
                    generatedMi.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedMi.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.MIASMATIC;
                    break;

                case 7:
                    GameObject generatedA = Instantiate(partySelect);
                    generatedA.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedA.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.ASSASSIN;
                    break;

                case 8:
                    GameObject generatedF = Instantiate(partySelect);
                    generatedF.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedF.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.FIGHTER;
                    break;

                case 9:
                    GameObject generatedMa = Instantiate(partySelect);
                    generatedMa.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedMa.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.MAGE;
                    break;

                case 10:
                    GameObject generatedN = Instantiate(partySelect);
                    generatedN.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedN.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.NECROMANCER;
                    break;

                case 11:
                    GameObject generatedR = Instantiate(partySelect);
                    generatedR.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedR.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.RANGER;
                    break;

                case 12:
                    GameObject generatedT = Instantiate(partySelect);
                    generatedT.transform.SetParent(recruitPanel.transform);
                    generatedCharacter = generatedT.GetComponent<GenerationOutcome>();
                    generatedCharacter.recruitClass = GenerationOutcome.RecruitClass.TAMER;
                    break;
            }

            available.Add(generatedCharacter);
        }
    }


}
