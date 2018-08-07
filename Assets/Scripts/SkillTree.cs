using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour {

    // Use this for initialization
    public Button[] buttons;
    private int TotalSkillpoint;
    private int unspent;
	void Start () {
        TotalSkillpoint = PlayerManager.Instance.totalSkillPoints;
        unspent = PlayerManager.Instance.skillPoints;
    }
	
	// Update is called once per frame
	void Update () {
        TotalSkillpoint = PlayerManager.Instance.totalSkillPoints;
        unspent = PlayerManager.Instance.skillPoints;
        for (int i = 0; i < PlayerManager.Instance.skills.Count; i++)
        {

               if(PlayerManager.Instance.skills[i] == 1)
                {
                    buttons[(i * 2)].interactable = false;
                    buttons[(i * 2) + 1].interactable = true;
                }
               else if(PlayerManager.Instance.skills[i] == 2)
                {
                    buttons[(i * 2)].interactable = true;
                    buttons[(i * 2) + 1].interactable = false;
                }
               else if(PlayerManager.Instance.skills[i] == 0)
                {
                    if(unspent > 0)
                    {
                        if(TotalSkillpoint > i)
                        {
                            buttons[i].interactable = true;
                            buttons[i + 1].interactable = true;
                        }
                    }
                    else
                    {
                        buttons[(i*2)].interactable = false;
                        buttons[(i*2) + 1].interactable = false;
                    }
                }
        }
	}
}
