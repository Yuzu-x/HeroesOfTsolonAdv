using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyAnalysis : MonoBehaviour
{
    public float teamScore = 0;
    public float targetScore = 165;

    public float guardianScore = 100;
    public float supportScore = 50;
    public float damageScore = 5;

    public GameObject partyObject;
    public List<GameObject> charactersOnTeam = new List<GameObject>();

    public Text advisorText;

    void Update()
    {
        CalculateTeamScore();
        ExamineTeamScore();

        foreach(GameObject character in charactersOnTeam)
        {
            PartyMember characterHired = character.GetComponent<PartyMember>();

            if(!characterHired.isHired)
            {
                charactersOnTeam.Remove(character);

                GenerationOutcome geneOutcome = character.GetComponent<GenerationOutcome>();

                if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.ENGINEER)
                {
                    teamScore -= guardianScore;
                }
                else if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.KNIGHT)
                {
                    teamScore -= guardianScore;
                }
                else if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.PALADIN)
                {
                    teamScore -= guardianScore;
                }
                else if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.PRIEST)
                {
                    teamScore -= supportScore;
                }
                else if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.MIASMATIC)
                {
                    teamScore -= supportScore;
                }
                else if(geneOutcome.recruitClass == GenerationOutcome.RecruitClass.WITCH)
                {
                    teamScore -= supportScore;
                }
                else
                {
                    teamScore -= damageScore;
                }
            }
        }
    }

    public void CalculateTeamScore()
    {
        GenerationOutcome[] partyClasses = partyObject.GetComponentsInChildren<GenerationOutcome>();
        
        foreach(GenerationOutcome classCheck in partyClasses)
        {
            switch(classCheck.recruitClass)
            {
                case GenerationOutcome.RecruitClass.ENGINEER:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += guardianScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.KNIGHT:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += guardianScore; 
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.PALADIN:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += guardianScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.PRIEST:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += supportScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.MIASMATIC:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += supportScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.WITCH:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += supportScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.ASSASSIN:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.FIGHTER:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.MAGE:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.NECROMANCER:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.RANGER:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;

                case GenerationOutcome.RecruitClass.TAMER:

                    if (!charactersOnTeam.Contains(classCheck.gameObject))
                    {
                        teamScore += damageScore;
                        charactersOnTeam.Add(classCheck.gameObject);
                    }

                    break;
            }
        }
    }

    public void ExamineTeamScore()
    {
        if(teamScore < 100)
        {
            advisorText.text = "A Guardian member would be a healthy start to your team!";
        }
        else if(teamScore < 150 && teamScore >= 100)
        {
            advisorText.text = "Your team would benefit greatly from a Support character!";
        }
        else if(teamScore < 165 && teamScore >= 150)
        {
            advisorText.text = "Gotta defeat your enemies somehow! Now might be time for a Damage character!";
        }
        else if(teamScore > 165)
        {
            advisorText.text = "This team seems a little unbalanced. If you've got a plan, then by all means!";
        }
    }
}
