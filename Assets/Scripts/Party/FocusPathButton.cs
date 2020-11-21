using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPathButton : CharacterLevelling
{
    public CharacterSpells thisFocus;
    public CharacterLevelling levellingScript;

    public Button focusButton;
    public Text buttonText;
    public float currentFocusInvested = 0;
    public float focusMasteryLevel = 1;


    private void Start()
    {
        focusButton = GetComponent<Button>();
        buttonText = gameObject.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (levellingScript.availableFocusPoints > 0 && currentFocusInvested < focusMasteryLevel)
        {
            CharacterSpells focusRequirement = FocusRequirements(thisFocus);

            if (focusRequirement != CharacterSpells.ZERO)
            {
                if (thisFocus == CharacterSpells.T1S5)
                {
                    if (levellingScript.T1S3Left)
                    {
                        T1S3Left = true;
                    }
                }
                else if (thisFocus == CharacterSpells.T2S5)
                {
                    if (levellingScript.T2S3Left)
                    {
                        T2S3Left = true;
                    }
                }
                else if (thisFocus == CharacterSpells.T3S5)
                {
                    if (levellingScript.T3S3Left)
                    {
                        T3S3Left = true;
                    }
                }

                if (levellingScript.focusAdopted.Contains(focusRequirement))
                {
                    focusButton.interactable = true;
                }
                else
                {
                    focusButton.interactable = false;
                }
            }
        }
        else
        {
            focusButton.interactable = false;
        }

        if (levellingScript.focusingCharacter != null)
        {
            ButtonTextAssign();
        }
    }

    public void FocusButton()
    {
        levellingScript.AttemptFocus(thisFocus);
        currentFocusInvested += 1;
    }

    private void ButtonTextAssign()
    {
        if(levellingScript.focusingCharacter.GetComponent<PaladinClass>().enabled)
        {
            switch (thisFocus)
            {
                case CharacterSpells.T1S1:
                    buttonText.text = "Transfer Stress";
                    break;
                case CharacterSpells.T1S2:
                    buttonText.text = "Inner Focus";
                    break;
                case CharacterSpells.T1S3O1:
                    buttonText.text = "Burn Bright";
                    break;
                case CharacterSpells.T1S4O1:
                    buttonText.text = "Master Strategist";
                    break;
                case CharacterSpells.T1S3O2:
                    buttonText.text = "Nirvana";
                    break;
                case CharacterSpells.T1S4O2:
                    buttonText.text = "Steadfast";
                    break;
                case CharacterSpells.T1S5:
                    buttonText.text = "Bring Them to Justice";
                    break;
                case CharacterSpells.T1S6:
                    buttonText.text = "Fortitude";
                    break;
                case CharacterSpells.T1S7O1:
                    buttonText.text = "Transcendence";
                    break;
                case CharacterSpells.T1S7O2:
                    buttonText.text = "Light Speed";
                    break;
                case CharacterSpells.T2S1:
                    buttonText.text = "Absolution";
                    break;
                case CharacterSpells.T2S2:
                    buttonText.text = "Shining Compassion";
                    break;
                case CharacterSpells.T2S3O1:
                    buttonText.text = "Roling With the Punches";
                    break;
                case CharacterSpells.T2S4O1:
                    buttonText.text = "Pacify";
                    break;
                case CharacterSpells.T2S3O2:
                    buttonText.text = "Seraphic Acuity";
                    break;
                case CharacterSpells.T2S4O2:
                    buttonText.text = "Quelling Grip";
                    break;
                case CharacterSpells.T2S5:
                    buttonText.text = "Bring Them to Justice";
                    break;
                case CharacterSpells.T2S6:
                    buttonText.text = "Benevolence";
                    break;
                case CharacterSpells.T2S7O1:
                    buttonText.text = "Divine Intervention";
                    break;
                case CharacterSpells.T2S7O2:
                    buttonText.text = "Ultimate Requisition";
                    break;
                case CharacterSpells.T3S1:
                    buttonText.text = "Burning Shackles";
                    break;
                case CharacterSpells.T3S2:
                    buttonText.text = "Light Forged";
                    break;
                case CharacterSpells.T3S3O1:
                    buttonText.text = "Radiant Prison";
                    break;
                case CharacterSpells.T3S4O1:
                    buttonText.text = "Searing Palm";
                    break;
                case CharacterSpells.T3S3O2:
                    buttonText.text = "Zealous Charge";
                    break;
                case CharacterSpells.T3S4O2:
                    buttonText.text = "Divine Onslaught";
                    break;
                case CharacterSpells.T3S5:
                    buttonText.text = "Bring Them to Justice";
                    break;
                case CharacterSpells.T3S6:
                    buttonText.text = "Stoicism";
                    break;
                case CharacterSpells.T3S7O1:
                    buttonText.text = "Grand Interrogation";
                    break;
                case CharacterSpells.T3S7O2:
                    buttonText.text = "The Impossible Sword";
                    break;
            }

        }
    }

}
