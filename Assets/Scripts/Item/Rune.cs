using UnityEngine;
using UnityEngine.UI;

public class Rune : Item
 {
    // Rune Power
    public int m_runePower;
    // Multiplier for randomiser
    private const int m_multiplier = 10;

    // Rune Ability Type
    public enum RuneAbilityType
    {
        Damage_Bonus,
        Health_Bonus,
        Mana_Bonus,
    }

    public Rune()
    {
        // Set rune ability to 0
        m_runePower = 0;
    }

    public Rune(string _name, int _id, ItemType _type)
    {
        // Set Item name
        m_itemName = _name;
        // Set Item ID
        m_itemID = _id;
        // Set Item Type
        m_itemType = _type;
        // Set Item Icon
        m_itemIcon = Resources.Load<Sprite>("Item Icon/" + _name);
        // Set rune ability to 0
        m_runePower = 0;
    }

    public Rune(string _name, ItemType _type)
    {
        // Set Item name
        m_itemName = _name;
        // Set Item Type
        m_itemType = _type;
        // Set Item Icon
        m_itemIcon = Resources.Load<Sprite>("Item Icon/" + _name);
        // Set rune ability to 0
        m_runePower = 0;
    }

    public void RandomiseRune()
    {
        // Set rune ability
        m_runePower = Random.Range(PlayerManager.Instance.m_currentLevel * m_multiplier - 10, PlayerManager.Instance.m_currentLevel * m_multiplier + 10);
        if(m_runePower <= 0)
            m_runePower = Random.Range(1, PlayerManager.Instance.m_currentLevel * m_multiplier + 10);
        string runeab = "Rune Ability is " + m_runePower.ToString();
        Debug.Log(runeab);
        // Check what rune type this is
        if(m_itemType == ItemType.Armour_Rune)
        {
            m_itemDesc = "+" + m_runePower.ToString() + " Health";
        }
        else if(m_itemType == ItemType.Weapon_Rune)
        {
            m_itemDesc = "+" + m_runePower.ToString() + " Damage";
        }
    }

    // To set the rune just with the rune power and item type
    public void SetRune(int _power, int _type)
    {
        // Set the power
        m_runePower = _power;
        // Set the type
        m_itemType = (Item.ItemType)_type;
        // Set the rest of the rune info based on item type
        switch(m_itemType)
        {
            case Item.ItemType.Armour_Rune:
                // Set the name
                m_itemName = "Armour Rune";
                // Set item description
                m_itemDesc = "+" + m_runePower.ToString() + " Health";
                break;
            case Item.ItemType.Weapon_Rune:
                // Set the name
                m_itemName = "Weapon Rune";
                // Set item description
                m_itemDesc = "+" + m_runePower.ToString() + " Damage";
                break;
        }
        // Set Item Icon
        m_itemIcon = Resources.Load<Sprite>("Item Icon/" + m_itemName);
    }
 }
