﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour {

    // Make this a Singleton
    private static Hotbar _instance;
    public static Hotbar Instance { get { return _instance; } }
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
    void Start ()
    { 
        // Initialize m_database
        m_database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        // Set the items in the hotbar
        SetHotbarItems();

    }
	
	// Update is called once per frame
	void Update () {
		// If player presses the left arrow
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // If the selected slot is not the first slot on the left 
            if(HotbarParent.Instance.GetCurrentSlotID() > 0)
            {
                // Set prev slot as not selected
                transform.GetChild(HotbarParent.Instance.GetCurrentSlotID()).GetChild(0).gameObject.SetActive(false);
                // Set new slot id
                HotbarParent.Instance.SetCurrSlotID(HotbarParent.Instance.GetCurrentSlotID() - 1);
                // Set new slot as selected
                transform.GetChild(HotbarParent.Instance.GetCurrentSlotID()).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                // Set prev slot as not selected
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                // Set new slot id
                HotbarParent.Instance.SetCurrSlotID(8);
                // Set new slot as selected
                transform.GetChild(8).GetChild(0).gameObject.SetActive(true);
            }
        }
        // If player clicks right arrow
        else if (Input.GetKeyDown(KeyCode.RightArrow) ||  Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // If the selected slot is not the first slot on the left 
            if (HotbarParent.Instance.GetCurrentSlotID() < 8)
            {
                // Set prev slot as not selected
                transform.GetChild(HotbarParent.Instance.GetCurrentSlotID()).GetChild(0).gameObject.SetActive(false);
                // Set new slot id
                HotbarParent.Instance.SetCurrSlotID(HotbarParent.Instance.GetCurrentSlotID() + 1);
                // Set new slot as selected
                transform.GetChild(HotbarParent.Instance.GetCurrentSlotID()).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                // Set prev slot as not selected
                transform.GetChild(8).GetChild(0).gameObject.SetActive(false);
                // Set new slot id
                HotbarParent.Instance.SetCurrSlotID(0);
                // Set new slot as selected
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }



    }

    // Set the items in the hotbar
    public void SetHotbarItems()
    {
        // First slot (Normal Arrow)
        transform.GetChild(0).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Arrow]);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        HotbarParent.Instance.SetCurrSlotID(0);

        // Second slot (Slow Arrow)
        transform.GetChild(1).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Slow_Arrow]);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

        // Third slot (Homing Arrow)
        transform.GetChild(2).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Homing_Arrow]);
        transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

        // Fourth slot (Small Health Pot)
        transform.GetChild(3).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Small_Health_Pot]);
        transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(3).GetChild(0).gameObject.SetActive(false);

        // Fifth slot (Medium Health Pot)
        transform.GetChild(4).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Medium_Health_Pot]);
        transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(4).GetChild(0).gameObject.SetActive(false);

        // Sixth slot (Large Health Pot)
        transform.GetChild(5).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Large_Health_Pot]);
        transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(5).GetChild(0).gameObject.SetActive(false);

        // Seventh slot (Small Mana Pot)
        transform.GetChild(6).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Small_Mana_Pot]);
        transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(6).GetChild(0).gameObject.SetActive(false);

        // Eight slot (Medium Mana Pot)
        transform.GetChild(7).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Medium_Mana_Pot]);
        transform.GetChild(7).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(7).GetChild(0).gameObject.SetActive(false);

        // Ninth slot (Large Mana Pot)
        transform.GetChild(8).GetComponent<HotbarSlot>().AddItem(m_database.m_itemsList[(int)ItemDatabase.TheItem.Large_Mana_Pot]);
        transform.GetChild(8).GetChild(0).gameObject.SetActive(true);
        // Set selection
        transform.GetChild(8).GetChild(0).gameObject.SetActive(false);
    }

    // Use the equipment
    public void UseHotbarItem(int _hotbarID)
    {
        // Set the item as hotbar item
        ShopItem theItem = (ShopItem)transform.GetChild(_hotbarID).GetComponent<HotbarSlot>().m_hotbarItem;
        // Check what item type it is
        switch(theItem.m_itemType)
        {
            case Item.ItemType.Consumables:
                // Remove one of item from inventory
                Inventory.Instance.Remove(theItem.m_itemName, 1);
                switch (theItem.m_itemID)
                {
                    // If it is health potion
                    case 5:
                        // Give player 10 health
                        PlayerManager.Instance.AddHP(10);
                        break;
                    case 6:
                        // Give player 30 health
                        PlayerManager.Instance.AddHP(30);
                        break;
                    case 7:
                        // Give player 50 health
                        PlayerManager.Instance.AddHP(50);
                        break;
                    // If it is mana potion
                    case 8:
                        // Give player 10 mana
                        PlayerManager.Instance.AddMana(10);
                        break;
                    case 9:
                        // Give player 30 mana
                        PlayerManager.Instance.AddMana(30);
                        break;
                    case 10:
                        // Give player 50 mana
                        PlayerManager.Instance.AddMana(50);
                        break;
                }
                break;
            case Item.ItemType.Arrow:
                break;
        }
    }
}
