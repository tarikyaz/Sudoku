using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] LevelManager currrentLevel;
    [SerializeField] LevelSetting levelSetting;
    public void Init()
    {
        currrentLevel.Init(levelSetting);

    }

}
