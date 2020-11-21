using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInteractionCheck : MonoBehaviour
{
    void Update()
    {
        foreach(Image image in gameObject.GetComponentsInChildren<Image>())
        {
            if(!image.enabled)
            {
                image.enabled = true;
            }
        }
    }
}
