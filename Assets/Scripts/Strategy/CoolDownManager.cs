using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager : MonoBehaviour
{
    public static CoolDownManager coolDownManager;

    List<GameObject> characterObjects = new List<GameObject>();
    GameObject[] charactersArray;
    string[] characterTags = { "Player", "Enemy", "ActivePlayer" };

    void Awake()
    {
        if(coolDownManager == null)
        {
            coolDownManager = this;
        }
        else if(coolDownManager != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(coolDownManager);
    }

    void Start()
    {
        foreach(string tag in characterTags)
        {
            charactersArray = GameObject.FindGameObjectsWithTag(tag);
        }

        foreach(GameObject chara in charactersArray)
        {
            if(chara.GetComponent<PlayerCharacter>() !=  null || chara.GetComponent<EnemyController>() != null)
            {
                characterObjects.Add(chara);
            }
        }
    }
}
