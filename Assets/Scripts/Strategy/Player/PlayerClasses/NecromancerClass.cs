using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NecromancerClass : PlayerCharacter
{
    public PlayerCharacter playerCharacter;
    public CharacterClassClass cooldownManager;
    public CharacterLevelling levelling;
    public FocusInteraction focusInteraction;

    public string treeNameOne = "Ruination";
    public string treeNameTwo = "Subjugation";
    public string treeNameThree = "Conscription";

    void Start()
    {
        if (playerCharacter == null)
        {
            playerCharacter = this.GetComponent<PlayerCharacter>();
        }

        if (cooldownManager == null)
        {
            cooldownManager = this.GetComponent<CharacterClassClass>();
        }
    }

    void Update()
    {
        if (focusInteraction.levelling != null)
        {
            levelling = focusInteraction.levelling;
        }
        else
        {
            levelling = null;
        }
    }
}
