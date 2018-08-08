using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public int attackDmg;
    public GameObject damageCounter;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && transform.parent.parent.gameObject.GetComponent<PlayerController>().isAttacking)
        {
            other.gameObject.GetComponent<AI>().MinusHP(PlayerManager.Instance.m_dmg);
            var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
            clone.GetComponentInChildren<DamageNumbers>().dmg = PlayerManager.Instance.m_dmg;
        }
    }
}
