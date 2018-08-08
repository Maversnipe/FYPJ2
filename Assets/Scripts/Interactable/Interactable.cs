using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    // Distance from interactable to interact
    public float m_radius = 3f;
    // Is focus
    protected bool m_isFocus = false;
    // Has interacted
    protected bool m_hasInteracted = false;

    // Interaction
    public virtual void Interact()
    {
        Debug.Log("Is it wrong place");
    }

    void Update()
    {
        
    }
}
