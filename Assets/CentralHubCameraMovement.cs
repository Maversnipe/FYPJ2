using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralHubCameraMovement : MonoBehaviour {
    // Player
    public GameObject m_player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Temp variables for x and y pos
        float posX, posY;
        // Set the temp pos to player pos
        posX = m_player.transform.position.x;
        Debug.Log(posX);
        posY = m_player.transform.position.y;

        // Check if camera will go out of bounds
        // Check Left and right
        if (posX < -3.6f)
            posX = -3.6f;
        else if (posX > 3.6f)
            posX = 3.6f;
        // Check up and down
        if (posY > 2.5f)
            posY = 2.5f;
        else if (posY < -2.5f)
            posY = -2.5f;

        // Set the position
        transform.position = new Vector3(posX, posY, transform.position.z);
	}
}
