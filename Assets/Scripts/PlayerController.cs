using System.Collections;
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
    public class DoubleTap
    {
        public KeyCode key;
        public float time;
    }

    public List<DoubleTap> doubleTap = new List<DoubleTap>();
	// Use this for initialization
	void Start () {
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
        mana = 1000;
    }
	
	// Update is called once per frame
	void Update () {

        bowSkill1 = false;
        for(int i = 0; i < doubleTap.Count; i++)
        {
            doubleTap[i].time -= Time.deltaTime;
            if(doubleTap[i].time <= 0)
            {
                doubleTap.Remove(doubleTap[i]);
                i--;
            }
        }
        if(dash)
        {
            if(dashTime <= 0)
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // Set player menu active to the opposite of itself
            PlayerMenu.Instance.SetMenuActive(!PlayerMenu.Instance.MenuIsActive());
            // Set the menu object to active
            GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.SetActive(PlayerMenu.Instance.MenuIsActive());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isMelee)
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
            }
            else
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
                {
                    float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position, transform.position);
                    if (dist < 10)
                    {
                        bowSkill1 = true;

                    }
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMelee)
            {
                bowSkill2 = false;
                transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
                if(!swordSkill2)
                {
                    swordSkill2 = true;
                    transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    swordSkill2 = false;
                }
            }
            else
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isMelee)
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
            else
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
        // For double tap dash
        if (Input.GetKeyDown(KeyCode.W))
        {
            bool check = false;
            for(int i =0; i < doubleTap.Count; i++)
            {
                if(doubleTap[i].key == KeyCode.W)
                {
                    if(doubleTap[i].time > 0)
                    {
                        check = true;
                        if(!dash)
                        {
                            dash = true;
                            dashTime = dashTimeSet;
                            dashSpeedUp = dashTime;
                        }
                        doubleTap[i].time = 0;
                    }
                    break;
                }
            }
            if(!check)
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
                            dashSpeedDown =dashSpeed;
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
                if(Input.GetAxisRaw("Vertical") > 0)
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

		if(Input.GetMouseButtonDown(1) && !inventoryIsActive)
        {
            if(isMelee)
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
        if(bowSkill2)
        {
            mana--;
            if(mana <= 0)
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
            mana--;
            if (mana <= 0)
            {
                swordSkill2 = false;
            }
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && attackTimer<=0 && !inventoryIsActive)
        {
			if(!isMelee && count > 0)
			{
                //mouseClick.Set(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
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
