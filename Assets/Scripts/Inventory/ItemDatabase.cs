using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	// List Of Items
	public List<Item> m_itemsList = new List<Item>();

	// Use this for initialization
	void Start()
	{
		m_itemsList.Add (new Item ("Sword", 0, "A Sword!", 4, Item.ItemType.Weapon));
		m_itemsList.Add (new Item ("Armour", 1, "An Armour!", 4, Item.ItemType.Armour));
	}
}
