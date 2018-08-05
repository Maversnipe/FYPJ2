using UnityEngine;
using UnityEngine.UI;

public class Rune : Item
 {
    // Rune ability
    public int m_runeAbility;
    // Multiplier for randomiser
    private const int m_multiplier = 10;

    public Rune()
    {
        // Set rune ability to 0
        m_runeAbility = 0;
        // Randomise Rune Ability
        RandomiseRune();
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
        m_runeAbility = 0;
        // Randomise Rune Ability
        RandomiseRune();
    }

    public void RandomiseRune()
    {
        // Set rune ability
        m_runeAbility = Random.Range(PlayerManager.Instance.m_currentLevel * m_multiplier - 10, PlayerManager.Instance.m_currentLevel * m_multiplier + 10);
        if(m_runeAbility <= 0)
            m_runeAbility = Random.Range(1, PlayerManager.Instance.m_currentLevel * m_multiplier + 10);
        string runeab = "Rune Ability is " + m_runeAbility.ToString();
        Debug.Log(runeab);
        // Check what rune type this is
        if(m_itemType == ItemType.Armour_Rune)
        {
            m_itemDesc = "+" + m_runeAbility.ToString() + " Health";
        }
        else if(m_itemType == ItemType.Weapon_Rune)
        {
            m_itemDesc = "+" + m_runeAbility.ToString() + " Damage";
        }
    }
 }
