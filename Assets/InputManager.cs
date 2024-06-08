using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public bool inspectMode = false;
    GameManager gameManager;
    ClickManager clickManager;
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of InputManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // If another instance already exists, destroy this one
        }
    }

    public Transform Bar;
    public bool inventoryActive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  {
            inventoryToggle();
        }
        if (Input.GetKeyDown(KeyCode.I))  {
            Debug.Log("I was pressed!");
            if (inspectMode)  {
                inspectMode = false;
            }
            else  {
                inspectMode = true;
            }
            ClickManager.Instance.setSelectedItem(null);
        }
    }

    public bool getInventoryActive()  {
        return(inventoryActive);
    }

    public bool getInspectMode()  {
        return(inspectMode);
    }

    public void inventoryToggle()
    {
        if (inventoryActive == true)  {
            Bar.position += new Vector3(0, 2, 0);
            inventoryActive = false;
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                collectedItem.item.position += new Vector3(0, 2, 0);
                collectedItem.inventoryImage.gameObject.SetActive(false);
            }
        }
        else  {
            Bar.position += new Vector3(0, -2, 0);
            inventoryActive = true;
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                collectedItem.item.position += new Vector3(0, -2, 0);
                collectedItem.inventoryImage.gameObject.SetActive(true);
            }
        }
        Debug.Log("Space bar was pressed!");
    }
}