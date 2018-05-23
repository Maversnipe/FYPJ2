using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D body;
    public float cooldownTime;
    private float cooldownTimer;
    public int AttackDmg;
    public int currentHP;
    public int maxHP;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Player"))
        {
            if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 5 && Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) > 0)
            {
                body.velocity = new Vector2((GameObject.Find("Player").transform.position - transform.position).x, (GameObject.Find("Player").transform.position - transform.position).y);
                body.velocity.Normalize();
            }
        }
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if(currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && cooldownTimer <= 0)
        {
            other.gameObject.GetComponent<PlayerManager>().MinusHP(AttackDmg);
            cooldownTimer = cooldownTime;
        }
    }
    public void MinusHP(int value)
    {
        currentHP -= value;
    }
}

