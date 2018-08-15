using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Player Current Level
    public int m_currentLevel;
    // Player Max EXP
    public float m_maxExp;
    // Player Current EXP
    public float m_currentExp;
    // Player Max Health
    public int m_maxHealth;
    // Player Current Health
    public int m_currentHealth;
    // Player Max Mana
    public int m_maxMana;
    // Player Current Mana
    public int m_currentMana;
    // Player Money
    public int m_moneyAmount;
    // Player skill points
    public int skillPoints;
    // Player total skill points
    public int totalSkillPoints;
    // Player level points
    public int levelPoints;
    public int arrowType;
    public bool allSkills;
    public int m_dmg;

    public float m_attackSpeed;

    //Health Regen
    public float m_healthRegen;

    //Mana Regen
    public float m_ManaRegen;

    public List<int> levels;

    public List<int> skills;
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

    // Interactable on focus
    public Interactable m_interactable;
    // Player GO
    public GameObject m_player;

    public bool invulnerable;

    public bool pause;

    public bool unlimited;

    public float timer;
    
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
            Start();
        }
    }

    // Use this for initialization
    void Start()
    {
        pause = false;
        allSkills = false;
        arrowType = 0;
        timer = 0;
        invulnerable = false;
        unlimited = false;
           m_maxHealth = 100;
        m_maxMana = 100;
        m_maxExp = 100;
        m_dmg = 10;
        m_attackSpeed = 1;
        m_healthRegen = 0.02f;
        m_ManaRegen = 0.02f;
        if (!PlayerPrefs.HasKey("Money"))
            PlayerPrefs.GetInt("Money", 1000);
        if (!PlayerPrefs.HasKey("HealthPoints"))
            PlayerPrefs.GetInt("HealthPoints",0);
        if (!PlayerPrefs.HasKey("ManaPoints"))
            PlayerPrefs.GetInt("ManaPoints", 0);
        if (!PlayerPrefs.HasKey("HealthRegenPoints"))
            PlayerPrefs.GetInt("HealthRegenPoints", 0);
        if (!PlayerPrefs.HasKey("ManaRegenPoints"))
            PlayerPrefs.GetInt("ManaRegenPoints", 0);
        if (!PlayerPrefs.HasKey("StrengthPoints"))
            PlayerPrefs.GetInt("StrengthPoints", 0);
        if (!PlayerPrefs.HasKey("AttackSpeedPoints"))
            PlayerPrefs.GetInt("AttackSpeedPoints", 0);
        if (!PlayerPrefs.HasKey("Skill1"))
            PlayerPrefs.GetInt("Skill1", 0);
        if (!PlayerPrefs.HasKey("Skill3"))
            PlayerPrefs.GetInt("Skill3", 0);
        if (!PlayerPrefs.HasKey("Skill2"))
            PlayerPrefs.GetInt("Skill2", 0);
        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.GetInt("Level", 1);
        if (!PlayerPrefs.HasKey("EXP"))
            PlayerPrefs.GetFloat("EXP", 0);
        skills.Clear();
        levels.Clear();
        // 1 = bow, 2 = sword;
        skills.Add(PlayerPrefs.GetInt("Skill1"));
        skills.Add(PlayerPrefs.GetInt("Skill2"));
        skills.Add(PlayerPrefs.GetInt("Skill3"));
        levels.Add(PlayerPrefs.GetInt("HealthPoints"));
        levels.Add(PlayerPrefs.GetInt("ManaPoints"));
        levels.Add(PlayerPrefs.GetInt("HealthRegenPoints"));
        levels.Add(PlayerPrefs.GetInt("ManaRegenPoints"));
        levels.Add(PlayerPrefs.GetInt("StrengthPoints"));
        levels.Add(PlayerPrefs.GetInt("AttackSpeedPoints"));
        m_currentLevel = PlayerPrefs.GetInt("Level");
        m_currentLevel = 2;
        totalSkillPoints = m_currentLevel / 3;
        skillPoints = totalSkillPoints;
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i] != 0)
            {
                skillPoints--;
            }
        }
        m_currentExp = PlayerPrefs.GetFloat("EXP");
        m_maxExp *= (1.5f * (m_currentLevel));

        m_maxHealth += (50 * levels[0]);
        m_currentHealth = m_maxHealth;

        m_maxMana += (50 * levels[1]);
        m_currentMana = m_maxMana;

        m_dmg += (2 * levels[4]);
        m_healthRegen += (0.02f * levels[2]);
        m_ManaRegen += (0.02f * levels[3]);
        m_attackSpeed += (0.2f * levels[5]);
        // Set Sword Damage Bonus to 0
        m_swordBonus = 0;
        // Set Bow Damage Bonus to 0
        m_bowBonus = 0;
        // Set Health Bonus to 0
        m_healthBonus = 0;
        levelPoints = m_currentLevel * 6; 
        for(int i=0;i<levels.Count; i++)
        {
            levelPoints -= levels[i];
        }
        // Set all bonus text to null
        m_bowBonusText = null;
        m_swordBonusText = null;
        m_armourBonusText = null;
        m_moneyAmount = PlayerPrefs.GetInt("Money");
        // Set the player
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (m_currentHealth < 0)
        {
            //died
        }
        string hi = "Health Bonus: " + m_healthBonus.ToString();
        Debug.Log(hi);
        if(timer > 5)
        {
            if(m_currentHealth < m_maxHealth)
            {
                m_currentHealth += (int)(m_maxHealth * m_healthRegen);
                if(m_currentHealth > m_maxHealth)
                {
                    m_currentHealth = m_maxHealth;
                }
            }

            if (m_currentMana < m_maxMana)
            {
                m_currentMana += (int)(m_maxMana * m_ManaRegen);
                if (m_currentMana > m_maxMana)
                {
                    m_currentMana = m_maxMana;
                }
            }
            timer = 0;
        }
        if(m_currentExp >= m_maxExp)
        {
            m_currentLevel++;
            levelPoints += 6;
            m_currentExp -= m_maxExp;
            m_maxExp *= 1.5f;
            if (m_currentLevel == 3 || m_currentLevel == 6 || m_currentLevel == 9)
            {
                skillPoints++;
                totalSkillPoints++;
            }
        }
        PlayerPrefs.SetInt("Money", m_moneyAmount);

            PlayerPrefs.SetInt("Money", m_moneyAmount);

            PlayerPrefs.SetInt("HealthPoints", levels[0]);

            PlayerPrefs.SetInt("ManaPoints", levels[1]);

            PlayerPrefs.SetInt("HealthRegenPoints", levels[2]);

            PlayerPrefs.SetInt("ManaRegenPoints", levels[3]);

            PlayerPrefs.SetInt("StrengthPoints", levels[4]);

            PlayerPrefs.SetInt("AttackSpeedPoints", levels[5]);

            PlayerPrefs.SetInt("Skill1", skills[0]);

            PlayerPrefs.SetInt("Skill3", skills[1]);

            PlayerPrefs.SetInt("Skill2", skills[2]);

            PlayerPrefs.SetInt("Level", m_currentLevel);

            PlayerPrefs.SetFloat("EXP", m_currentExp);

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
                        swordBonus += swordRune.m_runePower;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_SwordSlot_2.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune swordRune = (Rune)RuneMenu.Instance.m_SwordSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        swordBonus += swordRune.m_runePower;
                    }
                    // Set sword bonus
                    m_swordBonus = swordBonus;
                    string newBonusText = "Sword Damage Bonus: +" + m_swordBonus.ToString();
                    if (m_swordBonusText == null)
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
                        bowBonus += bowRune.m_runePower;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_BowSlot_2.IsEmpty())
                    {
                        // Cast item as a sword rune
                        Rune bowRune = (Rune)RuneMenu.Instance.m_BowSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        bowBonus += bowRune.m_runePower;
                    }
                    // Set bow bonus
                    m_bowBonus = bowBonus;
                    string newBonusText = "Bow Damage Bonus: +" + m_bowBonus.ToString();
                    if (m_bowBonusText == null)
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
                        armourBonus += armourRune.m_runePower;
                    }

                    // Check second rune slot if it is empty
                    if (!RuneMenu.Instance.m_ArmourSlot_2.IsEmpty())
                    {
                        Debug.Log("I AM getting somewhere okay 2");
                        // Cast item as a sword rune
                        Rune armourRune = (Rune)RuneMenu.Instance.m_ArmourSlot_2.m_item.Peek();
                        // Add sword bonus to the sword bonus var
                        armourBonus += armourRune.m_runePower;
                        Debug.Log(armourRune.m_runePower);
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

    // Level player up
    public void LevelUp()
    {

    }
}
