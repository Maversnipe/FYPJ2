using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public float rotationOffset;
    public Vector3 diff;

    public Transform arrowPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle + rotationOffset);
        if (GameObject.Find("Player"))
        {
            if (GameObject.Find("Player").GetComponent<PlayerController>().isAttacking && !GameObject.Find("Player").GetComponent<PlayerController>().isMelee)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().isAttacking = false;
                Instantiate(arrowPrefab, transform.position, transform.rotation);
            }
        }
    }
}
