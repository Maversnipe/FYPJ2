using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D body;
    public Vector2 target;
    public Vector2 dir;
    public float shortestDist;
    public float cooldownTime;
    private float cooldownTimer;
    public int AttackDmg;
    public int currentHP;
    public int maxHP;
    public bool knockback;
    public float knockbackTime;
    public float knockbackDist;
    public bool stun;
    public bool slow;
    public float slowTimer;
    public float slowSpeed;
    public float stunTimer;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        maxHP += PlayerManager.Instance.m_currentLevel * 50;
        AttackDmg += PlayerManager.Instance.m_currentLevel * 2;
        currentHP = maxHP;
        shortestDist = 100000;
        target.Set(transform.position.x, transform.position.y);
        knockback = false;
        stun = false;
        slow = false;
        slowTimer = 5;
        slowSpeed = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if(slow)
        {
            slowSpeed = 0.5f;
            slowTimer -= Time.deltaTime;
        }
        if(slowTimer <= 0)
        {
            slow = false;
            slowSpeed = 1.0f;
            slowTimer = 5f;
        }
        shortestDist = 100000;
        dir.Set(0, 0);
        if(!knockback && !stun)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Player")[i].transform.position, transform.position);
                if (dist < 5 && dist > 0 && dist < shortestDist)
                {
                    shortestDist = dist;
                    target.Set(GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x, GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y);
                }
            }
            dir.Set(target.x - transform.position.x, target.y - transform.position.y);
            body.velocity = dir.normalized * slowSpeed;
        }
        else if(knockback)
        {
            dir.Set(transform.position.x - target.x, transform.position.y - target.y);
            body.velocity = dir.normalized * knockbackDist;
            knockbackTime--;
        }
        else if(stun)
        {
            body.velocity.Set(0, 0);
            stunTimer -= Time.deltaTime;
        }
        if(knockbackTime <= 0 && knockback)
        {
            knockback = false;
            stun = true;
            stunTimer = 0.5f;
        }
        if(stunTimer <= 0)
        {
            stun = false;
        }
        

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if(currentHP <= 0)
        {
            gameObject.SetActive(false);
            PlayerManager.Instance.m_currentExp += 100 * 1.5f * PlayerManager.Instance.m_currentLevel;
            PlayerManager.Instance.m_moneyAmount += 100;
        }
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && cooldownTimer <= 0)
        {
            if(!PlayerManager.Instance.invulnerable)
            {
                other.gameObject.GetComponent<PlayerManager>().MinusHP(AttackDmg);
                cooldownTimer = cooldownTime;
            }

        }
    }
    public void MinusHP(int value)
    {
        currentHP -= value;
    }
}

