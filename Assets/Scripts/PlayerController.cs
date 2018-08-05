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
    public bool isAttacking;
    public bool isMelee;
    public float attackTime;
    private float attackTimer;
	private int count = 1;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        isMelee = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        // Check if player presses Inventory button
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // Set player menu active to the opposite of itself
            PlayerMenu.Instance.SetMenuActive(!PlayerMenu.Instance.MenuIsActive());
            // Set the menu object to active
            GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.SetActive(PlayerMenu.Instance.MenuIsActive());
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
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (!isAttacking)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5)
            {
                body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, body.velocity.y);
                isMoving = true;
                dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

            }
            if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5)
            {
                body.velocity = new Vector2(body.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                isMoving = true;
                dir = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }
            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                body.velocity = new Vector2(0f, body.velocity.y);
            }
            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                body.velocity = new Vector2(body.velocity.x, 0f);
            }
        }

		if(Input.GetMouseButtonDown(1) && !inventoryIsActive)
        {
            if(isMelee)
            {
                isMelee = false;
            }
            else
            {
                isMelee = true;
            }
        }
		if(Input.GetMouseButtonDown(0) && attackTimer<=0 && !inventoryIsActive)
        {
			if(!isMelee && count > 0)
			{            
				attackTimer = attackTime;
				isAttacking = true;
				body.velocity = Vector2.zero;
				if(isMelee)
				{
					anim.SetBool("isAttacking", isAttacking);
				}
				count--;
				//GameObject.Find ("Inventory").GetComponent<Inventory> ().RemoveItem (2);
			}
			if(isMelee)
			{
				attackTimer = attackTime;
				isAttacking = true;
				body.velocity = Vector2.zero;
				if(isMelee)
				{
					anim.SetBool("isAttacking", isAttacking);
				}
			}


        }
        if(attackTimer > 0)
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
    }
}
