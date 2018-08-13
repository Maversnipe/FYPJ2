using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSlot : MonoBehaviour {
    // Item contained in Slot
    public Item m_item;

    // Check if pointer is within slot
    private bool m_containsPoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Check if mouse cursor is in slot and if slot is empty
        if (m_containsPoint)
        {
            // Can add item info screen here
            if (!PlayerMenu.Instance.m_infoBox.gameObject.activeSelf)
            {
                // Set Info box to be active
                PlayerMenu.Instance.m_infoBox.gameObject.SetActive(true);
                // Set the item information
                PlayerMenu.Instance.m_infoBox.transform.GetChild(1).GetComponent<Text>().text = m_item.m_itemDesc;
            }
            else
            {
                // Set Info box to be not active
                PlayerMenu.Instance.m_infoBox.gameObject.SetActive(false);
            }
        }
    }

    // Getter for contains point
    public bool ContainsPoint()
    {
        return m_containsPoint;
    }

    // Setter for contains point
    public void SetContainsPoint(bool _contains)
    {
        m_containsPoint = _contains;
    }
}
