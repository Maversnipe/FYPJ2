using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public int currentLevel;
    public int maxHP;
    public int currentHP;

    // Make this a Singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
