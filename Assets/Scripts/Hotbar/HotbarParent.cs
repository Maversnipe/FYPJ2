using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarParent : MonoBehaviour {

    // Make this a Singleton
    private static HotbarParent _instance;
    public static HotbarParent Instance { get { return _instance; } }
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

    // Current info box slot id
    private int m_currSlotID;
    // Check if player menu is active
    private bool m_menuIsActive;

    // Use this for initialization
    void Start () {
        // Set menu to not active
        m_menuIsActive = false;
        // Set currSlotID
        m_currSlotID = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Getter for has current slot id
    public int GetCurrentSlotID()
    {
        return m_currSlotID;
    }

    // Setter for has target
    public void SetCurrSlotID(int _id)
    {
        m_currSlotID = _id;
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
