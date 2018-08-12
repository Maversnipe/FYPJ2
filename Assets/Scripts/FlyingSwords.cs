using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwords : MonoBehaviour {

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
        rotate += Time.deltaTime + 10;
        transform.rotation = Quaternion.Euler(0f, 0f, rotate);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerManager.Instance.pause)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<AI>().MinusHP(PlayerManager.Instance.m_dmg);
                var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
                clone.GetComponentInChildren<DamageNumbers>().dmgText.text = "" + PlayerManager.Instance.m_dmg;
            }
        }
    }
}
