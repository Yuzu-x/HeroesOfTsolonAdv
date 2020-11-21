using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLevelling : MonoBehaviour
{
    public PartyMember focusingCharacter;
    public PartyMemberSO focusingCharacterSO;
    public VictoryScript victoryScript;
    public float totalVictories = 0;
    public bool addedVictory = false;
    public float availableFocusPoints;
    public float totalFocusPoints;
    public float earnedFocusPoints;

    public Text treeOneName;
    public Text treeTwoName;
    public Text treeThreeName;

    public bool T1S3Left;
    public bool T1S3LeftLocked;
    public bool T1S3RightLocked;
    public bool T1S7LeftLocked;
    public bool T1S7RightLocked;
    public bool T2S3Left;
    public bool T2S3Locked;
    public bool T3S3Left;
    public bool T3S3Locked;

    //Tree:Spell:Option
    public enum CharacterSpells
    {
        ZERO,
        T1S1,
        T1S2,
        T1S3O1,
        T1S3O2,
        T1S4O1,
        T1S4O2,
        T1S5,
        T1S6,
        T1S7O1,
        T1S7O2,
        T2S1,
        T2S2,
        T2S3O1,
        T2S3O2,
        T2S4O1,
        T2S4O2,
        T2S5,
        T2S6,
        T2S7O1,
        T2S7O2,
        T3S1,
        T3S2,
        T3S3O1,
        T3S3O2,
        T3S4O1,
        T3S4O2,
        T3S5,
        T3S6,
        T3S7O1,
        T3S7O2,
        LOCKED,
    }

    public List<CharacterSpells> focusAdopted;

    void Update()
    {
        if(victoryScript = null)
        {
            GameObject results = GameObject.FindGameObjectWithTag("ResultsManager");
            victoryScript = results.GetComponent<VictoryScript>();
        }

        if(victoryScript != null && victoryScript.victoryAchieved)
        {
            totalVictories += 1;
            availableFocusPoints += earnedFocusPoints;
            totalFocusPoints += earnedFocusPoints;
            addedVictory = true;
        }

        if(focusingCharacter != null)
        {
            focusingCharacterSO = focusingCharacter.memberSO;
            AssignNames();
        }
        else
        {
            focusingCharacterSO = null;
        }
    }

    public void Focus()
    {
        GameObject[] focusTrees = GameObject.FindGameObjectsWithTag("FocusTree");
        Button[] focusButtons;

        foreach(GameObject tree in focusTrees)
        {
            focusButtons = tree.GetComponentsInChildren<Button>();
        }
    }

    public void FocusSelected(CharacterSpells focusSelected)
    {
        Debug.Log("Currently adding " + focusSelected + " to focusAdopted");

        if (!focusAdopted.Contains(focusSelected))
        {
            focusAdopted.Add(focusSelected);
        }
    }

    public CharacterSpells FocusRequirements(CharacterSpells focusSelect)
    {
        switch (focusSelect)
        {
            case CharacterSpells.T1S2: return CharacterSpells.T1S1;
            case CharacterSpells.T1S3O1:
                T1S3Left = true;
                if (!T1S3RightLocked)
                {
                    T1S3LeftLocked = true;
                    return CharacterSpells.T1S2;
                }
                else
                {
                    return CharacterSpells.LOCKED;
                }
            case CharacterSpells.T1S3O2:
                T1S3Left = false;
                if (!T1S3LeftLocked)
                {
                    T1S3RightLocked = true;
                    return CharacterSpells.T1S2;
                }
                else
                {
                    return CharacterSpells.LOCKED;
                }
            case CharacterSpells.T1S4O1: return CharacterSpells.T1S3O1;
            case CharacterSpells.T1S4O2: return CharacterSpells.T1S3O2;
            case CharacterSpells.T1S5:
                if(T1S3Left)
                {
                    Debug.Log("Should return T1S4O1");
                    return CharacterSpells.T1S4O1;
                }
                else
                {
                    return CharacterSpells.T1S4O2;
                }
            case CharacterSpells.T1S6: return CharacterSpells.T1S5;
            case CharacterSpells.T1S7O1:
                if (!T1S7RightLocked)
                {
                    T1S7LeftLocked = true;
                    return CharacterSpells.T1S6;
                }
                else
                {
                    return CharacterSpells.LOCKED;
                }
            case CharacterSpells.T1S7O2:
                if (!T1S7LeftLocked)
                {
                    T1S7RightLocked = true;
                    return CharacterSpells.T1S6;
                }
                else
                {
                    return CharacterSpells.LOCKED;
                }
            case CharacterSpells.T2S2: return CharacterSpells.T2S1;
            case CharacterSpells.T2S3O1:
                T2S3Left = true;
                return CharacterSpells.T2S2;
            case CharacterSpells.T2S3O2:
                T2S3Left = false;
                return CharacterSpells.T2S2;
            case CharacterSpells.T2S4O1: return CharacterSpells.T2S3O1;
            case CharacterSpells.T2S4O2: return CharacterSpells.T2S3O2;
            case CharacterSpells.T2S5:
                if (!T2S3Left)
                {
                    return CharacterSpells.T2S4O2;
                }
                else
                {
                    return CharacterSpells.T2S4O1;
                }
            case CharacterSpells.T2S6: return CharacterSpells.T2S5;
            case CharacterSpells.T2S7O1: return CharacterSpells.T2S6;
            case CharacterSpells.T2S7O2: return CharacterSpells.T2S6;
            case CharacterSpells.T3S2: return CharacterSpells.T3S1;
            case CharacterSpells.T3S3O1:
                T3S3Left = true;
                return CharacterSpells.T3S2;
            case CharacterSpells.T3S3O2:
                T3S3Left = false;
                return CharacterSpells.T3S2;
            case CharacterSpells.T3S4O1: return CharacterSpells.T3S3O1;
            case CharacterSpells.T3S4O2: return CharacterSpells.T3S3O2;
            case CharacterSpells.T3S5:
                if (T3S3Left)
                {
                    return CharacterSpells.T3S4O1;
                }
                else
                {
                    return CharacterSpells.T3S4O2;
                }
            case CharacterSpells.T3S6: return CharacterSpells.T3S5;
            case CharacterSpells.T3S7O1: return CharacterSpells.T3S6;
            case CharacterSpells.T3S7O2: return CharacterSpells.T3S6;
            case CharacterSpells.LOCKED: return CharacterSpells.LOCKED;
        }

        return CharacterSpells.ZERO;
    }

    public void AttemptFocus(CharacterSpells focusSelect)
    {
        CharacterSpells focusRequirement = FocusRequirements(focusSelect);

        if(focusRequirement != CharacterSpells.ZERO)
        {
            if(focusAdopted.Contains(focusRequirement))
            {
                if (availableFocusPoints - 1 >= 0)
                {
                    availableFocusPoints -= 1;
                    FocusSelected(focusSelect);
                }
            }
            else
            {
                Debug.Log("AttemptFocus fails");
            }
        }
        else
        {
            if(availableFocusPoints - 1 >= 0)
            {
                availableFocusPoints -= 1;
                FocusSelected(focusSelect);
            }
        }
    }

    void AssignNames()
    {
        if(focusingCharacter.GetComponent<PaladinClass>().enabled)
        {
            PaladinClass paladin = focusingCharacter.GetComponent<PaladinClass>();
            treeOneName.text = paladin.treeNameOne;
            treeTwoName.text = paladin.treeNameTwo;
            treeThreeName.text = paladin.treeNameThree;
        }
    }
}
