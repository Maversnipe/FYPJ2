using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneMenu : MonoBehaviour
{
    // Make this a Singleton
    private static RuneMenu _instance;
    public static RuneMenu Instance { get { return _instance; } }
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

    // Armour Slot 1
    public Slot m_ArmourSlot_1;
    // Armour Slot 2
    public Slot m_ArmourSlot_2;
    // Sword Slot 1
    public Slot m_SwordSlot_1;
    // Sword Slot 2
    public Slot m_SwordSlot_2;
    // Bow Slot 1
    public Slot m_BowSlot_1;
    // Bow Slot 2
    public Slot m_BowSlot_2;

    // Slot List
    public GameObject[] m_runeSlotList = null;

    // Use this for initialization
    void Start()
    {
        // Initialise array
        m_runeSlotList = new GameObject[6];
        // Fill up the slot list
        m_runeSlotList[0] = m_ArmourSlot_1.gameObject;
        m_runeSlotList[1] = m_ArmourSlot_2.gameObject;
        m_runeSlotList[2] = m_SwordSlot_1.gameObject;
        m_runeSlotList[3] = m_SwordSlot_2.gameObject;
        m_runeSlotList[4] = m_BowSlot_1.gameObject;
        m_runeSlotList[5] = m_BowSlot_2.gameObject;

        SaveInventory.Instance.Load();

        // Set the bonuses for sword
        PlayerManager.Instance.SetBonuses(0);
        // Set the bonuses for bow
        PlayerManager.Instance.SetBonuses(1);
        // Set the bonuses for armour
        PlayerManager.Instance.SetBonuses(2);

        // Set Parent to inactive
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
