using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	// Item Name
	public string m_itemName;
	// Item ID
	public int m_itemID;
	// Item Description
	public string m_itemDesc;
	// Item Icon
	public Texture2D m_itemIcon;
	// Item Power
	public int m_itemPower;
	// Item Type
	public ItemType m_itemType;

	// Item Types
	public enum ItemType {
		None,
		Weapon,
		Armour,
		Consumables,
		Quest,
	}

	// Constructor
	public Item()
	{
	}

	// Overloaded Constructor
	public Item(string _name, int _id, string _desc, int _power, ItemType _type)
	{
		// Set Item name
		m_itemName = _name;
		// Set Item ID
		m_itemID = _id;
		// Set Item Description
		m_itemDesc = _desc;
		// Set Item Power
		m_itemPower = _power;
		// Set Item Type
		m_itemType = _type;
		// Set Item Icon
		m_itemIcon = Resources.Load<Texture2D>("Item Icon/" + _name);
	}
}
