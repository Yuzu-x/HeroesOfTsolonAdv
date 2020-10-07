using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClassClass : MonoBehaviour
{
    public List<string> spellCooling = new List<string>();
    public List<float> cooldownTimers = new List<float>();
    public TurnManager turnManager;
    public bool haveCooled = false;

    float currentCooldown1 = 500;
    float currentCooldown2 = 500;
    float currentCooldown3 = 500;
    float currentCooldown4 = 500;
    float currentCooldown5 = 500;
    float currentCooldown6 = 500;
    float currentCooldown7 = 500;
    float currentCooldown8 = 500;
    float currentCooldown9 = 500;
    float currentCooldown10 = 500;
    float currentCooldown11 = 500;
    float currentCooldown12 = 500;
    float currentCooldown13 = 500;
    float currentCooldown14 = 500;

    string coolingSpell1 = null;
    string coolingSpell2 = null;
    string coolingSpell3 = null;
    string coolingSpell4 = null;
    string coolingSpell5 = null;
    string coolingSpell6 = null;
    string coolingSpell7 = null;
    string coolingSpell8 = null;
    string coolingSpell9 = null;
    string coolingSpell10 = null;
    string coolingSpell11 = null;
    string coolingSpell12 = null;
    string coolingSpell13 = null;
    string coolingSpell14 = null;

    private void Start()
    {
        cooldownTimers.Add(currentCooldown1);
        cooldownTimers.Add(currentCooldown2);
        cooldownTimers.Add(currentCooldown3);
        cooldownTimers.Add(currentCooldown4);
        cooldownTimers.Add(currentCooldown5);
        cooldownTimers.Add(currentCooldown6);
        cooldownTimers.Add(currentCooldown7);
        cooldownTimers.Add(currentCooldown8);
        cooldownTimers.Add(currentCooldown9);
        cooldownTimers.Add(currentCooldown10);
        cooldownTimers.Add(currentCooldown11);
        cooldownTimers.Add(currentCooldown12);
        cooldownTimers.Add(currentCooldown13);
        cooldownTimers.Add(currentCooldown14);
    }

    public void Update()
    {
        if(turnManager == null)
        {
            GameObject findTurnManager = GameObject.FindGameObjectWithTag("GameController");
            turnManager = findTurnManager.GetComponent<TurnManager>();
        }

        if(turnManager.playerTurn && !haveCooled)
        {
            haveCooled = true;
            currentCooldown1 -= 1;
            currentCooldown2 -= 1;
            currentCooldown3 -= 1;
            currentCooldown4 -= 1;
            currentCooldown5 -= 1;
            currentCooldown6 -= 1;
            currentCooldown7 -= 1;
            currentCooldown8 -= 1;
            currentCooldown9 -= 1;
            currentCooldown10 -= 1;
            currentCooldown11 -= 1;
            currentCooldown12 -= 1;
            currentCooldown13 -= 1;
            currentCooldown14 -= 1;

            if(currentCooldown1 == 0 && spellCooling.Contains(coolingSpell1))
            {
                spellCooling.Remove(coolingSpell1);
                currentCooldown1 = 500;
            }
            if (currentCooldown2 == 0 && spellCooling.Contains(coolingSpell2))
            {
                spellCooling.Remove(coolingSpell2);
                currentCooldown2 = 500;
            }
            if (currentCooldown3 == 0 && spellCooling.Contains(coolingSpell3))
            {
                spellCooling.Remove(coolingSpell3);
                currentCooldown3 = 500;
            }
            if (currentCooldown4 == 0 && spellCooling.Contains(coolingSpell4))
            {
                spellCooling.Remove(coolingSpell4);
                currentCooldown4 = 500;
            }
            if (currentCooldown5 == 0 && spellCooling.Contains(coolingSpell5))
            {
                spellCooling.Remove(coolingSpell5);
                currentCooldown5 = 500;
            }
            if (currentCooldown6 == 0 && spellCooling.Contains(coolingSpell6))
            {
                spellCooling.Remove(coolingSpell6);
                currentCooldown6 = 500;
            }
            if (currentCooldown7 == 0 && spellCooling.Contains(coolingSpell7))
            {
                spellCooling.Remove(coolingSpell7);
                currentCooldown7 = 500;
            }
            if (currentCooldown8 == 0 && spellCooling.Contains(coolingSpell8))
            {
                spellCooling.Remove(coolingSpell8);
                currentCooldown8 = 500;
            }
            if (currentCooldown9 == 0 && spellCooling.Contains(coolingSpell9))
            {
                spellCooling.Remove(coolingSpell9);
                currentCooldown9 = 500;
            }
            if (currentCooldown10 == 0 && spellCooling.Contains(coolingSpell10))
            {
                spellCooling.Remove(coolingSpell10);
                currentCooldown10 = 500;
            }
            if (currentCooldown11 == 0 && spellCooling.Contains(coolingSpell11))
            {
                spellCooling.Remove(coolingSpell11);
                currentCooldown11 = 500;
            }
            if (currentCooldown12 == 0 && spellCooling.Contains(coolingSpell12))
            {
                spellCooling.Remove(coolingSpell12);
                currentCooldown12 = 500;
            }
            if (currentCooldown13 == 0 && spellCooling.Contains(coolingSpell13))
            {
                spellCooling.Remove(coolingSpell13);
                currentCooldown13 = 500;
            }
            if (currentCooldown14 == 0 && spellCooling.Contains(coolingSpell14))
            {
                spellCooling.Remove(coolingSpell14);
                currentCooldown14 = 500;
            }
        }
        else if(!turnManager.playerTurn)
        {
            haveCooled = false;
        }
    }

    public void BeginCooldown(string spellName, float initialCooldown)
    {
        if(!spellCooling.Contains(spellName))
        {
            spellCooling.Add(spellName);
            bool foundTimer = false;

            if (!foundTimer)
            {

                if (currentCooldown1 > 100)
                {
                    currentCooldown1 = initialCooldown;
                    coolingSpell1 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown2 > 100)
                {
                    currentCooldown2 = initialCooldown;
                    coolingSpell2 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown3 > 100)
                {
                    currentCooldown3 = initialCooldown;
                    coolingSpell3 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown4 > 100)
                {
                    currentCooldown4 = initialCooldown;
                    coolingSpell4 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown5 > 100)
                {
                    currentCooldown5 = initialCooldown;
                    coolingSpell5 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown6 > 100)
                {
                    currentCooldown6 = initialCooldown;
                    coolingSpell6 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown7 > 100)
                {
                    currentCooldown7 = initialCooldown;
                    coolingSpell7 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown8 > 100)
                {
                    currentCooldown8 = initialCooldown;
                    coolingSpell8 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown9 > 100)
                {
                    currentCooldown9 = initialCooldown;
                    coolingSpell9 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown10 > 100)
                {
                    currentCooldown10 = initialCooldown;
                    coolingSpell10 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown11 > 100)
                {
                    currentCooldown11 = initialCooldown;
                    coolingSpell11 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown12 > 100)
                {
                    currentCooldown12 = initialCooldown;
                    coolingSpell12 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown13 > 100)
                {
                    currentCooldown13 = initialCooldown;
                    coolingSpell13 = spellName;
                    foundTimer = true;
                }
                else if (currentCooldown14 > 100)
                {
                    currentCooldown14 = initialCooldown;
                    coolingSpell14 = spellName;
                    foundTimer = true;
                }
            }
        }
    }
}
