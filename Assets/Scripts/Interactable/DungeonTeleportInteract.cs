using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonTeleportInteract : Interactable
{
    // Interaction Text
    public Text m_interactionText;

    // Use this for initialization
    void Start ()
    {
        // Set radius
        m_radius = 1f;
    }

    // Interaction
    public override void Interact()
    {
        base.Interact();
        // Set text to be inactive
        m_interactionText.gameObject.SetActive(false);
        // Set interactable to null
        PlayerManager.Instance.m_interactable = null;
        // Set to be focused
        m_isFocus = false;
        // Change scene to the dungeon scene
        SceneManager.LoadScene("Test");
    }

    void Update()
    {
        // Check if in focus 
        if (m_isFocus)
        {
            // Check if text is active
            if (!m_interactionText.gameObject.activeSelf)
            {
                // Set text to be active
                m_interactionText.gameObject.SetActive(true);
                // Set text
                m_interactionText.text = "Press E to go to the Dungeon";
            }

            // Checks if still within radius
            float dist = Vector3.Distance(PlayerManager.Instance.m_player.transform.position, transform.position);
            if (dist > m_radius)
            {   // If not within radius
                // Remove from interactable
                PlayerManager.Instance.m_interactable = null;
                // Set to be focused
                m_isFocus = false;
                // Set text to be inactive
                m_interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Check if near player
            // Get Dist between player and interactable
            if (PlayerManager.Instance.m_interactable == null
                && !ShopMenu.Instance.MenuIsActive() && !PlayerMenu.Instance.MenuIsActive())
            {
                float dist = Vector3.Distance(PlayerManager.Instance.m_player.transform.position, transform.position);
                if (dist <= m_radius)
                {   // Add this interactable to the list of interactables
                    PlayerManager.Instance.m_interactable = this;
                    // Set to be focused
                    m_isFocus = true;
                    Debug.Log(gameObject.name);
                }
            }
        }
    }
}
