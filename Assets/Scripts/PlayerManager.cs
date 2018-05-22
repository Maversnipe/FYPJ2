using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int maxHP;
    public int currentHP;
	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(currentHP < 0)
        {
            //died
        }
	}

    public void AddHP(int value)
    {
        currentHP += value;
    }

    public void MinusHP(int value)
    {
        currentHP -= value;
    }
}
