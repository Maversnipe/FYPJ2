using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	// List of items in inventory
	public List<Item> m_inventory = new List<Item>();
	// Item Database
	private ItemDatabase m_database;
	// Slots
	public List<Item> m_slots = new List<Item>();

	// Number of slots for inventory
	public int m_slotsX, m_slotsY;

	// Item Information
	private string m_itemInfo;

	// Skin of individual slot
	public GUISkin m_slotSkin;

	// Item that is being dragged by cursor
	private Item m_draggedItem;
	// Prev index of item being dragged
	private int m_draggedPrevIndex;

	// Bool that determines if inventory is active
	private bool m_invActive = false;
	// Bool that determined if info box is active
	private bool m_infoActive = false;
	// Bool that determines if an item is being dragged
	private bool m_itemIsDrag = false;
	// Bool that keeps track if left mouse button is down
	private bool m_LMBDown;

	// Use this for initialization
	void Start()
	{
		// Initialise Slots
		for(int i = 0; i < (m_slotsX * m_slotsY); ++i)
		{
			// Put blank/empty item in slots
			m_slots.Add (new Item());
			// Put blank/empty item in inv
			m_inventory.Add (new Item ());
		}

		// Initialize m_database
		m_database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent <ItemDatabase>();

		/*
		// Add all the items from database into inventory
		for(int i = 0; i < m_database.m_itemsList.Count; ++i)
		{
			m_inventory.Add(m_database.m_itemsList[i]);
		}
		*/

		// Add an item into inventory
		AddItem(0);
		AddItem(1);
	}

	// Update is called once per frame
	void Update()
	{
		// Checks if inventory button is pressed (Can change to key press later)
		if(Input.GetButtonDown ("Inventory"))
		{
			// Make m_invActive the opposite of itself
			m_invActive = !m_invActive;
			print (m_invActive);
		}

		if (Input.GetMouseButtonUp (0))
		{
			// Set Left Mouse Button Down to false
			m_LMBDown = false;
		}
	}

	// This is called for rendering and for handling GUI events
	void OnGUI()
	{
		// Set info box as empty
		m_itemInfo = "";

		// Assign skin if not done so already
		if (!GUI.skin)
		{
			GUI.skin = m_slotSkin;
		}

		// Checks if inventory is active
		if(m_invActive)
		{
			// If inventory is active, draw inventory
			DrawInventory ();

			// Checks if info box is active
			if(m_infoActive)
			{
				// If info box is active, draw info box 
				GUI.Box (new Rect(Event.current.mousePosition.x + 10f, Event.current.mousePosition.y, 200, 200), m_itemInfo, m_slotSkin.GetStyle("Info_Box"));

				// Set info box as inactive
				m_infoActive = false;
			}
		}

		// Checks if there is an item being dragged
		if(m_itemIsDrag)
		{
			// If there is an item being dragged, draw item
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), m_draggedItem.m_itemIcon);
		}
	}

	// Function that helps to draw Inventory
	void DrawInventory()
	{
		// Variable that represents the amount to offset the inventory
		float offset_x = Screen.width / 2f - (m_slotsX / 2) * 60;
		float offset_y = Screen.height / 2f - (m_slotsY / 2) * 60;

		// Draw background for inventory slots
		GUI.Box(new Rect (offset_x - 5, offset_y - 5, m_slotsX * 60, m_slotsY * 60), "", m_slotSkin.GetStyle("Info_Box"));

		// Index of the curr slot 
		int index = 0;

		// Variable that contains the curr event
		Event e = Event.current;

		// Draw each slot of the inventory and 
			// item in slot individually in a for loop
		for(int x = 0; x < m_slotsX; ++x)
		{
			for(int y = 0; y < m_slotsY; ++y)
			{
				// Get pos of curr slot and put into a variable
				Rect slotRect = new Rect (x * 60 + offset_x, y * 60 + offset_y, 50, 50);
				// Draw slot
				GUI.Box (slotRect, "", m_slotSkin.GetStyle ("Inv_Slot"));

				// Make curr index of slots list be same as curr index of inventory list
				m_slots[index] = m_inventory[index];

				// Checks if there are any items in the curr slot
				if(m_slots[index].m_itemName != null)
				{ // If there is an item in slot
					// Draw item in slot
					GUI.DrawTexture (slotRect, m_slots[index].m_itemIcon);

					// Checks if mouse cursor is within slot
					if(slotRect.Contains (e.mousePosition))
					{
						// Create the item's info
						m_itemInfo = CreateItemInfo (m_slots[index]);
						// Set info box as active
						m_infoActive = true;

						// Check if mouse has been clicked and dragged
						if(e.button == 0 && e.isMouse && !m_itemIsDrag && !m_LMBDown)
						{
							// Set item is drag to true
							m_itemIsDrag = true;
							// Set dragged item to the item clicked on
							m_draggedItem = m_slots[index];
							// Empty the inventory slot
							m_inventory[index] = new Item();
							// Set item's prev index
							m_draggedPrevIndex = index;
							// Set Left Mouse Button Down to true
							m_LMBDown = true;
							// Skip to next iteration in loop
							continue;
						}

						// Check if mouse has been clicked while item is being dragged
						if(e.button == 0 && e.isMouse && m_itemIsDrag && !m_LMBDown)
						{
							// Set item is drag to true
							m_itemIsDrag = false;
							// Set the dragged item's prev slot to contain the current inventory slot item
							m_inventory[m_draggedPrevIndex] = m_inventory[index];
							// Set this inventory slot to contain the dragged item 
							m_inventory[index] = m_draggedItem;
							// Set dragged item to null
							m_draggedItem = null;
							// Set Left Mouse Button Down to true
							m_LMBDown = true;
						}
					}
				}
				else
				{ // If there is no item in slot
					// Check if mouse has been clicked while item is being dragged
					if(slotRect.Contains(e.mousePosition) && e.button == 0 && e.isMouse && m_itemIsDrag && !m_LMBDown)
					{
						// Set item is drag to true
						m_itemIsDrag = false;
						// Set this inventory slot to contain the dragged item 
						m_inventory[index] = m_draggedItem;
						// Set dragged item to null
						m_draggedItem = null;
						// Set Left Mouse Button Down to true
						m_LMBDown = true;
					}
				}

				// Update index
				++index;
			}
		}
	}

	// Create the item's info to show player
	string CreateItemInfo(Item _item)
	{
		// Variable which will contain the item info, starting with item name
		string info = _item.m_itemName;

		// Add Item description
		info += "\n\n" + _item.m_itemDesc;

		// Returns the info
		return info;
	}

	// Function that adds item into inventory based on item ID
	void AddItem(int _id)
	{
		// Loop through inventory
		for(int i = 0; i < m_inventory.Count; ++i)
		{
			// Checks for an empty slot
			if(m_inventory[i].m_itemName == null)
			{
				// Loop through database to find item of specified ID
				for (int j = 0; j < m_database.m_itemsList.Count; ++j)
				{
					// Checks item's ID for specified item ID
					if (m_database.m_itemsList [j].m_itemID == _id)
					{
						// Set inventory in empty slot to be the item of specified ID
						m_inventory[i] = m_database.m_itemsList [j];
					}
				}
				// Break if empty slot is found
				break;
			}
		}
	}

	// Function that removes item from inventory based on specified ID
	void RemoveItem(int _id)
	{
		// Loop through inventory
		for(int i = 0; i < m_inventory.Count; ++i)
		{
			// Checks if inventory ID is same as specified item ID
			if(m_inventory[i].m_itemID == _id)
			{
				m_inventory [i] = new Item ();
				break;
			}
		}
	}

	// Checks if item of specified ID is in inventory
	bool ItemCheck(int _id)
	{
		// Loops through the inventory
		for(int i = 0; i < m_inventory.Count; ++ i)
		{
			// Checks if item ID in curr inv slot is same as specified item ID
			if(m_inventory[i].m_itemID == _id)
			{
				// Return true if item exists in inventory
				return true;
			}
		}
		// Return false if item does not exist in inventory
		return false;
	}

}
