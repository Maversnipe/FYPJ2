using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public Text points;

    private void Update()
    {
        points.text = "" + PlayerManager.Instance.levelPoints;
    }

    public void AddHealth()
    {
        if(PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[0] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
    public void AddMana()
    {
        if (PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[1] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
    public void AddHealthRegen()
    {
        if (PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[2] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
    public void AddManaRegen()
    {
        if (PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[3] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
    public void AddDamage()
    {
        if (PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[4] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
    public void AddAttackSpeed()
    {
        if (PlayerManager.Instance.levelPoints > 0)
        {
            PlayerManager.Instance.levels[5] += 1;
            PlayerManager.Instance.levelPoints--;
        }
    }
}
