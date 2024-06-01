using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Transform player;
    static float moveSpeed = 6f, itemMoveSpeed = 60f;
    GameManager gameManager;
    public Transform DefInventory;
    public void Start()  {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void goToItem(itemData item)  {
        if (item.goToPoint == null)  {
            Debug.Log("NULL" + item.goToPoint.position);
        }
        else  {
            StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position, moveSpeed));
            attemptItemAquisition(item);

        }
    }

    private void attemptItemAquisition(itemData item)  {
        if (item.requiredItemID == -1 || GameManager.CollectedItems.Contains(item))  {
            GameManager.CollectedItems.Add(item);
            Debug.Log("itemID " + item.itemID + " has been aquired!");
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                Debug.Log("Collected item ID: " + collectedItem.itemID);
            }
            if (InputManager.Instance.getInventoryActive() == false)  {
                item.item.position += new Vector3(0, 2, 0); 
                InputManager.Instance.InventoryToggle();
            }
            StartCoroutine(gameManager.MoveToPoint(item.item, item.inventoryPosition.position, item.speed));
            //item.item.position = DefInventory.position;
            //goToInventory(item);
        }
    }

    /*public void goToInventory(itemData item)  {
        item.position = DefInventory;
    }*/
}
