using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameManager gameManager;
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of InputManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure InputManager persists between scenes
        }
        else
        {
            Destroy(gameObject); // If another instance already exists, destroy this one
        }
    }


    public Transform Bar;
    public bool inventoryActive;
    // Update is called once per frame

    void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {

            // Call the function you want to trigger
            InventoryToggle();
        }
    }

    // Define the function you want to trigger

    public bool getInventoryActive()  {
        return(inventoryActive);
    }

    public void InventoryToggle()
    {
        if (inventoryActive == true)  {
            Bar.position += new Vector3(0, 2, 0);
            inventoryActive = false;
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                collectedItem.item.position += new Vector3(0, 2, 0); 
            }
        }
        else  {
            Bar.position += new Vector3(0, -2, 0);
            inventoryActive = true;
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                collectedItem.item.position += new Vector3(0, -2, 0); 
            }
        }
        // Add your function logic here
        Debug.Log("Space bar was pressed!");
    }
}
