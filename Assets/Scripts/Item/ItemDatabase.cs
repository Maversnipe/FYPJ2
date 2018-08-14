using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	// List Of Items
	public List<Item> m_itemsList = new List<Item>();

    // Enum that contains all the items
    // Can be used to get item id
    public enum TheItem
    {
        Weapon_Rune = 0,
        Armour_Rune = 1,
        Arrow = 2,
        Slow_Arrow = 3,
        Homing_Arrow = 4,
        Small_Health_Pot = 5,
        Medium_Health_Pot = 6,
        Large_Health_Pot = 7,
        Small_Mana_Pot = 8,
        Medium_Mana_Pot = 8,
        Large_Mana_Pot = 9,
    }

	// Use this for initialization
	void Start()
	{
		m_itemsList.Add(new Rune("Weapon Rune", 0, Item.ItemType.Weapon_Rune));
		m_itemsList.Add(new Rune("Armour Rune", 1, Item.ItemType.Armour_Rune));
		m_itemsList.Add(new ShopItem("Arrow", 2, "A normal arrow that can be used to kill enemies", Item.ItemType.Arrow, true, 20));
        m_itemsList.Add(new ShopItem("Slow Arrow", 3, "An arrow that can slow down your enemies", Item.ItemType.Arrow, true, 20));
        m_itemsList.Add(new ShopItem("Homing Arrow", 4, "An arrow that follows your enemies", Item.ItemType.Arrow, true, 20));
        m_itemsList.Add(new ShopItem("Small Health Pot", 5, "Adds 10 Health", Item.ItemType.Arrow, true, 20));
        m_itemsList.Add(new ShopItem("Medium Health Pot", 6, "Adds 30 Health", Item.ItemType.Arrow, true, 30));
        m_itemsList.Add(new ShopItem("Large Health Pot", 7, "Adds 50 Health", Item.ItemType.Arrow, true, 40));
        m_itemsList.Add(new ShopItem("Small Mana Pot", 8, "Adds 10 Mana", Item.ItemType.Arrow, true, 20));
        m_itemsList.Add(new ShopItem("Medium Mana Pot", 9, "Adds 30 Mana", Item.ItemType.Arrow, true, 30));
        m_itemsList.Add(new ShopItem("Large Mana Pot", 10, "Adds 50 Mana", Item.ItemType.Arrow, true, 40));

    }

    // Use a name to find an item
    public Item GetItem(string _name)
    {
        // Loop through item database
        for(int i = 0; i < m_itemsList.Count; ++i)
        {
            // Check if name matches
            if(_name.Equals(m_itemsList[i].m_itemName))
            {
                // Return if name matches
                return m_itemsList[i];
            }
        }
        return null;
    }
}
