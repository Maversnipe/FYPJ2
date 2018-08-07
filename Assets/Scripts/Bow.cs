using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public float rotationOffset;
    public Vector2 diff;

    public Transform arrowPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        diff.Set((Input.mousePosition.x - (float)(Screen.width * 0.5)), (Input.mousePosition.y - (float)(Screen.height * 0.5)));
        diff.Normalize();

        float rotAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle + rotationOffset);

        if (transform.parent.parent.gameObject.GetComponent<PlayerController>().isAttacking && !transform.parent.parent.gameObject.GetComponent<PlayerController>().isMelee)
        {
            diff.Set((Input.mousePosition.x - (float)(Screen.width * 0.5)), (Input.mousePosition.y - (float)(Screen.height * 0.5)));
            diff.Normalize();

            rotAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotAngle + rotationOffset);
            transform.parent.parent.gameObject.GetComponent<PlayerController>().isAttacking = false;
            Instantiate(arrowPrefab, transform.position, transform.rotation);
            diff.Set((Input.mousePosition.x - (float)(Screen.width * 0.5)), (Input.mousePosition.y - (float)(Screen.height * 0.5)));
            diff.Normalize();
            arrowPrefab.GetComponent<Arrow>().dir.Set(diff.x, diff.y);
            Debug.Log(diff);
        }
    }
}
