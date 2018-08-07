using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {
    // Shop Slot ID
    public int m_shopSlotID;
    // Item contained in shop slot
    public ShopItem m_shopItem;
    // Item Icon
    public Image m_icon;
    // Item Count Text
    public Text m_countText;

    // Check if pointer is within slot
    private bool m_containsPoint;
    // Check if able to buy item
    private bool m_ableToBuy;

    // Use this for initialization
    void Start ()
    {
        // Initialise Item
        m_shopItem = null;
        // Sets contains point to false
        m_containsPoint = false;
    }
	
	// Update is called once per frame
	void Update () {
        // Check if able to buy item
        if(m_shopItem != null)
            CheckAbleToBuy();

        // Checks if cursor is over slot
        if (m_containsPoint)
        {
            // Can add item info screen here
            if (!ShopMenu.Instance.m_infoBox.gameObject.activeSelf)
            {
                // Set Info box to be active
                ShopMenu.Instance.m_infoBox.gameObject.SetActive(true);
                // Set this slot to be the current slot id in player menu
                ShopMenu.Instance.SetCurrSlotID(this.m_shopSlotID);
                // Have a single shop item var
                // Set the item name
                ShopMenu.Instance.m_infoBox.transform.GetChild(0).GetComponent<Text>().text = m_shopItem.m_itemName;
                // Set the item information
                ShopMenu.Instance.m_infoBox.transform.GetChild(1).GetComponent<Text>().text = m_shopItem.m_itemDesc;
                // Set the item price
                string itemPrice = "$" + m_shopItem.m_itemPrice.ToString();
                ShopMenu.Instance.m_infoBox.transform.GetChild(2).GetComponent<Text>().text = itemPrice;
                // Check if able to buy
                if(!m_ableToBuy)
                {   // If not able to buy, print warning
                    ShopMenu.Instance.m_infoBox.transform.GetChild(3).gameObject.SetActive(true);
                    ShopMenu.Instance.m_infoBox.transform.GetChild(3).GetComponent<Text>().text = "You do not have enough money to buy this";
                }
                else
                {
                    ShopMenu.Instance.m_infoBox.transform.GetChild(3).gameObject.SetActive(false);
                }
                // Change the position of the info box
                ShopMenu.Instance.m_infoBox.rectTransform.position = new Vector3(gameObject.transform.position.x + ShopMenu.Instance.m_infoBox.rectTransform.sizeDelta.x * 0.75f,
                                                                                   gameObject.transform.position.y, 0f);
            }

            // Check if mouse button is pressed and if item can be bought
            if (m_ableToBuy && Input.GetMouseButtonDown(0))
            { // Item is bought      
                // Add item to inventory
                Inventory.Instance.Add(m_shopItem);
                // Minus player money
                PlayerManager.Instance.m_moneyAmount -= m_shopItem.m_itemPrice;
                // Update UI amount
                ShopMenu.Instance.ChangeMoneyAmount();
                // Check if the item can be bought with new amount
                CheckAbleToBuy();
                // Set warning to true if not able to buy
                if (!m_ableToBuy)
                {   // If not able to buy, print warning
                    ShopMenu.Instance.m_infoBox.transform.GetChild(3).gameObject.SetActive(true);
                    ShopMenu.Instance.m_infoBox.transform.GetChild(3).GetComponent<Text>().text = "You do not have enough money to buy this";
                }

            }
        }
        else if(ShopMenu.Instance.GetCurrentSlotID() == this.m_shopSlotID)
        {   // If not in slot anymore
            // Remove info box
            // Set Info box to be not active
            ShopMenu.Instance.m_infoBox.gameObject.SetActive(false);
            // Turn off warning
            ShopMenu.Instance.m_infoBox.transform.GetChild(3).gameObject.SetActive(false);
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

    // Checks if the item can be bought with player's money
    public void CheckAbleToBuy()
    {
        // Check if player has enough money
        if(PlayerManager.Instance.m_moneyAmount < m_shopItem.m_itemPrice)
        {
            // Not enough, set false
            m_ableToBuy = false;
        }
        else
        {
            // Enough, set true
            m_ableToBuy = true;
        }
        Debug.Log(PlayerManager.Instance.m_moneyAmount);
    }

    // Add item to slot
    public void AddItem(Item _item)
    {
        // Set item to new item
        m_shopItem = (ShopItem)_item;
        // Set item icon
        m_icon.sprite = m_shopItem.m_itemIcon;
        m_icon.gameObject.SetActive(true);
    }
}
