using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Vector2 dir;
    public float FlightTime;
    public float FlightTimer;
    public float moveSpeed;
    private Rigidbody2D body;
    public int Damage;
    // Use this for initialization
    void Start () {
        FlightTime = 1.5f;
        FlightTimer = 0f;
        moveSpeed = 10f;
        Damage = 10;
        body = GetComponent<Rigidbody2D>();

        dir.Set(GameObject.Find("Bow").GetComponent<Bow>().diff.x, GameObject.Find("Bow").GetComponent<Bow>().diff.y);
    }
	
	// Update is called once per frame
	void Update () {

		if(FlightTimer <= FlightTime || transform.childCount <=0)
        {
            body.velocity = dir * moveSpeed;
           // transform.position += new Vector3(dir.x, dir.y, 0) * Time.deltaTime * moveSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
        FlightTimer += Time.deltaTime;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<AI>().MinusHP(Damage);
            Destroy(gameObject);
        }
    }
}
