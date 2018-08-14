using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    // Slot Type
    public SlotType m_slotType;
    // Slot ID
    public int m_slotID;
    // Item contained in Slot
    public Stack<Item> m_item;
    // Item Icon
    public Image m_icon;
    // Item Count Text
    public Text m_countText;
    
    // Bool to check if slot is empty
    private bool m_isEmpty;
    // Check if pointer is within slot
    private bool m_containsPoint;

    // Enum of slot types
    public enum SlotType
    {
        All,
        Weapon_Rune,
        Armour_Rune,
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // Set the slot to empty
        m_isEmpty = true;
        // Initialise Item Array
        m_item = new Stack<Item>();
        // Sets contains point to false
        m_containsPoint = false;
    }

    void Update()
    {
        // Check if mouse cursor is in slot and if slot is empty
        if (!m_isEmpty && m_containsPoint)
        {
            // Can add item info screen here
            if (!PlayerMenu.Instance.m_infoBox.gameObject.activeSelf)
            {
                // Set Info box to be active
                PlayerMenu.Instance.m_infoBox.gameObject.SetActive(true);
                // Set this slot to be the current slot id in player menu
                PlayerMenu.Instance.SetCurrSlotID(this.m_slotID);
                // Set the item name
                PlayerMenu.Instance.m_infoBox.transform.GetChild(0).GetComponent<Text>().text = m_item.Peek().m_itemName;
                // Set the item information
                PlayerMenu.Instance.m_infoBox.transform.GetChild(1).GetComponent<Text>().text = m_item.Peek().m_itemDesc;
                // Change the position of the info box
                if (this.m_slotType == SlotType.All)
                {
                    // Set position based on slot position
                    PlayerMenu.Instance.m_infoBox.rectTransform.position = new Vector3(gameObject.transform.position.x - PlayerMenu.Instance.m_infoBox.rectTransform.sizeDelta.x * 0.75f,
                                                                                        gameObject.transform.position.y, 0f);
                }
                else
                {
                    // Set position based on slot position
                    PlayerMenu.Instance.m_infoBox.rectTransform.position = new Vector3(gameObject.transform.position.x + PlayerMenu.Instance.m_infoBox.rectTransform.sizeDelta.x * 0.75f,
                                                                                        gameObject.transform.position.y, 0f);
                }
            }

            // Check if mouse button is pressed and if player has no target item
            if (!PlayerMenu.Instance.HasTarget() && Input.GetMouseButtonDown(0))
            { // Select item if no target item in hand
                // Make the target item be this slot's item
                for(int i = 0; i < m_item.Count; ++i)
                {
                    PlayerMenu.Instance.m_targetItem.Push(m_item.Peek());
                }
                // Add image of the item to the cursor
                PlayerMenu.Instance.m_targetImage.sprite = m_item.Peek().m_itemIcon;
                // Set the image to be true
                PlayerMenu.Instance.m_targetImage.gameObject.SetActive(true);
                // Set the image position to mouse position
                PlayerMenu.Instance.m_targetImage.rectTransform.position = Input.mousePosition;
                // Set has target to true
                PlayerMenu.Instance.SetHasTarget(true);
                // Remove item from slot
                RemoveItem(m_item.Count);

                // Check if the slot is a rune slot
                if (m_slotType == SlotType.Armour_Rune ||
                    m_slotType == SlotType.Weapon_Rune)
                {
                    // Set bonuses for rune
                    if (this.m_slotID == RuneMenu.Instance.m_SwordSlot_1.m_slotID
                        || this.m_slotID == RuneMenu.Instance.m_SwordSlot_2.m_slotID)
                    {   // Sword Rune
                        PlayerManager.Instance.SetBonuses(0);
                    }
                    else if (this.m_slotID == RuneMenu.Instance.m_BowSlot_1.m_slotID
                        || this.m_slotID == RuneMenu.Instance.m_BowSlot_2.m_slotID)
                    {   // Bow Rune
                        PlayerManager.Instance.SetBonuses(1);
                    }
                    else if (this.m_slotID == RuneMenu.Instance.m_ArmourSlot_1.m_slotID
                        || this.m_slotID == RuneMenu.Instance.m_ArmourSlot_2.m_slotID)
                    {   // Armour Rune
                        PlayerManager.Instance.SetBonuses(2);
                    }
                }
            }       
        }
        else if (m_isEmpty && m_containsPoint)
        {
            // Check if mouse button is pressed and if player has target item
            if (PlayerMenu.Instance.HasTarget() && Input.GetMouseButtonDown(0))
            {
                bool canPlace = true;

                // Check if the correct item is put in the correct slot
                // Example: Weapon Rune in Weapon Rune Slot
                if ((m_slotType == SlotType.Armour_Rune &&
                    PlayerMenu.Instance.m_targetItem.Peek().m_itemType != Item.ItemType.Armour_Rune) ||
                    (m_slotType == SlotType.Weapon_Rune &&
                    PlayerMenu.Instance.m_targetItem.Peek().m_itemType != Item.ItemType.Weapon_Rune))
                    canPlace = false;

                if (canPlace)
                {
                    // Add selected item into the slot
                    Debug.Log(PlayerMenu.Instance.m_targetItem.Count);
                    AddItem(PlayerMenu.Instance.m_targetItem.Peek(), PlayerMenu.Instance.m_targetItem.Count);
                    // Remove item from PlayerMenu target
                    while (PlayerMenu.Instance.m_targetItem.Count != 0)
                    {
                        PlayerMenu.Instance.m_targetItem.Pop();
                    }
                    // Remove image of target
                    PlayerMenu.Instance.m_targetImage.sprite = null;
                    // Set the image to be inactive
                    PlayerMenu.Instance.m_targetImage.gameObject.SetActive(false);
                    // Set has target to false
                    PlayerMenu.Instance.SetHasTarget(false);

                    // Check if the slot is a rune slot
                    if(m_slotType == SlotType.Armour_Rune ||
                        m_slotType == SlotType.Weapon_Rune)
                    {
                        // Set bonuses for rune
                        if(this.m_slotID == RuneMenu.Instance.m_SwordSlot_1.m_slotID 
                            || this.m_slotID == RuneMenu.Instance.m_SwordSlot_2.m_slotID)
                        {   // Sword Rune
                            PlayerManager.Instance.SetBonuses(0);
                        }
                        else if (this.m_slotID == RuneMenu.Instance.m_BowSlot_1.m_slotID
                            || this.m_slotID == RuneMenu.Instance.m_BowSlot_2.m_slotID)
                        {   // Bow Rune
                            PlayerManager.Instance.SetBonuses(1);
                        }
                        else if (this.m_slotID == RuneMenu.Instance.m_ArmourSlot_1.m_slotID
                            || this.m_slotID == RuneMenu.Instance.m_ArmourSlot_2.m_slotID)
                        {   // Armour Rune
                            PlayerManager.Instance.SetBonuses(2);
                        }
                    }
                }
                else
                {
                    // Put up a warning that cannot place item in this slot
                }
            }
            else if(PlayerMenu.Instance.HasTarget() && PlayerMenu.Instance.GetCurrentSlotID() == this.m_slotID)
            {
                // Set Info box to be not active
                PlayerMenu.Instance.m_infoBox.gameObject.SetActive(false);
            }
        }
        
        // If the mouse is not over the slot
        if(!m_containsPoint && PlayerMenu.Instance.GetCurrentSlotID() == this.m_slotID)
        {
            // Remove info box
            // Set Info box to be not active
            PlayerMenu.Instance.m_infoBox.gameObject.SetActive(false);
        }
    }   

    // Add item to slot
    public void AddItem(Item _item, int _numToAdd)
    {
        //  Check if item is null
        if (_item == null)
        {
            Debug.Log("Item is null!");
            return;
        }

        // Check if num to add is more than 0
        if (_numToAdd <= 0)
        {
            Debug.Log("Num to add is less than 1");
            return;
        }

        for (int i = 0; i < _numToAdd; ++i)
        {
            m_item.Push(_item);
        }
        m_isEmpty = false;
        
        // Set item icon
        m_icon.sprite = m_item.Peek().m_itemIcon;
        m_icon.gameObject.SetActive(true);
        // Set item count
        if (m_item.Count > 1)
        {
            m_countText.text = m_item.Count.ToString();
            m_countText.gameObject.SetActive(true);
        }

        // Save inventory
        SaveInventory.Instance.Save();
    }

    // Remove 1 item from the slot
    public void RemoveItem(int _numToRemove)
    {
        if(_numToRemove > m_item.Count)
        {
            Debug.Log("You are removing more items than there are in the slot!");
            return;
        }
        
        // Remove items based on number to remove
        for (int i = 0; i < _numToRemove; ++i)
        {
            m_item.Pop();
        }

        // Check if all items are removed, or if 1 item is left
        if (m_item.Count == 0)
        {
            ClearSlot();
        }
        else if(m_item.Count == 1)
        {
            m_countText.gameObject.SetActive(false);
        }

        // Save inventory
        SaveInventory.Instance.Save();
    }

    // Clear slot
    public void ClearSlot()
    {
        // Set sprite to null
        m_icon.sprite = null;
        // Set icon to inactive
        m_icon.gameObject.SetActive(false);
        // Set item count text to be inactive
        m_countText.gameObject.SetActive(false);
        // Set slot to be empty
        m_isEmpty = true;
    }

    // Getter for whether slot is empty
    public bool IsEmpty()
    {
        return m_isEmpty;
    }

    // Getter for whether slot is empty
    public void SetIsEmpty(bool _empty)
    {
        m_isEmpty = _empty;
    }

    // Getter for contains point
    public bool ContainsPoint()
    {
        return m_containsPoint;
    }

    // Setter for contains point
    public void SetContainsPoint(bool _contains)
    {
        m_containsPoint = _contains;
    }
}
