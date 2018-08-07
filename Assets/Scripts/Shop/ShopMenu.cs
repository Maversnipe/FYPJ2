using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour {

    // Make this a Singleton
    private static ShopMenu _instance;
    public static ShopMenu Instance { get { return _instance; } }
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

    // Info box image
    public Image m_infoBox;
    // Money Counter
    public Text m_playerAmount;
    
    // Current info box slot id
    private int m_currSlotID;
    // Check if player menu is active
    private bool m_menuIsActive;

    // Use this for initialization
    void Start ()
    {
        // Set menu to not active
        m_menuIsActive = false;
        // Set the amount of money
        ChangeMoneyAmount();
        // Set currSlotID
        m_currSlotID = -1;
    }
	
	// Update is called once per frame
	void Update () {
    }

    // Change Money Amount
    public void ChangeMoneyAmount()
    {
        // Set the new text to the player curr amount of money
        string amount = "$" + PlayerManager.Instance.m_moneyAmount.ToString();
        m_playerAmount.text = amount;
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
