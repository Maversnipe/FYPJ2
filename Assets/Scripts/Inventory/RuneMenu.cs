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
        DontDestroyOnLoad(this);
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

    // Use this for initialization
    void Start()
    {
        // Set each slot from save game

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
