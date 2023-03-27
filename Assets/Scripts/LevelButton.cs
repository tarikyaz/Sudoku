using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] LevelSetting levelSetting;
    [SerializeField] Button button;
    [SerializeField] TMP_Text text;
    private void Start()
    {
        text.text = "Level " + (transform.GetSiblingIndex()+1).ToString();
        button.onClick.AddListener(() => {
            GameManager.Instance.StartLevel(levelSetting);
        });
    }
}
