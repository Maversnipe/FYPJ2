using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInventory : MonoBehaviour {
    // Make this a Singleton
    private static SaveInventory _instance;
    public static SaveInventory Instance { get { return _instance; } }

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

    private ItemDatabase m_database;
	// Use this for initialization
	void Start ()
    {
        // Initialize m_database
        m_database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Save the player's inventory info
    public void Save()
    {
        // For Inventory Slots
        // Iterate through array of Slots
        for(int i = 0; i < Inventory.Instance.m_slotList.Length; ++i)
        {
            // Check if there are items
            if(Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_item.Count == 0)
            {   // If no items
                // Name of save
                string saveName = "InventorySlot" + i.ToString();
                // Set in PlayerPrefs to nothing
                PlayerPrefs.SetString(saveName, "");
                continue;
            }
            // Get the item
            Item theItem = Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_item.Peek();
            // Set item count
            int itemCount = Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_item.Count;
            switch(theItem.m_itemType)
            {
                case Item.ItemType.Weapon_Rune:
                case Item.ItemType.Armour_Rune:
                    // Cast item as rune
                    Rune theRune = (Rune)theItem;
                    // Create a save
                    ItemSave _runeSave = new ItemSave(theRune.m_runePower, (int)theRune.m_itemType, itemCount, theRune.m_itemName);                   
                    // Name of save
                    string runeSaveName = "InventorySlot" + i.ToString();
                    // Convert save to json
                    string jsonRuneSave = JsonUtility.ToJson(_runeSave);
                    // Set in PlayerPrefs
                    PlayerPrefs.SetString(runeSaveName, jsonRuneSave);
                    Debug.Log(jsonRuneSave);
                    break;
                case Item.ItemType.None:
                    break;
                default:
                    // Create a save
                    ItemSave _itemSave = new ItemSave((int)theItem.m_itemType, itemCount, theItem.m_itemName);
                    // Name of save
                    string saveName = "InventorySlot" + i.ToString();
                    // Convert save to json
                    string jsonSave = JsonUtility.ToJson(_itemSave);
                    // Set in PlayerPrefs
                    PlayerPrefs.SetString(saveName, jsonSave);
                    break;
            }
        }

        // For Rune Slots
    }

    // Load the player's inventory info
    public void Load()
    {
        // For Inventory Slots
        // Iterate through the whole inventory
        for(int i = 0; i < Inventory.Instance.m_slotList.Length; ++i)
        {
            // Save name
            string saveName = "InventorySlot" + i.ToString();
            // The saved item
            string theSave = PlayerPrefs.GetString(saveName);
            // Check if there are any saves under the save name
            if (theSave != null && theSave.Length > 0)
            {
                // Get item from json string
                ItemSave theItem = JsonUtility.FromJson<ItemSave>(theSave);
                // Check if item is null
                if(theItem != null)
                {
                    // Iterate to give the amount of objects
                    for (int j = 0; j < theItem.GetCount(); ++j)
                    {
                        // Check if it is rune type or not
                        if (theItem.GetRunePower() == -1)
                        {
                            // Cast as ShopItem type
                            ShopItem _item = (ShopItem)m_database.GetItem(theItem.GetName());
                            // Push into the slot item stack
                            Inventory.Instance.m_slotList[i].GetComponent<Slot>().
                                m_item.Push(_item);
                        }
                        else
                        {
                            // Cast as Rune type
                            Rune _rune = new Rune();
                            _rune.SetRune(theItem.GetRunePower(), theItem.GetItemType());
                            // Push into the slot item stack
                            Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_item.Push(_rune);
                        }
                    }

                    // Spawn item
                    Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_icon.gameObject.SetActive(true);
                    Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_icon.sprite = 
                        Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_item.Peek().m_itemIcon;
                    // Check if more than 1 of the item
                    if(theItem.GetCount() > 1)
                    {
                        // Spawn item count 
                        Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_countText.gameObject.SetActive(true);
                        Inventory.Instance.m_slotList[i].GetComponent<Slot>().m_countText.text = theItem.GetCount().ToString();
                    }
                    // Set slot to not empty
                    Inventory.Instance.m_slotList[i].GetComponent<Slot>().SetIsEmpty(false);
                }
                else
                {   // If item is null
                    Debug.Log("The Load has a problem");
                }
            }
        }
    }
}
