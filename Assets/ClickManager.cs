using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    private bool redFlag = false, blueFlag = false, yellowFlag = false;
    public Text jealousy;
    public itemData selectedItem = null, noteWritten, safeNote;
    public Transform player, candleLit, safeOpen;
    static float moveSpeed = 6f, itemMoveSpeed = 60f;
    GameManager gameManager;
    UIManager uiManager;
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
                    UIManager.Instance.UpdateText("It fits...");
                    break;
                case 3:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        candleLit.position = new Vector3(-5.73952f, 2.24513f, 0);
                        Destroy(imageCheck.image);
                        selectedItem = null;
                        UIManager.Instance.UpdateText("Arson achieved");
                    }
                    break;
                case 4:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        Destroy(selectedItem.gameObject);
                        attemptItemAquisition(noteWritten);
                        selectedItem = null;
                        UIManager.Instance.UpdateText("Arson achieved");
                    }
                    break;
                case 5:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(4.619f, 3.33f, 0);
                        redFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        Destroy(imageCheck.gameObject);
                        if (redFlag && blueFlag && yellowFlag)  {
                            jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                            UIManager.Instance.UpdateText("A message has apeared on the wall.");
                        }
                    }
                    break;
                case 6:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(3.62f, 3.33f, 0);
                        Destroy(imageCheck.gameObject);
                        blueFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        if (redFlag && blueFlag && yellowFlag)  {
                            jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                            UIManager.Instance.UpdateText("A message has apeared on the wall.");
                        }
                    }
                    break;
                case 7:
                    if (selectedItem.itemID == imageCheck.imageID)  {
                        selectedItem.item.position = new Vector3(5.623f, 3.33f, 0);
                        Destroy(imageCheck.gameObject);
                        yellowFlag = true;
                        GameManager.CollectedItems.Remove(selectedItem);
                        Destroy(selectedItem.inventoryImage);
                        selectedItem = null;
                        if (redFlag && blueFlag && yellowFlag)  {
                            jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                            UIManager.Instance.UpdateText("A message has apeared on the wall.");
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
                    UIManager.Instance.UpdateText("Unknown item selected.");
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
                        UIManager.Instance.UpdateText("It's a tie covered in blood. Probably the weapon used to commit the murder. It has pretty garish colours.");
                        break;
                case 3:
                        UIManager.Instance.UpdateText("A simple match box.");
                        break;
                case 4:
                        UIManager.Instance.UpdateText("It's a piece of paper. I can faintly feel indentations on it, as if it has been written on, but i can's see aything.");
                        break;
                case 5:
                        UIManager.Instance.UpdateText("It's a red frame. It can be placed on a surface or be hang.");
                        break;
                case 6:
                        UIManager.Instance.UpdateText("It's a blue frame. It can be placed on a surface or be hang.");
                        break;
                case 7:
                        UIManager.Instance.UpdateText("It's a yellow frame. It can be placed on a surface or be hang.");
                        break;
                case 8:
                        UIManager.Instance.UpdateText("It has random numbers written on it now.");
                        break;
                case 9:
                        UIManager.Instance.UpdateText("It reads: Dear brother, I am tired of seeing all the attention and admiration you always receive. I cannot compete with you anymore. I hate you!");
                        break;
                default:
                    UIManager.Instance.UpdateText("No comment");
                    break;
            }
        }
    }

    public void inpsectEnvironmentEvents(int envCode)  {
        if (InputManager.Instance.getInspectMode())  {
            switch (envCode)  {
                case 0:
                        UIManager.Instance.UpdateText("He has already passed. The cause of death seems to be asphyxiation. The blood came from the fall afterwards");
                        break;
                case 1:
                        UIManager.Instance.UpdateText("It's a wardrobe");
                        break;
                case 2:
                        UIManager.Instance.UpdateText("It's a canlde. It seems to have been used frequently and recently.");
                        break;
                case 3:
                        UIManager.Instance.UpdateText("It holds a suprisingly strong flame, for its size.");
                        break;
                case 4:
                        UIManager.Instance.UpdateText("It needs a number combination to open");
                        break;
                case 5:
                        UIManager.Instance.UpdateText("It's a hook hammered into the wall.");
                        break;
                default:
                    UIManager.Instance.UpdateText("No comment");
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
