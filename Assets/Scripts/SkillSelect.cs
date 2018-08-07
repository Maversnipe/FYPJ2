using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : MonoBehaviour {

    public void AddSwordSkill()
    {
        if(PlayerManager.Instance.skillPoints > 0)
        {
            PlayerManager.Instance.skills[PlayerManager.Instance.totalSkillPoints - PlayerManager.Instance.skillPoints] = 2;
            PlayerManager.Instance.skillPoints--;
        }

    }
    public void AddBowSkill()
    {
        if (PlayerManager.Instance.skillPoints > 0)
        {
            PlayerManager.Instance.skills[PlayerManager.Instance.totalSkillPoints - PlayerManager.Instance.skillPoints] = 1;
            PlayerManager.Instance.skillPoints--;
        }

    }
}
