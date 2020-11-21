using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverCharacter : MonoBehaviour
{
    public GameObject mouseOverCharacterPanel;
    public Image mouseOverPanelImage;
    public Image mouseOverHealth;
    public bool mouseOverPanel = false;
    public GameObject expandedCharacterPanel;
    public Image expandedPanelImage;
    public bool expandedPanel = false;

    void Update()
    {
        CheckOverCharacter();

    }

    public void CheckOverCharacter()
    {
        Ray searchForCharacter = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit checkCharacter;

        if(Physics.Raycast(searchForCharacter, out checkCharacter))
        {
            if(checkCharacter.collider.tag == "Player" || checkCharacter.collider.tag == "ActivePlayer" || checkCharacter.collider.tag == "Enemy")
            {
                mouseOverPanel = true;

                switch(checkCharacter.collider.tag)
                {
                    case "Player":
                        PlayerCharacter playerController = checkCharacter.collider.GetComponent<PlayerCharacter>();

                            mouseOverCharacterPanel.SetActive(true);
                            mouseOverPanelImage.sprite = playerController.characterProfile;
                            mouseOverHealth.fillAmount = playerController.currentHealth / playerController.maximumHealth;

                        break;

                    case "ActivePlayer":
                        PlayerCharacter activePlayerController = checkCharacter.collider.GetComponent<PlayerCharacter>();

                            mouseOverCharacterPanel.SetActive(true);
                            mouseOverPanelImage.sprite = activePlayerController.characterProfile;
                            mouseOverHealth.fillAmount = activePlayerController.currentHealth / activePlayerController.maximumHealth;

                        break;

                    case "Enemy":
                        EnemyController enemyController = checkCharacter.collider.GetComponent<EnemyController>();

                            mouseOverCharacterPanel.SetActive(true);
                            mouseOverPanelImage.sprite = enemyController.characterProfile;
                            mouseOverHealth.fillAmount = enemyController.currentHealth / enemyController.maximumHealth;

                        break;
                }

                if(Input.GetMouseButtonUp(1) && mouseOverPanel)
                {
                    expandedPanel = true;
                    expandedCharacterPanel.SetActive(true);

                    switch (checkCharacter.collider.tag)
                    {
                        case "Player":
                            PlayerCharacter playerController = checkCharacter.collider.GetComponent<PlayerCharacter>();

                            expandedPanelImage.sprite = playerController.characterProfileExpanded;
                            mouseOverHealth.fillAmount = playerController.currentHealth / playerController.maximumHealth;

                            break;

                        case "ActivePlayer":
                            PlayerCharacter activePlayerController = checkCharacter.collider.GetComponent<PlayerCharacter>();

                            expandedPanelImage.sprite = activePlayerController.characterProfileExpanded;
                            mouseOverHealth.fillAmount = activePlayerController.currentHealth / activePlayerController.maximumHealth;

                            break;

                        case "Enemy":
                            EnemyController enemyController = checkCharacter.collider.GetComponent<EnemyController>();

                            expandedPanelImage.sprite = enemyController.characterProfileExpanded;
                            mouseOverHealth.fillAmount = enemyController.currentHealth / enemyController.maximumHealth;

                            break;
                    }
                }
            }
            else
            {
                mouseOverPanel = false;
                mouseOverCharacterPanel.SetActive(false);
            }
        }
    }
}
