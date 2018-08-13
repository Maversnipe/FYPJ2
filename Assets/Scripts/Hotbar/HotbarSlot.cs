using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour {
    // Hotbar Slot ID
    public int m_hotbarSlotID;
    // Item contained in shop slot
    public Item m_hotbarItem;
    // Item Icon
    public Image m_icon;
    // Item Count Text
    public Text m_countText;

    // Check if pointer is within slot
    private bool m_containsPoint;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {
        // Sets contains point to false
        m_containsPoint = false;
    }
	
	// Update is called once per frame
	void Update () {
        // Checks if cursor is over slot and if player has clicked
        if (m_containsPoint && Input.GetMouseButton(0))
        {
            // Create a temp to store previous slot ID
            int prevID = HotbarParent.Instance.GetCurrentSlotID();
            // Set the previous selected to false
            Hotbar.Instance.transform.GetChild(prevID).GetChild(0).gameObject.SetActive(false);
            // Set current slot as selected
            HotbarParent.Instance.SetCurrSlotID(m_hotbarSlotID);
            transform.GetChild(0).gameObject.SetActive(true);
        }

        // Checks if it exists in inventory
        Stack<Item> itemStack = null;
        itemStack = Inventory.Instance.ItemSearch(m_hotbarItem.m_itemName);
        Debug.Log(Inventory.Instance);
        if (itemStack != null)
        {
            m_countText.text = itemStack.Count.ToString();
        }
        else
        {
            // Set count to 0 if item not in inventory
            m_countText.text = "0";
        }
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

    // Add item to slot
    public void AddItem(Item _item)
    {
        // Set item to new item
        m_hotbarItem = _item;
        // Set item icon
        m_icon.sprite = m_hotbarItem.m_itemIcon;
        m_icon.gameObject.SetActive(true);
    }
}
