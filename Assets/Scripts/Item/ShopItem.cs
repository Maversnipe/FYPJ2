using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Item {
    // Item's price
    public int m_itemPrice;

    // Constructor
    public ShopItem()
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
        // Set Item's price
        m_itemPrice = 0;
    }

    // Overloaded Constructor
    public ShopItem(string _name, int _id, string _desc, ItemType _type, bool _stackable, int _price)
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
        // Set item price
        m_itemPrice = _price;
    }
}
