using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour {
    // How close player has to get to interactable
    public float m_radius = 3f;

    // Reference to player's transform
    private Transform m_player;
    // Bool to check if player has interacted with the interactable
    private bool m_isInteract = false;

    // What happens when interacting with the interactable
    public virtual void Interact()
    {
        // Meant to be overwritten
    }

    void Update()
    {

    }
}
