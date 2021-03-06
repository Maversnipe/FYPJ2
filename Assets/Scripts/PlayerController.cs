﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Inventory inventory;
    public float moveSpeed;
    private bool isMoving;
    private Vector2 dir;
    private Animator anim;
    private Rigidbody2D body;
    public float dashSpeedUp;
    public float dashSpeedDown;
    public float dashSpeedLeft;
    public float dashSpeedRight;
    public float dashTime;
    public float dashTimeSet;
    public float dashSpeed;
    public float pressedTime;
    public bool dash;
    public bool isAttacking;
    public bool isMelee;
    public bool bowSkill1;
    public bool bowSkill2;
    public bool bowSkill3;
    public bool swordSkill1;
    public bool swordSkill2;
    public bool swordSkill3;
    public int mana;
    public float attackTime;
    private float attackTimer;
	public int count = 1000;
    public Vector2 mouseClick;
    public GameObject damageCounter;
    public bool skillTree;
    public bool levelTree;
    bool menuOpen;
    public class DoubleTap
    {
        public KeyCode key;
        public float time;
    }

    public List<DoubleTap> doubleTap = new List<DoubleTap>();
	// Use this for initialization
	void Start () {
        skillTree = false;
        levelTree = false;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        isMelee = false;
        dashSpeedUp = 1;
        dashSpeedDown = 1;
        dashSpeedLeft = 1;
        dashSpeedRight = 1;
        dashTime = 0f;
        count = 1000;
        dash = false;
        bowSkill1 = false;
        bowSkill2 = false;
        bowSkill3 = false;
        swordSkill1 = false;
        swordSkill2 = false;
        swordSkill3 = false;
        menuOpen = false;
        GameObject.FindGameObjectWithTag("Skill").transform.GetChild(0).gameObject.SetActive(skillTree);
        GameObject.FindGameObjectWithTag("Level").transform.GetChild(0).gameObject.SetActive(levelTree);
        GameObject.FindGameObjectWithTag("Skill").transform.GetChild(1).gameObject.SetActive(skillTree);
        GameObject.FindGameObjectWithTag("Level").transform.GetChild(1).gameObject.SetActive(levelTree);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PlayerManager.Instance.pause)
            {
                PlayerManager.Instance.pause = false;
            }
            else
            {
                PlayerManager.Instance.pause = true;
            }
        }
        if (PlayerManager.Instance.pause)
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            //GameObject.Find("Player UI").transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("Player UI").transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (Hotbar.Instance.GetInstanceID() == 0)
                {
                    PlayerManager.Instance.arrowType = 0;
                }
                if (Hotbar.Instance.GetInstanceID() == 1)
                {
                    PlayerManager.Instance.arrowType = 1;
                }
                if (Hotbar.Instance.GetInstanceID() == 2)
                {
                    PlayerManager.Instance.arrowType = 2;
                }
            }
            bowSkill1 = false;
            for (int i = 0; i < doubleTap.Count; i++)
            {
                doubleTap[i].time -= Time.deltaTime;
                if (doubleTap[i].time <= 0)
                {
                    doubleTap.Remove(doubleTap[i]);
                    i--;
                }
            }
            if (dash)
            {
                if (dashTime <= 0)
                {
                    dashSpeedUp = 1;
                    dashSpeedDown = 1;
                    dashSpeedLeft = 1;
                    dashSpeedRight = 1;
                    dash = false;
                }
            }
            dashTime -= 1;
            // Check if player presses Inventory button
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // Set player menu active to the opposite of itself
                PlayerMenu.Instance.SetMenuActive(!PlayerMenu.Instance.MenuIsActive());
                // Set the menu object to active
                GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.SetActive(PlayerMenu.Instance.MenuIsActive());
                if (PlayerMenu.Instance.MenuIsActive())
                {
                    skillTree = false;
                    levelTree = false;
                    GameObject.FindGameObjectWithTag("Skill").transform.GetChild(0).gameObject.SetActive(skillTree);
                    GameObject.FindGameObjectWithTag("Level").transform.GetChild(0).gameObject.SetActive(levelTree);
                    GameObject.FindGameObjectWithTag("Skill").transform.GetChild(1).gameObject.SetActive(skillTree);
                    GameObject.FindGameObjectWithTag("Level").transform.GetChild(1).gameObject.SetActive(levelTree);
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (skillTree)
                {
                    skillTree = false;
                    menuOpen = false;
                }
                else
                {
                    skillTree = true;
                    levelTree = false;
                    PlayerMenu.Instance.SetMenuActive(false);
                    GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.SetActive(PlayerMenu.Instance.MenuIsActive());
                    GameObject.FindGameObjectWithTag("Level").transform.GetChild(0).gameObject.SetActive(levelTree);
                    GameObject.FindGameObjectWithTag("Level").transform.GetChild(1).gameObject.SetActive(levelTree);
                    menuOpen = true;
                }
                GameObject.FindGameObjectWithTag("Skill").transform.GetChild(0).gameObject.SetActive(skillTree);
                GameObject.FindGameObjectWithTag("Skill").transform.GetChild(1).gameObject.SetActive(skillTree);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (levelTree)
                {
                    levelTree = false;
                    menuOpen = false;
                }
                else
                {
                    levelTree = true;
                    skillTree = false;
                    PlayerMenu.Instance.SetMenuActive(false);
                    GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.SetActive(PlayerMenu.Instance.MenuIsActive());
                    GameObject.FindGameObjectWithTag("Skill").transform.GetChild(0).gameObject.SetActive(skillTree);
                    GameObject.FindGameObjectWithTag("Skill").transform.GetChild(1).gameObject.SetActive(skillTree);
                    menuOpen = true;
                }
                GameObject.FindGameObjectWithTag("Level").transform.GetChild(0).gameObject.SetActive(levelTree);
                GameObject.FindGameObjectWithTag("Level").transform.GetChild(1).gameObject.SetActive(levelTree);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isMelee && (PlayerManager.Instance.levels[0] == 0 || PlayerManager.Instance.allSkills))
                {
                    if (PlayerManager.Instance.m_currentMana >= 10)
                    {
                        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
                        {
                            float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position, transform.position);
                            if (dist < 5)
                            {
                                if (!GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<AI>().knockback)
                                {
                                    GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<AI>().knockback = true;
                                    GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<AI>().knockbackTime = 15;
                                    GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<AI>().knockbackDist = 10;
                                }

                            }
                        }
                        var clone = (GameObject)Instantiate(damageCounter, transform.position, transform.rotation);
                        clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "KNOCKBACK";

                        PlayerManager.Instance.m_currentMana -= 10;


                    }

                }
                else if (!isMelee && (PlayerManager.Instance.levels[0] == 1 || PlayerManager.Instance.allSkills))
                {
                    if (PlayerManager.Instance.m_currentMana >= 10)
                    {
                        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
                        {
                            float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position, transform.position);
                            if (dist < 10)
                            {
                                bowSkill1 = true;

                            }
                        }
                        if (!PlayerManager.Instance.unlimited)
                        {
                            if (PlayerManager.Instance.arrowType == 0)
                            {
                                Inventory.Instance.Remove("Arrow", 1);
                            }
                            if (PlayerManager.Instance.arrowType == 1)
                            {
                                Inventory.Instance.Remove("Slow Arrow", 1);
                            }
                            if (PlayerManager.Instance.arrowType == 2)
                            {
                                Inventory.Instance.Remove("Homing Arrow", 1);
                            }
                        }

                        PlayerManager.Instance.m_currentMana -= 10;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isMelee && (PlayerManager.Instance.levels[1] == 2 || PlayerManager.Instance.allSkills))
                {
                    bowSkill2 = false;
                    transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
                    if (!swordSkill2)
                    {
                        swordSkill2 = true;
                        transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        swordSkill2 = false;
                    }
                }
                else if (!isMelee && (PlayerManager.Instance.levels[1] == 1 || PlayerManager.Instance.allSkills))
                {
                    swordSkill2 = false;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
                    if (!bowSkill2)
                    {
                        bowSkill2 = true;
                        transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        bowSkill2 = false;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (isMelee && (PlayerManager.Instance.levels[2] == 2 || PlayerManager.Instance.allSkills))
                {
                    bowSkill3 = false;
                    if (!swordSkill3)
                    {
                        swordSkill3 = true;
                    }
                    else
                    {
                        swordSkill3 = false;
                    }
                }
                else if (!isMelee && (PlayerManager.Instance.levels[2] == 1 || PlayerManager.Instance.allSkills))
                {
                    swordSkill3 = false;
                    if (!bowSkill3)
                    {
                        bowSkill3 = true;
                    }
                    else
                    {
                        bowSkill3 = false;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (PlayerManager.Instance.allSkills)
                {
                    PlayerManager.Instance.allSkills = false;
                }
                else
                {
                    PlayerManager.Instance.allSkills = true;
                }
            }
            // For double tap dash
            if (Input.GetKeyDown(KeyCode.W))
            {
                bool check = false;
                for (int i = 0; i < doubleTap.Count; i++)
                {
                    if (doubleTap[i].key == KeyCode.W)
                    {
                        if (doubleTap[i].time > 0)
                        {
                            check = true;
                            if (!dash)
                            {
                                dash = true;
                                dashTime = dashTimeSet;
                                dashSpeedUp = dashTime;
                                PlayerManager.Instance.m_currentMana -= 10;
                            }
                            doubleTap[i].time = 0;
                        }
                        break;
                    }
                }
                if (!check)
                {
                    DoubleTap pressedKey = new DoubleTap();
                    pressedKey.key = KeyCode.W;
                    pressedKey.time = pressedTime;
                    doubleTap.Add(pressedKey);
                }

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                bool check = false;
                for (int i = 0; i < doubleTap.Count; i++)
                {
                    if (doubleTap[i].key == KeyCode.A)
                    {
                        if (doubleTap[i].time > 0)
                        {
                            check = true;
                            if (!dash)
                            {
                                dash = true;
                                dashTime = dashTimeSet;
                                dashSpeedLeft = dashSpeed;
                                PlayerManager.Instance.m_currentMana -= 10;
                            }
                            doubleTap[i].time = 0;
                        }
                        break;
                    }
                }
                if (!check)
                {
                    DoubleTap pressedKey = new DoubleTap();
                    pressedKey.key = KeyCode.A;
                    pressedKey.time = pressedTime;
                    doubleTap.Add(pressedKey);
                }

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                bool check = false;
                for (int i = 0; i < doubleTap.Count; i++)
                {
                    if (doubleTap[i].key == KeyCode.S)
                    {
                        if (doubleTap[i].time > 0)
                        {
                            check = true;
                            if (!dash)
                            {
                                dash = true;
                                dashTime = dashTimeSet;
                                dashSpeedDown = dashSpeed;
                                PlayerManager.Instance.m_currentMana -= 10;
                            }
                            doubleTap[i].time = 0;
                        }
                        break;
                    }
                }
                if (!check)
                {
                    DoubleTap pressedKey = new DoubleTap();
                    pressedKey.key = KeyCode.S;
                    pressedKey.time = pressedTime;
                    doubleTap.Add(pressedKey);
                }

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                bool check = false;
                for (int i = 0; i < doubleTap.Count; i++)
                {
                    if (doubleTap[i].key == KeyCode.D)
                    {
                        if (doubleTap[i].time > 0)
                        {
                            check = true;
                            if (!dash)
                            {
                                dash = true;
                                dashTime = dashTimeSet;
                                dashSpeedRight = dashSpeed;
                                PlayerManager.Instance.m_currentMana -= 10;
                            }
                            doubleTap[i].time = 0;
                        }
                        break;
                    }
                }
                if (!check)
                {
                    DoubleTap pressedKey = new DoubleTap();
                    pressedKey.key = KeyCode.D;
                    pressedKey.time = pressedTime;
                    doubleTap.Add(pressedKey);
                }

            }

            bool inventoryIsActive = PlayerMenu.Instance.MenuIsActive();

            isMoving = false;
            if (isMelee)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

            }
            if (!isAttacking)
            {
                if (!menuOpen)
                {
                    if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5)
                    {
                        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, body.velocity.y);
                        if (Input.GetAxisRaw("Horizontal") > 0)
                        {
                            body.velocity *= dashSpeedRight;
                        }
                        if (Input.GetAxisRaw("Horizontal") < 0)
                        {
                            body.velocity *= dashSpeedLeft;
                        }
                        dir = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                        isMoving = true;
                        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

                    }
                    if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5)
                    {
                        body.velocity = new Vector2(body.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                        isMoving = true;
                        if (Input.GetAxisRaw("Vertical") > 0)
                        {
                            body.velocity *= dashSpeedUp;
                        }
                        if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            body.velocity *= dashSpeedDown;
                        }
                        dir = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                    }
                    if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
                    {
                        body.velocity = new Vector2(0f, body.velocity.y);
                        //body.velocity *= dashSpeedLeft;
                    }
                    if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
                    {
                        body.velocity = new Vector2(body.velocity.x, 0f);
                        //body.velocity *= dashSpeedDown;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1) && !inventoryIsActive && !menuOpen)
            {
                if (isMelee)
                {
                    isMelee = false;
                    swordSkill1 = false;
                    swordSkill2 = false;
                    swordSkill3 = false;
                }
                else
                {
                    isMelee = true;
                    bowSkill1 = false;
                    bowSkill2 = false;
                    bowSkill3 = false;
                }
            }
            if (bowSkill2)
            {
                PlayerManager.Instance.m_currentMana--;
                if (!PlayerManager.Instance.unlimited)
                {
                    if (PlayerManager.Instance.arrowType == 0)
                    {
                        Inventory.Instance.Remove("Arrow", 1);
                    }
                    if (PlayerManager.Instance.arrowType == 1)
                    {
                        Inventory.Instance.Remove("Slow Arrow", 1);
                    }
                    if (PlayerManager.Instance.arrowType == 2)
                    {
                        Inventory.Instance.Remove("Homing Arrow", 1);
                    }
                }

                if (PlayerManager.Instance.m_currentMana <= 0)
                {
                    bowSkill2 = false;
                }
            }
            else
            {
                transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
            if (swordSkill2)
            {
                PlayerManager.Instance.m_currentMana--;
                if (!PlayerManager.Instance.invulnerable)
                {
                    PlayerManager.Instance.m_currentHealth--;
                }

                if (PlayerManager.Instance.m_currentMana <= 0)
                {
                    swordSkill2 = false;
                }
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }
            if (bowSkill3)
            {
                PlayerManager.Instance.unlimited = true;
                PlayerManager.Instance.m_currentMana--;
            }
            else
            {
                PlayerManager.Instance.unlimited = false;
            }
            if (swordSkill3)
            {
                PlayerManager.Instance.invulnerable = true;
                PlayerManager.Instance.m_currentMana--;
            }
            else
            {
                PlayerManager.Instance.invulnerable = false;
            }
            if (Input.GetMouseButtonDown(0) && attackTimer <= 0 && !inventoryIsActive && !menuOpen)
            {
                if (!isMelee && count > 0)
                {
                    //mouseClick.Set(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
                    attackTimer = attackTime;
                    isAttacking = true;
                    body.velocity = Vector2.zero;
                    if (isMelee)
                    {
                        anim.SetBool("isAttacking", isAttacking);
                    }
                    count--;
                    if (!PlayerManager.Instance.unlimited)
                    {
                        if (PlayerManager.Instance.arrowType == 0)
                        {
                            Inventory.Instance.Remove("Arrow", 1);
                        }
                        if (PlayerManager.Instance.arrowType == 1)
                        {
                            Inventory.Instance.Remove("Slow Arrow", 1);
                        }
                        if (PlayerManager.Instance.arrowType == 2)
                        {
                            Inventory.Instance.Remove("Homing Arrow", 1);
                        }
                    }
                    
                }
                if (isMelee)
                {
                    attackTimer = attackTime;
                    isAttacking = true;
                    body.velocity = Vector2.zero;
                    if (isMelee)
                    {
                        anim.SetBool("isAttacking", isAttacking);
                    }
                }


            }
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                if (isMelee)
                {
                    anim.SetBool("isAttacking", isAttacking);
                }
            }
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            anim.SetFloat("LastMoveX", dir.x);
            anim.SetFloat("LastMoveY", dir.y);
            anim.SetBool("isMoving", isMoving);

            if (ShopMenu.Instance)
            {
                // Check if shop is active
                if (ShopMenu.Instance.MenuIsActive())
                {
                    // If player presses E, interact
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Make shop inactive
                        ShopMenu.Instance.transform.GetChild(0).gameObject.SetActive(false);
                        // Set Shop Menu as inative
                        ShopMenu.Instance.SetMenuActive(false);
                    }
                }
                else
                {
                    // Check if there are interactables
                    if (PlayerManager.Instance.m_interactable != null)
                    {
                        // If player presses E, interact
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            // Interact
                            PlayerManager.Instance.m_interactable.Interact();
                        }
                    }
                }
            }

            if((ShopMenu.Instance && ShopMenu.Instance.MenuIsActive()) || PlayerMenu.Instance.MenuIsActive() || skillTree || levelTree)
            {
                // Set Hotbar to inactive
                HotbarParent.Instance.transform.GetChild(0).gameObject.SetActive(false);
                HotbarParent.Instance.SetMenuActive(false);
                menuOpen = true;
            }
            else if(!HotbarParent.Instance.transform.GetChild(0).gameObject.activeSelf)
            {
                // Set Hotbar to active
                HotbarParent.Instance.transform.GetChild(0).gameObject.SetActive(true);
                HotbarParent.Instance.SetMenuActive(true);
                menuOpen = false;
            }

            // Check if menu is open
            if(!menuOpen)
            {
                // Check if player used left shift
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    // Use the item
                    Hotbar.Instance.UseHotbarItem(HotbarParent.Instance.GetCurrentSlotID());
                }
            }
        }
        
    }
}
