using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTree : MonoBehaviour {

    public Button[] buttons;
    public Text levelText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "";
        for(int i = 0; i< PlayerManager.Instance.levels.Count; i++ )
        {
            if(i == 0)
            {
                levelText.text += "Health: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            if (i == 1)
            {
                levelText.text += "Mana: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            if (i == 2)
            {
                levelText.text += "Health Regen: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            if (i == 3)
            {
                levelText.text += "Mana Regen: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            if (i == 4)
            {
                levelText.text += "Damage: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            if (i == 5)
            {
                levelText.text += "Attack Speed: ";
                levelText.text += PlayerManager.Instance.levels[i];
            }
            levelText.text += "\n";
            levelText.text += "\n";

        }
		if(PlayerManager.Instance.levelPoints <=0)
        {
            for(int i =0; i<buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }
        }
	}
}
