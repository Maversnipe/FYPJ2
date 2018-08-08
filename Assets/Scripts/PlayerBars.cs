using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour {
    public Slider Health;
    public Slider Mana;
    public Slider Exp;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Health.maxValue = PlayerManager.Instance.m_maxHealth;
        Health.value = PlayerManager.Instance.m_currentHealth;
        Mana.maxValue = PlayerManager.Instance.m_maxMana;
        Mana.value = PlayerManager.Instance.m_currentMana;
        Exp.maxValue = PlayerManager.Instance.m_maxExp;
        Exp.value = PlayerManager.Instance.m_currentExp;
    }
}
