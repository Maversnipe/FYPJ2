using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Player"))
        {
            if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 5 && Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) > 0)
            {
                //transform.position = Vector3.Lerp(GameObject.Find("Player").transform.position, transform.position, moveSpeed * Time.deltaTime);

                body.velocity = new Vector2((GameObject.Find("Player").transform.position - transform.position).x, (GameObject.Find("Player").transform.position - transform.position).y);
                body.velocity.Normalize();
            }
        }
	}
}
