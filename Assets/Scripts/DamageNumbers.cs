using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbers : MonoBehaviour {

    public float moveSpeed;
    public int dmg;
    public Text dmgText;
    public float timer;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //dmgText.text = "" + dmg;
        if (!PlayerManager.Instance.pause)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime));
            if (timer <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
            timer -= Time.deltaTime;
        }
    }
}
