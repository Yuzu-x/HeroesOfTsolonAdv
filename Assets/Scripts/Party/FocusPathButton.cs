using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPathButton : CharacterLevelling
{
    public CharacterSpells thisFocus;
    public CharacterLevelling levellingScript;

    public Button focusButton;
    public float currentFocusInvested = 0;
    public float focusMasteryLevel = 1;


    private void Start()
    {
        focusButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(levellingScript.availableFocusPoints > 0 && currentFocusInvested < focusMasteryLevel)
        {
            CharacterSpells focusRequirement = FocusRequirements(thisFocus);

            if(focusRequirement != CharacterSpells.ZERO)
            {
                if(thisFocus == CharacterSpells.T1S5)
                {
                    if(levellingScript.T1S3Left)
                    {
                        T1S3Left = true;
                    }
                }
                else if(thisFocus == CharacterSpells.T2S5)
                {
                    if(levellingScript.T2S3Left)
                    {
                        T2S3Left = true;
                    }
                }
                else if(thisFocus == CharacterSpells.T3S5)
                {
                    if(levellingScript.T3S3Left)
                    {
                        T3S3Left = true;
                    }
                }

                if(levellingScript.focusAdopted.Contains(focusRequirement))
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
    }

    public void FocusButton()
    {
        levellingScript.AttemptFocus(thisFocus);
        currentFocusInvested += 1;
    }
}
