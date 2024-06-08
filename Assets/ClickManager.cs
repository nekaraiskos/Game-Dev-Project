using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    private bool redFlag = false, blueFlag = false, yellowFlag = false;
    public itemData selectedItem = null, noteWritten, safeNote;
    public Transform player, candleLit, safeOpen;
    static float moveSpeed = 6f, itemMoveSpeed = 60f;
    GameManager gameManager;
    public Transform DefInventory;
    public void Start()  {
        gameManager = FindObjectOfType<GameManager>();
    }

    public static ClickManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of ClickManager exists
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

    public void goToItem(itemData item)  {
        if (item.goToPoint == null)  {
            Debug.Log("NULL" + item.goToPoint.position);
        }
        else  {
            StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position, moveSpeed));
            attemptItemAquisition(item);

        }
    }

    public void selectItem(itemData item)  {
        if (InputManager.Instance.getInspectMode())  {
            return;
        }
        if (selectedItem != null && selectedItem == item)  {
            selectedItem = null;
            return;
        }
        if (GameManager.CollectedItems.Contains(item))  {
            Debug.Log("ITEM SELECTED: " + item.itemID);
            selectedItem = item;
        }
    }

        public void destroyImage(Image imageToDestroy)  {
        if (InputManager.Instance.getInspectMode())  {
            return;
        }
        Destroy(imageToDestroy);
    }

    public void destroyObject(GameObject objectToDestroy)  {
        if (InputManager.Instance.getInspectMode())  {
            return;
        }
        Destroy(objectToDestroy);
    }

    public void events(imageData imageCheck)  {

        if (selectedItem != null)  {
            switch (selectedItem.itemID)  {
                case 1:
                    selectedItem.item.position = new Vector3(0.79f, -2.98f, 0);
                    Debug.Log("OH THE IRONY");
                    break;
                case 3:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        candleLit.position = new Vector3(-5.73952f, 2.24513f, 0);
                        Destroy(imageCheck.image);
                        selectedItem = null;
                        Debug.Log("Arson achieved");
                    }
                    break;
                case 4:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        Destroy(selectedItem.gameObject);
                        attemptItemAquisition(noteWritten);
                        selectedItem = null;
                        Debug.Log("Arson achieved");
                    }
                    break;
                case 5:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(4.619f, 3.33f, 0);
                        redFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        Debug.Log("RED frame PLACED");
                        if (redFlag && blueFlag && yellowFlag)  {
                            Debug.Log("EURIKA");
                        }
                    }
                    break;
                case 6:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(3.62f, 3.33f, 0);
                        Destroy(imageCheck);
                        blueFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        Debug.Log("BLUE frame PLACED");
                        if (redFlag && blueFlag && yellowFlag)  {
                            Debug.Log("EURIKA");
                        }
                    }
                    break;
                case 7:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(5.623f, 3.33f, 0);
                        Destroy(imageCheck);
                        yellowFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        Debug.Log("YELLOW frame PLACED");
                        if (redFlag && blueFlag && yellowFlag)  {
                            Debug.Log("EURIKA");
                        }
                    }
                    break;
                case 8:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        safeOpen.position = new Vector3(5.6f, -3.3f, 0);
                        Destroy(imageCheck.image);
                        attemptItemAquisition(safeNote);
                        selectedItem = null;
                    }
                    break;
                default:
                    Debug.Log("Unknown item selected.");
                    break;
            }
        }
    }

    public void inpsectItemEvents(itemData item)  {
        if (!GameManager.CollectedItems.Contains(item))  {
            return;
        }
        if (InputManager.Instance.getInspectMode())  {
            switch (item.itemID)  {
                case 1:
                        Debug.Log("My what a lovely tie. I just hope no-one had to die to this tie. That would just make me cry");
                        break;
                case 3:
                        Debug.Log("Matchtastic");
                        break;
                case 4:
                        Debug.Log("This is a piece of paper made of wood");
                        break;
                case 5:
                        Debug.Log("Ze red frame");
                        break;
                case 6:
                        Debug.Log("Ze blue frame");
                        break;
                case 7:
                        Debug.Log("Ze yellow frame");
                        break;
                case 8:
                        Debug.Log("Useless piece of paper, just has a bunch of random numbers written on it. Useless");
                        break;
                case 9:
                        Debug.Log("Something something killer");
                        break;
                default:
                    Debug.Log("No comment");
                    break;
            }
        }
    }

    public void inpsectEnvironmentEvents(int envCode)  {
        if (InputManager.Instance.getInspectMode())  {
            switch (envCode)  {
                case 0:
                        Debug.Log("Yeeeeeeeep... he dead");
                        break;
                case 1:
                        Debug.Log("It's a wardrobe");
                        break;
                case 2:
                        Debug.Log("An inconspicuous UNLIT candle, hmmmmmm. What could i possibly do with an UNLIT candle");
                        break;
                case 3:
                        Debug.Log("Such a nice STRONG flame, why you could probably see through objects made from wood composites with it");
                        break;
                case 4:
                        Debug.Log("Nothing to see here. Just your typical floor safe");
                        break;
                default:
                    Debug.Log("No comment");
                    break;
            }
        }
    }

    public void setSelectedItem(itemData item)  {
        selectedItem = item;
    }

    public void attemptItemAquisition(itemData item)  {
        if (InputManager.Instance.getInspectMode())  {
            return;
        }
        if (item.itemID > 0 && (item.requiredItemID == -1 || GameManager.CollectedItems.Contains(item)))  {
            GameManager.CollectedItems.Add(item);
            Debug.Log("itemID " + item.itemID + " has been aquired!");
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                Debug.Log("Collected item ID: " + collectedItem.itemID);
            }
            if (InputManager.Instance.getInventoryActive() == false)  {
                item.item.position += new Vector3(0, 2, 0); 
                InputManager.Instance.inventoryToggle();
            }
            StartCoroutine(gameManager.MoveToPoint(item.item, item.inventoryPosition.position, item.speed));
            item.changeSortingLayer("UI");
            Destroy(item.itemImage);
            //item.item.position = DefInventory.position;
            //goToInventory(item);
        }
    }

    /*public void goToInventory(itemData item)  {
        item.position = DefInventory;
    }*/
}
