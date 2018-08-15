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
        }
        if(transform.parent.parent.gameObject.GetComponent<PlayerController>().bowSkill1)
        {
            int hello = 0;
            do
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
                {
                    float dist = Vector3.Distance(GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position, transform.position);
                    if (dist < 10)
                    {
                        Instantiate(arrowPrefab, transform.position, transform.rotation);
                        arrowPrefab.GetComponent<Arrow>().target = GameObject.FindGameObjectsWithTag("Enemy")[i];
                        arrowPrefab.GetComponent<Arrow>().skill1 = true;
                        hello++;
                        if (hello == 1)
                        {
                            break;
                        }
                    }
                }
            } while (hello != 3);

        }
    }
}
