using UnityEngine;
using UnityEngine.UI;

public class Item {
	// Item Name
	public string m_itemName;
	// Item ID
	public int m_itemID;
	// Item Description
	public string m_itemDesc;
	// Item Icon
	public Sprite m_itemIcon;
	// Item Type
	public ItemType m_itemType;
    // Check if Item is stackable
    public bool m_stackable;

	// Item Types
	public enum ItemType {
		None,
		WeaponRune,
		ArmourRune,
		Consumables,
        Arrow,
		Quest,
	}

	// Constructor
	public Item()
	{
        // Set Item name
        m_itemName = "";
        // Set Item ID
        m_itemID = -1;
        // Set Item Description
        m_itemDesc = "";
        // Set Item Type
        m_itemType = ItemType.None;
        // Set Item Icon
        m_itemIcon = null;
        // Set Item Stackable
        m_stackable = false;
    }

	// Overloaded Constructor
	public Item(string _name, int _id, string _desc, ItemType _type, bool _stackable)
	{
		// Set Item name
		m_itemName = _name;
		// Set Item ID
		m_itemID = _id;
		// Set Item Description
		m_itemDesc = _desc;
		// Set Item Type
		m_itemType = _type;
		// Set Item Icon
		m_itemIcon = Resources.Load<Sprite>("Item Icon/" + _name);
        // Set Item Stackable
        m_stackable = _stackable;
	}
}
