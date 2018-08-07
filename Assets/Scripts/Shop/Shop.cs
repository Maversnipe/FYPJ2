using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    
    // Make this a Singleton
    private static Shop _instance;
    public static Shop Instance { get { return _instance; } }
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

    // Item Database
    private ItemDatabase m_database;

    // Use this for initialization
    void Start () {
        // Initialize m_database
        m_database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        // Set the items in the shop
        SetShopItems();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Set the items in the shop
    public void SetShopItems()
    {
        // First slot (Normal Arrow)
        gameObject.transform.GetChild(0).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Arrow]);        
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        // Second slot (Slow Arrow)
        gameObject.transform.GetChild(1).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Arrow]);
        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

        // Third slot (Homing Arrow)
        gameObject.transform.GetChild(2).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Arrow]);
        gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);

        // Fourth slot (Small Health Pot)
        gameObject.transform.GetChild(3).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Small_Health_Pot]);
        gameObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);

        // Fifth slot (Medium Health Pot)
        gameObject.transform.GetChild(4).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Medium_Health_Pot]);
        gameObject.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);

        // Sixth slot (Large Health Pot)
        gameObject.transform.GetChild(5).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Large_Health_Pot]);
        gameObject.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);

        // Seventh slot (Small Mana Pot)
        gameObject.transform.GetChild(6).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Small_Mana_Pot]);
        gameObject.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);

        // Eight slot (Medium Mana Pot)
        gameObject.transform.GetChild(7).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Medium_Mana_Pot]);
        gameObject.transform.GetChild(7).GetChild(0).gameObject.SetActive(true);

        // Ninth slot (Large Mana Pot)
        gameObject.transform.GetChild(8).GetComponent<ShopSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Large_Mana_Pot]);
        gameObject.transform.GetChild(8).GetChild(0).gameObject.SetActive(true);
    }
}
