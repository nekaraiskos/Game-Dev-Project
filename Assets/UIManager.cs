using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour  {
    public bool textActive = false;
    public Text text;
    public Image textAccept;
    public Transform textBox, boxOperative;
    public static UIManager Instance { get; private set; }

    private void Awake()  {
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

    void Start()  {
        if (text != null)  {
            text.text = "Game Started!";
        }
    }

    public void UpdateText(string message)  {
        if (text != null)  {
            text.text = message;
        }
        textBoxOpen();
    }
    public bool getTextAcive()  {
        return(textActive);
    }

    public void textBoxOpen()  {
        textActive = true;
        InputManager.Instance.setInspectMode(false);
        textBox.position += new Vector3(0, 4, 0);
        boxOperative.position += new Vector3(0, 4, 0);
        text.transform.localPosition += new Vector3(0, 4, 0);
        textAccept.transform.localPosition += new Vector3(0, 12, 0);
    }

    public void textBoxClose()  {
        textActive = false;
        textBox.position += new Vector3(0, -4, 0);
        boxOperative.position += new Vector3(0, -4, 0);
        text.transform.localPosition += new Vector3(0, -4, 0);
        textAccept.transform.localPosition += new Vector3(0, -12, 0);
    }
}
