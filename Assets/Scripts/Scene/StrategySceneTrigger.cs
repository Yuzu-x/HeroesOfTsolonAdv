using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Scene Trigger", menuName = "Scene Trigger/Empty")]
public class StrategySceneTrigger : ScriptableObject
{
    public string triggerLocation;
    public string triggerSceneName;
    public Scene triggerScene;

    public VictoryScript victoryScript;
    public string victoryCondition;
}
