using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableCurrentMenu : MonoBehaviour
{
    public void CloseCurrent()
    {
        Image openPanelImmediate = gameObject.GetComponentInParent<Image>();
        Image openPanel = openPanelImmediate.GetComponentInParent<Image>();
        if(openPanel.GetComponent<MouseOverCharacter>() != null)
        {
            MouseOverCharacter mouseOver = openPanel.GetComponent<MouseOverCharacter>();
            mouseOver.expandedPanel = false;
        }
        openPanel.gameObject.SetActive(false);
    }
}
