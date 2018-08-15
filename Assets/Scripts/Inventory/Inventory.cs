using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    // Make this a Singleton
    private static Inventory _instance;
    public static Inventory Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Num Of Slots
    public int m_numSlots;
    
    // Array that contains all inv slots
    public GameObject[] m_slotList = null;
    // Number of slots used
    private int m_numSlotsUsed;

    void Start()
    {
        // Initialise array
        m_slotList = new GameObject[m_numSlots];
        // int that keeps track of number of non-slot children
        int numOfNonSlot = 0;
        // Fill up the slot list
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            // Checks if the child is a slot type
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<Slot>() != null)
                m_slotList[i] = gameObject.transform.GetChild(i).gameObject;
            else
                numOfNonSlot++;
        }
    }

    void Update()
    {
       
        
    }

    // Add Item to inventory
    public bool Add (Item _item)
    {
        // Check if the inventory is full
        if (m_numSlotsUsed >= m_numSlots)
        {
            return false;
        }

        // Checks if item is stackable
        if (_item.m_stackable)
        {
            // Keep track of the first empty slot that comes up
            int firstEmptySlot = -1;
            // To check if the item has been placed in a stack
            bool addedToStack = false;

            // Iterate through array of slots
            for (int i = 0; i < m_numSlots; ++i)
            {
                // Checks if slot is empty
                if (firstEmptySlot == -1 && m_slotList[i].GetComponent<Slot>().IsEmpty())
                {
                    firstEmptySlot = i;
                }
                else if(!m_slotList[i].GetComponent<Slot>().IsEmpty())
                {
                    Debug.Log(m_slotList);
                    if (m_slotList[i].GetComponent<Slot>().m_item.Peek().m_itemName.Equals(_item.m_itemName))
                    {
                        addedToStack = true;
                        // Add to stack
                        m_slotList[i].GetComponent<Slot>().AddItem(_item, 1);
                        break;
                    }
                }
            }

            // Check if item has already been added to stack
            if (!addedToStack)
            {
                m_slotList[firstEmptySlot].GetComponent<Slot>().AddItem(_item, 1);
                m_numSlotsUsed++;
            }
        }
        else
        {
            // Iterate through array of slots
            for (int i = 0; i < m_numSlots; ++i)
            {
                // Checks if slot is empty
                if (m_slotList[i].GetComponent<Slot>().IsEmpty())
                {
                    m_slotList[i].GetComponent<Slot>().AddItem(_item, 1);
                    m_numSlotsUsed++;
                    break;
                }
            }
        }

        // Save inventory
        SaveInventory.Instance.Save();

        // Item is added
        return true;
    }

    public void Remove(string _itemName, int _numToRemove)
    {
        // Iterate through the whole inventory
        for (int i = 0; i < m_numSlots; ++i)
        {
            // Check if slot is empty
            if (!m_slotList[i].GetComponent<Slot>().IsEmpty())
            {   // If slot not empty
                // Checks if both have same name
                Debug.Log(m_slotList);
                if (m_slotList[i].GetComponent<Slot>().m_item.Peek().m_itemName.Equals(_itemName))
                {   // If name matches
                    // Loop through the number of times the item is to be removed
                    for (int j = 0; j < _numToRemove; ++j)
                    {
                        // Remove item
                        m_slotList[i].GetComponent<Slot>().m_item.Pop();

                        if (m_slotList[i].GetComponent<Slot>().m_item.Count == 0)
                        {
                            m_slotList[i].GetComponent<Slot>().SetIsEmpty(true);

                            // Save inventory
                            SaveInventory.Instance.Save();
                            break;
                        }
                    }
                }
            }
        }
    }

    // Search for items in inventory
    public Stack<Item> ItemSearch(string _itemName)
    {
        // Iterate through the whole inventory
        for(int i = 0; i < m_numSlots; ++i)
        {
            // Check if slot is empty
            if (!m_slotList[i].GetComponent<Slot>().IsEmpty())
            {   // If slot not empty
                // Checks if both have same name
                Debug.Log(m_slotList[i]);
                Debug.Log(m_slotList[i].GetComponent<Slot>().m_item.Count);
                if (m_slotList[i].GetComponent<Slot>().m_item.Peek().m_itemName.Equals(_itemName))
                {   // If name matches
                    return m_slotList[i].GetComponent<Slot>().m_item;
                }
            }
        }
        Debug.Log("Hellllllloooooooooo");
        // If doesn't exist
        return null;
    }
}
