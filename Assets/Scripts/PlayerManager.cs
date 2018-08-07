using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    // Player Current Level
    public int m_currentLevel;
    // Player Max Health
    public int m_maxHealth;
    // Player Current Health
    public int m_currentHealth;

    // Player Sword Damage Bonus
    private int m_swordBonus;
    // Player Bow Damage Bonus
    private int m_bowBonus;
    // Player Health Bonus
    private int m_healthBonus;

    // Text that shows Armour Bonus
    private Text m_armourBonusText;
    // Text that shows Sword Bonus
    private Text m_swordBonusText;
    // Text that shows Bow Bonus
    private Text m_bowBonusText;

    // Make this a Singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
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

    // Use this for initialization
    void Start () {
        m_currentHealth = m_maxHealth;
        // Set Sword Damage Bonus to 0
        m_swordBonus = 0;
        // Set Bow Damage Bonus to 0
        m_bowBonus = 0;
        // Set Health Bonus to 0
        m_healthBonus = 0;
        // Set all bonus text to null
        m_bowBonusText = null;
        m_swordBonusText = null;
        m_armourBonusText = null;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(m_currentHealth < 0)
        {
            //died
        }
        string hi = "Health Bonus: " + m_healthBonus.ToString();
        Debug.Log(hi);
	}

    public void AddHP(int value)
    {
        m_currentHealth += value;
    }

    public void MinusHP(int value)
    {
        m_currentHealth -= value;
    }

    // Set Player Bonuses based on Player's Runes
    // 0 is sword, 1 is bow, 2 is armour
    public void SetBonuses(int _runeType)
    {
        switch (_runeType)
        {
            case 0:
                {   // Set for sword
                    // Var to keep track of bonus
                    int swordBonus = 0;
                    // Check first Rune slot if it is empty
                    if (!RuneMenu.Instance.m_SwordSlot_1.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune swordRune = (Rune)RuneMenu.Instance.m_SwordSlot_1.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        swordBonus += swordRune.m_runeAbility;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_SwordSlot_2.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune swordRune = (Rune)RuneMenu.Instance.m_SwordSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        swordBonus += swordRune.m_runeAbility;
                    }
                    // Set sword bonus
                    m_swordBonus = swordBonus;
                    string newBonusText = "Sword Damage Bonus: +" + m_swordBonus.ToString();
                    if(m_swordBonusText == null)
                    {
                        // Set Sword Bonus Text
                        m_swordBonusText = GameObject.FindGameObjectWithTag("SwordBonusText").GetComponent<Text>();
                    }
                    m_swordBonusText.text = newBonusText;
                    Debug.Log("Sword WHAT");
                    break;
                }

            case 1:
                {   // Set for Bow
                    // Var to keep track of bonus
                    int bowBonus = 0;
                    // Check first Rune slot if it is empty
                    if (!RuneMenu.Instance.m_BowSlot_1.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune bowRune = (Rune)RuneMenu.Instance.m_BowSlot_1.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        bowBonus += bowRune.m_runeAbility;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_BowSlot_2.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune bowRune = (Rune)RuneMenu.Instance.m_BowSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        bowBonus += bowRune.m_runeAbility;
                    }
                    // Set bow bonus
                    m_bowBonus = bowBonus;
                    string newBonusText = "Bow Damage Bonus: +" + m_bowBonus.ToString();
                    if(m_bowBonusText == null)
                    {
                        // Set Bow Bonus Text
                        m_bowBonusText = GameObject.FindGameObjectWithTag("BowBonusText").GetComponent<Text>();
                    }
                    m_bowBonusText.text = newBonusText;
                    break;
                }

            case 2:
                {   // Set for Armour
                    // Var to keep track of bonus
                    int armourBonus = 0;

                    //Debug.Log("I AM kinda okay");
                    // Check first Rune slot if it is empty
                    if (!RuneMenu.Instance.m_ArmourSlot_1.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune armourRune = (Rune)RuneMenu.Instance.m_ArmourSlot_1.m_item.Peek();
                        Debug.Log(armourRune);
                        // Add sword bonus to the sword bonus var
                        armourBonus += armourRune.m_runeAbility;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_ArmourSlot_2.IsEmpty())
                    {
                        Debug.Log("I AM getting somewhere okay 2");
                        // Cast item as a sword rune
                        Rune armourRune = (Rune)RuneMenu.Instance.m_ArmourSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        armourBonus += armourRune.m_runeAbility;
                        Debug.Log(armourRune.m_runeAbility);
                    }
                    // Set health bonus
                    m_healthBonus = armourBonus;
                    string newBonusText = "Health Bonus: +" + m_healthBonus.ToString();
                    // Check if the text is null
                    if (m_armourBonusText == null)
                    {
                        // Set Armour Bonus Text
                        m_armourBonusText = GameObject.FindGameObjectWithTag("ArmourBonusText").GetComponent<Text>();
                    }
                    m_armourBonusText.text = newBonusText;
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
}
