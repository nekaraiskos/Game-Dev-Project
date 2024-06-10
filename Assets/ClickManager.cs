using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ClickManager : MonoBehaviour
{
    //These are the preloaded, out of bounds objects for level 1
    private bool redFlag = false, blueFlag = false, yellowFlag = false;
    public Text jealousy;
    public Transform candleLit, safeOpen, lampOpen;
    public itemData noteWritten, safeNote;
    //These are the preloaded, out of bounds objects for level 2
    public Transform smallLockerOpen, tempObject = null;
    public itemData cakePoison, cakeChocolate;
    //These are the preloaded, out of bounds objects for level 3
    private bool evidence1 = false, evidence2 = false, evidence3 = false, evidence4 = false, evidence5 = false;
    public itemData selectedItem = null, holdItem = null;
    public Image tempImage = null;
    static float moveSpeed = 6f, itemMoveSpeed = 60f;
    GameManager gameManager;
    UIManager uiManager;
    AudioManager audioManager;
    public void Start()  {
        gameManager = FindObjectOfType<GameManager>();
    }

    public static ClickManager Instance { get; private set; }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

        if (candleLit == null && cakePoison != null)  {
            UIManager.Instance.UpdateText("I need to gain access to the kitchen if i want to poison that fat pig, however simply walking in will get me kicked right back out...");
        }
    }

    public void goToItem(itemData item)  {
        if (item.goToPoint == null)  {
            Debug.Log("NULL" + item.goToPoint.position);
        }
        else  {
            attemptItemAquisition(item);

        }
    }

    public void TempHoldObject(Transform tempHold)  {
        tempObject = tempHold;
    }

    public void TempHoldImage(Image placeTempImage)  {
        tempImage = placeTempImage;
    }

    public void TempHoldItem(itemData item)  {
        holdItem = item;
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
            audioManager.PlaySFX(audioManager.equip);
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
        if (objectToDestroy.GetComponent<itemData>() != null )  {
            GameManager.CollectedItems.Remove(objectToDestroy.GetComponent<itemData>());
        }
    }

    private IEnumerator delayedAction(int code)  {
        Debug.Log("Action started");
        float  speeeeeed = 1.0f;

        if (code == 101)  {
            holdItem.item.position += new Vector3(0, 10, 0);
            yield return new WaitForSeconds(1.0f);
            holdItem.item.position += new Vector3(0, -10, 0);
            holdItem = null;
        }
        else if (code == 108)  {
            selectedItem.item.position = new Vector3(5.5f, 4.5f, 0);
            for (int i = 0; i < 50; i++)  {
                //Debug.Log("Action loop started");
                if (i < 6) {
                    audioManager.PlaySFX(audioManager.eating);
                }
                else {
                    audioManager.PlaySFX(audioManager.bite);
                }
                selectedItem.item.position += new Vector3(1.1f, 0, 0);
                yield return new WaitForSeconds(speeeeeed);
                selectedItem.item.position += new Vector3(-1.1f, 0, 0);
                yield return new WaitForSeconds(speeeeeed);
                speeeeeed = speeeeeed - (speeeeeed * 0.2f);
            }
            selectedItem.item.position += new Vector3(1.1f, 0, 0);
            tempObject.position += new Vector3(-10, 0, 0);
            yield return new WaitForSeconds(0.5f);
            audioManager.PlaySFX(audioManager.deathSound);
        }

        Debug.Log("Action completed after 3 seconds delay");
    }

    public void startDialogue(string say)  {
        UIManager.Instance.UpdateText(say);
    }

    public void startDelayedAction(int code)  {
        StartCoroutine(delayedAction(code));
    }    

    public void moveObjectX(int posChange)  {
        
        tempObject.position += new Vector3(posChange, 0, 0);;
        tempObject = null;
    }

    public void moveImageX(int posChange)  {
        
        tempImage.transform.localPosition += new Vector3(posChange, 0, 0);
        tempImage = null;
    }

    public void setEvidence (int code)  {
        switch(code)  {
            case 1:
                evidence1 = true;
                break;
            case 2:
                evidence2 = true;
                break;
            case 3:
                evidence3 = true;
                break;
            case 4:
                evidence4 = true;
                break;
            case 5:
                evidence5 = true;
                break;
        }
        if (evidence1 == true && evidence2 == true && evidence3 == true && evidence4 == true && evidence5 == true)  {
            UIManager.Instance.UpdateText("Hmmm, these murders remind me of something, maybe the nine killer pleasures, um, i mean seven deadly sins. I just remembered I had another case in the file cabinet! But where is the key...");
        }

    }

    public void events(imageData imageCheck)  {

        if (selectedItem != null)  {
            if (selectedItem.itemID < 100)  {
                switch (selectedItem.itemID)  {
                    case 1:
                        selectedItem.item.position = new Vector3(0.79f, -2.98f, 0);
                        selectedItem.changeSortingLayer("Default");
                        UIManager.Instance.UpdateText("It fits...");
                        break;
                    case 3:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.lightMatch);
                            candleLit.position = new Vector3(-5.73952f, 2.24513f, 0);
                            Destroy(imageCheck.image);
                            selectedItem = null;
                            UIManager.Instance.UpdateText("Arson achieved");
                        }
                        break;
                    case 4:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.paperRuffles);
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.inventoryImage);
                            Destroy(selectedItem.gameObject);
                            attemptItemAquisition(noteWritten);
                            selectedItem = null;
                            UIManager.Instance.UpdateText("Arson achieved 2: set fire to the loo.");
                        }
                        break;
                    case 5:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.placeFrame);
                            selectedItem.item.position = new Vector3(4.619f, 3.33f, 0);
                            redFlag = true;
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.inventoryImage);
                            selectedItem = null;
                            Destroy(imageCheck.gameObject);
                            if (redFlag && blueFlag && yellowFlag)  {
                                jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                                lampOpen.position  += new Vector3(-10, 0, 0);
                                UIManager.Instance.UpdateText("A message has apeared on the wall.");
                            }
                        }
                        break;
                    case 6:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.placeFrame);
                            selectedItem.item.position = new Vector3(3.62f, 3.33f, 0);
                            Destroy(imageCheck.gameObject);
                            blueFlag = true;
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.inventoryImage);
                            selectedItem = null;
                            if (redFlag && blueFlag && yellowFlag)  {
                                jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                                lampOpen.position  += new Vector3(-10, 0, 0);
                                UIManager.Instance.UpdateText("A message has apeared on the wall.");
                            }
                        }
                        break;
                    case 7:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.placeFrame);
                            selectedItem.item.position = new Vector3(5.623f, 3.33f, 0);
                            Destroy(imageCheck.gameObject);
                            yellowFlag = true;
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.inventoryImage);
                            selectedItem = null;
                            if (redFlag && blueFlag && yellowFlag)  {
                                jealousy.transform.localPosition += new Vector3(-10, 0, 0);
                                lampOpen.position  += new Vector3(-10, 0, 0);
                                UIManager.Instance.UpdateText("A message has apeared on the wall.");
                            }
                        }
                        break;
                    case 8:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            audioManager.PlaySFX(audioManager.safeOpen);
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
            else if (selectedItem.itemID < 200)  {
                switch (selectedItem.itemID)  {
                    case 102:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            attemptItemAquisition(holdItem);
                            smallLockerOpen.position += new Vector3(10, 0, 0);
                            Destroy(imageCheck.gameObject);
                            audioManager.PlaySFX(audioManager.boxOpen);
                            //Play HEY!! sound effect here
                            holdItem = null;
                        }
                        break;
                    case 104:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.gameObject);
                            Destroy(cakeChocolate.inventoryImage.gameObject);
                            GameManager.CollectedItems.Remove(cakeChocolate);
                            Destroy(cakeChocolate.gameObject);
                            holdItem.item.position += new Vector3(10, 0, 0);
                            attemptItemAquisition(holdItem);
                            Destroy(imageCheck.gameObject);
                            audioManager.PlaySFX(audioManager.poison);
                            //Play HEY!! sound effect here
                            UIManager.Instance.UpdateText("Almost done. I just need to hide the poison somehow");
                            holdItem = null;
                        }
                        break;
                    case 105:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            GameManager.CollectedItems.Remove(selectedItem);
                            Destroy(selectedItem.gameObject);
                            Destroy(cakePoison.inventoryImage.gameObject);
                            GameManager.CollectedItems.Remove(cakePoison);
                            Destroy(cakePoison.gameObject);
                            holdItem.item.position += new Vector3(10, 0, 0);
                            attemptItemAquisition(holdItem);
                            Destroy(imageCheck.gameObject);
                            //Play HEY!! sound effect here
                            UIManager.Instance.UpdateText("Perfect. One cake coming right up!");
                            holdItem = null;
                        }
                        break;
                    case 108:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            if (blueFlag == false)  {
                                UIManager.Instance.UpdateText("That puddle is a safety hazard. I only have one chance at this and i don't want to slip and drop the cake.");
                            }
                            else  {
                                if (InputManager.Instance.getInventoryActive())  {
                                    InputManager.Instance.inventoryToggle();
                                }
                                GameManager.CollectedItems.Remove(selectedItem);
                                startDelayedAction(108);
                            }
                        }
                        break;
                    case 109:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            Destroy(tempObject.gameObject);
                            Destroy(imageCheck.gameObject);
                            audioManager.PlaySFX(audioManager.sweeping);
                            blueFlag = true;
                            holdItem = null;
                            tempObject = null;
                        }
                        break;
                }
            }
            else if (selectedItem.itemID < 300)  {
                switch (selectedItem.itemID)  {
                    case 201:
                        if (selectedItem.itemID == imageCheck.imageID)  {
                            Destroy(imageCheck.gameObject);
                            StartCoroutine(gameManager.MoveToPoint(tempObject, new Vector2(-5.461f, -1.951f), 15));
                            //tempObject.position = new Vector3(-5.461f, -1.951f, 0);
                            
                            tempImage.transform.localPosition = new Vector3(-5.461f, -1.951f, 0);
                        }
                        break;
                }
            }
        }
    }

    public void inpsectItemEvents(itemData item)  {
        if (!GameManager.CollectedItems.Contains(item))  {
            return;
        }
        if (InputManager.Instance.getInspectMode())  {
            if (item.itemID < 100)  {
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

                            /*UIManager.Instance.UpdateText("It reads: Dear brother, I am tired of seeing all the attention and admiration you always receive. I cannot compete with you anymore. I hate you!");
                            
                            UIManager.Instance.UpdateText("It reads: Dear brother, you attention seeking bastard, is it not enough you hoarded all our parents love, now you want to do the same for the rest of society as well! But soon, you will regret your every decision that led you here. I will make sure of it");*/
                            UIManager.Instance.UpdateText("It reads: Dear brother, the fascination you seem to garner from everyone around you remains incomprehensible to me. Despite all my numerous attempts and efforts, it seems to me, that so long as you are around, i will be forever doomed to be forgotten and discarded. A fate i refuse!");
                            UIManager.Instance.setSecondaryText(1);

                            break;
                    default:
                        UIManager.Instance.UpdateText("No comment");
                        break;
                }
            }
            else if (item.itemID < 200)  {
                switch (item.itemID)  {
                    case 102:
                        UIManager.Instance.UpdateText("It's a note with a series of numbers written on it");
                        break;
                }
            }
            else if (item.itemID < 300)  {
                switch (item.itemID)  {
                }
            }
        }
    }

    public void inpsectEnvironmentEvents(int envCode)  {
        if (InputManager.Instance.getInspectMode())  {
            if (envCode < 100)  {
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
            else if (envCode < 200)  {
                switch (envCode)  {
                    case 100:
                        UIManager.Instance.UpdateText("There is a sign on the side that reads: {BEWARE! Rat poison} . Unfortuantely it's locked");
                        break;
                    case 101:
                        UIManager.Instance.UpdateText("Look at that fatass eating. Well not for much longer");
                        break;
                }
            }
            else if (envCode < 300)  {
                switch (envCode)  {
                }
            }
        }
    }

    public void setSelectedItem(itemData item)  {
        selectedItem = item;
    }

    public itemData getSelectedItem()  {
        return(selectedItem);
    }

    public void attemptItemAquisition(itemData item)  {
        if (InputManager.Instance.getInspectMode())  {
            return;
        }
        if (item.itemID > 0 && (item.requiredItemID == -1 || GameManager.CollectedItems.Contains(item)))  {
            GameManager.CollectedItems.Add(item);
            Debug.Log("itemID " + item.itemID + " has been aquired!");
            Debug.Log("ALL Collected items ID: \n");
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                Debug.Log("|  " + collectedItem.itemID + "  |  ");
            }
            Debug.Log("DONE\n");
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
