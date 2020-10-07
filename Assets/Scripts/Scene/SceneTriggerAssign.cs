using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerAssign : MonoBehaviour
{
    public StrategySceneTrigger nextScene;
    public SceneController sceneController;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            sceneController.LoadScene(nextScene.triggerSceneName);
        }
    }
}
