using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D body;
    public Vector3 target;
    public Vector2 dir;
    public float shortestDist;
    public float cooldownTime;
    public float cooldownTimer;
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
    public Vector2 roomStart;
    public Vector2 roomEnd;
    public GameObject damageCounter;
    public bool chase;
    public float idle;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        maxHP += PlayerManager.Instance.m_currentLevel * 50;
        AttackDmg += PlayerManager.Instance.m_currentLevel * 10;
        currentHP = maxHP;
        shortestDist = 100000;
        target.Set(transform.position.x, transform.position.y, transform.position.z);
        knockback = false;
        stun = false;
        slow = false;
        slowTimer = 5;
        slowSpeed = 1;
        cooldownTime = 1.5f;
        dir.Set(0, 0);
        idle = 2;
        chase = false;
    }
	
	// Update is called once per frame
	void Update () {
        string help = "Start:" + roomStart.x + "," + roomStart.y + "End: " + roomEnd.x + "," + roomEnd.y;
        Debug.Log(help);
        if (!PlayerManager.Instance.pause)
        {
            if (slow)
            {
                slowSpeed = 0.5f;
                slowTimer -= Time.deltaTime;
            }
            if (slowTimer <= 0)
            {
                slow = false;
                slowSpeed = 1.0f;
                slowTimer = 5f;
            }
            shortestDist = 100000;
            if (!knockback && !stun)
            {
                if(!chase)
                {
                    if(Vector2.Distance(transform.position, target) <= 1)
                    {
                        idle =- Time.deltaTime;

                    }
                    if(idle <= 0)
                    {
                        target.x = Random.Range(roomStart.x, roomEnd.x);
                        target.y = Random.Range(roomStart.y, roomEnd.y);
                        idle = 2;
                    }
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
                    {
                        if (GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x > roomStart.x && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x < roomEnd.x
                            && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y > roomStart.y && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y < roomEnd.y)
                        {
                            float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Player")[i].transform.position, transform.position);
                            if (dist < 5 && dist > 0 && dist < shortestDist)
                            {
                                shortestDist = dist;
                                target.Set(GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x, GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y, GameObject.FindGameObjectsWithTag("Player")[i].transform.position.z);
                                chase = true;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
                    {
                        if (GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x > roomStart.x && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x < roomEnd.x
                        && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y > roomStart.y && GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y < roomEnd.y)
                        {
                            float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Player")[i].transform.position, transform.position);
                            if (dist < 5 && dist > 0 && dist < shortestDist)
                            {
                                shortestDist = dist;
                                target.Set(GameObject.FindGameObjectsWithTag("Player")[i].transform.position.x, GameObject.FindGameObjectsWithTag("Player")[i].transform.position.y, GameObject.FindGameObjectsWithTag("Player")[i].transform.position.z);
                                chase = true;
                            }
                        }
                        else
                        {
                            chase = false;
                            idle = 0;
                            target.Set(transform.position.x, transform.position.y, transform.position.z);
                        }
                    }
                }
               
                dir.Set(target.x - transform.position.x, target.y - transform.position.y);
                body.velocity = dir.normalized * slowSpeed;
            }
            else if (knockback)
            {
                dir.Set(transform.position.x - target.x, transform.position.y - target.y);
                body.velocity = dir.normalized * knockbackDist;
                knockbackTime--;
            }
            else if (stun)
            {
                body.velocity.Set(0, 0);
                stunTimer -= Time.deltaTime;
            }
            if (knockbackTime <= 0 && knockback)
            {
                knockback = false;
                stun = true;
                stunTimer = 0.5f;
            }
            if (stunTimer <= 0)
            {
                stun = false;
            }


            if (cooldownTimer > -1)
            {
                cooldownTimer -= Time.deltaTime;
            }
            if (currentHP <= 0)
            {
                gameObject.SetActive(false);
                PlayerManager.Instance.m_currentExp += 10 * 1.5f * PlayerManager.Instance.m_currentLevel;
                PlayerManager.Instance.m_moneyAmount += 100;
                var clone = (GameObject)Instantiate(damageCounter, transform.position, transform.rotation);
                clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "" + PlayerManager.Instance.m_dmg + "\n" + "+" + (10 * 1.5f * PlayerManager.Instance.m_currentLevel) + "exp" + "\n" + "+" + 100 + "coins";
            }
        }
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!PlayerManager.Instance.pause)
        {
            if (other.gameObject.tag == "Player" && cooldownTimer <= 0)
            {
                if (!PlayerManager.Instance.invulnerable)
                {
                    other.gameObject.GetComponent<PlayerManager>().MinusHP(AttackDmg);
                    var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
                    clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "-" + AttackDmg;
                    cooldownTimer = cooldownTime;
                }

            }
            if (other.gameObject.tag == "Enemy" && !chase)
            {
                Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!PlayerManager.Instance.pause)
        {
            if (other.gameObject.tag == "Player" && cooldownTimer <= 0)
            {
                if (!PlayerManager.Instance.invulnerable)
                {
                    other.gameObject.GetComponent<PlayerManager>().MinusHP(AttackDmg);
                    var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
                    clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "-" + AttackDmg;
                    cooldownTimer = cooldownTime;
                }

            }
        }
    }
    public void MinusHP(int value)
    {
        currentHP -= value;
    }
}

