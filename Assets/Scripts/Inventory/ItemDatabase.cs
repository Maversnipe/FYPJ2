using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	// List Of Items
	public List<Item> m_itemsList = new List<Item>();

	// Use this for initialization
	void Start()
	{
		m_itemsList.Add (new Rune ("Weapon Rune", 0, Item.ItemType.WeaponRune));
		m_itemsList.Add (new Rune ("Armour Rune", 1, Item.ItemType.ArmourRune));
		m_itemsList.Add (new Item ("Arrow", 2, "An Arrow!", Item.ItemType.Arrow, true));
	}
}
