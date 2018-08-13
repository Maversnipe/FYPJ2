using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour {

    // Use this for initialization
    public Button[] buttons;
    private int TotalSkillpoint;
    private int unspent;
    public Text points;
    // Check if player menu is active
    private bool m_menuIsActive;
    void Start ()
    {
        // Set menu to not active
        m_menuIsActive = false;
        TotalSkillpoint = PlayerManager.Instance.totalSkillPoints;
        unspent = PlayerManager.Instance.skillPoints;
    }
	
	// Update is called once per frame
	void Update () {
        points.text = "" + unspent;
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

    // Getter for the menu is active
    public bool MenuIsActive()
    {
        return m_menuIsActive;
    }

    // Setter for the menu is active
    public void SetMenuActive(bool _isActive)
    {
        m_menuIsActive = _isActive;
    }
}
