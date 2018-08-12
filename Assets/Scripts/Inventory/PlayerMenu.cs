using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {

    // Make this a Singleton
    private static PlayerMenu _instance;
    public static PlayerMenu Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // Info box image
    public Image m_infoBox;
    // Target Item (Basically the selected Item)
    public Stack<Item> m_targetItem;
    // The image of the target item
    public Image m_targetImage;

    // Current info box slot id
    private int m_currSlotID;
    // Check if there is a target item
    private bool m_hasTarget;
    // Check if player menu is active
    private bool m_menuIsActive;

    // Use this for initialization
    void Start () {
        // Set selected item to null
        m_targetItem = new Stack<Item>();
        // Set has target to false
        m_hasTarget = false;
        // Set menu to not active
        m_menuIsActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Checks if there is a target item
        if(m_hasTarget)
        {
            // Set the position to follow mouse cursor
            m_targetImage.rectTransform.position = new Vector3(Input.mousePosition.x, 
                                                               Input.mousePosition.y + m_targetImage.rectTransform.sizeDelta.y * 0.5f, 0f);
        }
    }
    
    // Getter for has target
    public bool HasTarget()
    {
        return m_hasTarget;
    }

    // Setter for has target
    public void SetHasTarget(bool _hasTarget)
    {
        m_hasTarget = _hasTarget;
    }

    // Getter for has current slot id
    public int GetCurrentSlotID()
    {
        return m_currSlotID;
    }

    // Setter for has target
    public void SetCurrSlotID(int _id)
    {
        m_currSlotID = _id;
    }

    // Getter for the menu is active
    public bool MenuIsActive()
    {
        return m_menuIsActive;
    }

    // Setter for the menu is active
    public void SetMenuActive(bool _isActive)
    {
        m_menuIsActive = _isActive;
    }
}
