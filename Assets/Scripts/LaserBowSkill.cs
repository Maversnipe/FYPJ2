using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBowSkill : MonoBehaviour {

    public int attackDmg;
    public GameObject damageCounter;
    float rotate;
    // Use this for initialization
    void Start()
    {
        rotate = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<AI>().MinusHP(PlayerManager.Instance.m_dmg);
            var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
            clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "" + PlayerManager.Instance.m_dmg;
        }
    }
}
