[System.Serializable]
public class ItemSave{
    // Item name
    public string m_itemName;
    // Item Type
    public int m_itemType;
    // Item count
    public int m_count;
    // Rune Power
    public int m_runePower;

    // Overloaded Constructor
    public ItemSave(int _itemType, int _count, string _name)
    {
        // Set Item type
        m_itemType = _itemType;
        // Set item count
        m_count = _count;
        // Set rune power
        m_runePower = -1;
        // Set name
        m_itemName = _name;
    }

    // Overloaded Constructor
    public ItemSave(int _power, int _itemType, int _count, string _name)
    {
        // Set rune power
        m_runePower = _power;
        // Set item type
        m_itemType = _itemType;
        // Set item count
        m_count = _count;
        // Set name
        m_itemName = _name;
    }

    // Getter for item type
    public int GetItemType()
    {
        return m_itemType;
    }

    // Setter for rune power
    void SetItemType(int _itemType)
    {
        m_itemType = _itemType;
    }

    // Getter for count
    public int GetCount()
    {
        return m_count;
    }

    // Setter for count
    void SetCount(int _count)
    {
        m_count = _count;
    }

    // Getter for rune power
    public int GetRunePower()
    {
        return m_runePower;
    }

    // Setter for rune power
    void SetRunePower(int _power)
    {
        m_runePower = _power;
    }

    // Getter for name
    public string GetName()
    {
        return m_itemName;
    }

    // Setter for name
    void SetName(string _name)
    {
        m_itemName = _name;
    }
}
