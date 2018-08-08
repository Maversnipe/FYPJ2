using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Vector2 dir;
    public float FlightTime;
    public float FlightTimer;
    public float moveSpeed;
    public bool skill1;
    private Rigidbody2D body;
    public int Damage;
    public GameObject damageCounter;
    public GameObject target;
    // Use this for initialization
    void Start () {
        FlightTime = 10.5f;
        FlightTimer = 0f;
        moveSpeed = 10f;
        Damage = PlayerManager.Instance.m_dmg;
        body = GetComponent<Rigidbody2D>();
        if(!skill1)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bow").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Bow")[i].transform.position.x == transform.position.x && GameObject.FindGameObjectsWithTag("Bow")[i].transform.position.y == transform.position.y)
                {
                    dir.Set(GameObject.FindGameObjectsWithTag("Bow")[i].GetComponent<Bow>().diff.x, GameObject.FindGameObjectsWithTag("Bow")[i].GetComponent<Bow>().diff.y);
                }
            }
        }

            // dir.Set(GameObject.Find("Bow").GetComponent<Bow>().diff.x, GameObject.Find("Bow").GetComponent<Bow>().diff.y);
        }
	
	// Update is called once per frame
	void Update () {

        if(skill1)
        {
            FlightTimer = 0;
            dir.Set(target.transform.position.y - transform.position.x, target.transform.position.y - transform.position.y);
            dir.Normalize();
            transform.rotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 45);
        }
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
            var clone = (GameObject)Instantiate(damageCounter, other.transform.position, other.transform.rotation);
            clone.GetComponentInChildren<DamageNumbers>().dmg = Damage;
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
