using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public bool inspectMode = false, inventoryActive;
    public Transform Bar;
    GameManager gameManager;
    ClickManager clickManager;
    AudioManager audioManager;
    private bool inputMode= false; // Flag to control player input
    private string userInput = "", correctInput = "426"; // String variable to store user input
    public static InputManager Instance { get; private set; }

    private void Awake()  {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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


    void Update()  {
        if (inputMode)  {
            Debug.Log("lets go:");
            // Allow the player to input the string
            if (Input.anyKeyDown)  {
                // Append the input character to the userInput string
                userInput += Input.inputString;
                Debug.Log("INPUT: " + userInput);
            }
            if (userInput.Length == 3)  {
                if (userInput == correctInput)  {
                    Debug.Log("SUCCESS " + userInput);
                    inputMode = false;
                }
                else  {
                    inputMode = false;
                    return;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))  {
            inventoryToggle();
        }
        if (Input.GetKeyDown(KeyCode.I))  {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("I was pressed!");
            if (UIManager.Instance.getTextAcive())  {
                return;
            }
            if (inspectMode)  {
                inspectMode = false;
            }
            else  {
                audioManager.PlaySFX(audioManager.inspect);
                inspectMode = true;
            }
            ClickManager.Instance.setSelectedItem(null);
        }
    }

    public void checkInput(Image image)  {
        if (userInput == correctInput)  {
            Destroy(image.gameObject);
        }
    }

    public bool getInventoryActive()  {
        return(inventoryActive);
    }
    public void setInspectMode(bool set)  {
        inspectMode = set;
    }

    public void setInputMode(bool set)  {
        if (userInput != correctInput)  {
            userInput = "";
            inputMode = set;
            UIManager.Instance.UpdateText("Let's see what could the code be...");
        }
    }

    public bool getInspectMode()  {
        return(inspectMode);
    }

    public void inventoryToggle()  {
        if (inventoryActive == true)  {
            audioManager.PlaySFX(audioManager.invClose);
            Bar.position += new Vector3(0, 2, 0);
            inventoryActive = false;
            foreach (itemData collectedItem in GameManager.CollectedItems)  {
                collectedItem.item.position += new Vector3(0, 2, 0);
                collectedItem.inventoryImage.gameObject.SetActive(false);
            }
        }
        else  {
            audioManager.PlaySFX(audioManager.invOpen);
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