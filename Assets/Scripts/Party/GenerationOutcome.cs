using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerationOutcome : MonoBehaviour
{
    public Text classNameText;
    public Image classSymbolImage;
    public Image roleSymbolImage;

    public Image guardianSymbol;

    public enum RecruitClass
    {
        PALADIN,
        KNIGHT,
        ENGINEER,
        PRIEST,
        WITCH,
        MIASMATIC,
        ASSASSIN,
        FIGHTER,
        MAGE,
        NECROMANCER,
        RANGER,
        TAMER
    }

    public RecruitClass recruitClass;
    void Update()
    {
        switch(recruitClass)
        {
            case RecruitClass.PALADIN:
                classNameText.text = "Paladin";

                guardianSymbol.enabled = true;
                break;

            case RecruitClass.KNIGHT:
                classNameText.text = "Knight";

                guardianSymbol.enabled = true;
                break;

            case RecruitClass.ENGINEER:
                classNameText.text = "Engineer";

                guardianSymbol.enabled = true;
                break;

            case RecruitClass.PRIEST:
                classNameText.text = "Priest";
                break;

            case RecruitClass.WITCH:
                classNameText.text = "Witch";
                break;

            case RecruitClass.MIASMATIC:
                classNameText.text = "Miasmatic";
                break;

            case RecruitClass.ASSASSIN:
                classNameText.text = "Assassin";
                break;

            case RecruitClass.FIGHTER:
                classNameText.text = "Fighter";
                break;

            case RecruitClass.MAGE:
                classNameText.text = "Mage";
                break;

            case RecruitClass.NECROMANCER:
                classNameText.text = "Necromancer";
                break;

            case RecruitClass.RANGER:
                classNameText.text = "Ranger";
                break;

            case RecruitClass.TAMER:
                classNameText.text = "Tamer";
                break;
        }
    }
}
