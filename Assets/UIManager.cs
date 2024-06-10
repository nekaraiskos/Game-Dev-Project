using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour  {
    public bool textActive = false;
    public int secondaryText = 0;
    public Text text, secondText;
    public Image textAccept, secondTextAccept;
    public Transform textBox, boxOperative, secondOperative, secondTextBox;
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

    /*void Start()  {
        if (text != null)  {
            text.text = "Game Started!";
        }
    }*/

    public void UpdateText(string message)  {
        if (text != null)  {
            text.text = message;
        }
        textBoxOpen();
    }
    public bool getTextAcive()  {
        return(textActive);
    }

    public void setSecondaryText(int setTo)  {
        secondaryText = setTo;
    }

    public void setBoxOperative(Transform newOp)  {
        newOp.position += new Vector3(0, 10, 0);
        boxOperative = newOp;
        //UpdateText("Much better. Now back to muredering");
    }

    public void secondTextBoxOpen()  {
        InputManager.Instance.setInspectMode(false);
        secondTextBox.position = new Vector3(0, -3.9927f, 0);
        secondOperative.position = new Vector3(-5.787f, -4.033f, 0);
        secondText.transform.localPosition = new Vector3(1.52f, -3.98f, 0);
        secondTextAccept.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void secondTextBoxClose()  {
        InputManager.Instance.setInspectMode(false);
        secondTextBox.position += new Vector3(0, -15, 0);
        secondOperative.position += new Vector3(0, -15, 0);
        secondText.transform.localPosition += new Vector3(0, -15, 0);
        secondTextAccept.transform.localPosition += new Vector3(0, -12, 0);
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
        if (secondaryText == 1)  {
            UpdateText("\"Judging by this note, it seems likely the cause of this murder was one brother's jealousy over the other's success. Probably an inferiority complex, or perhaps delusions of grandeur.\"");
        }
        textActive = false;
        textBox.position += new Vector3(0, -4, 0);
        boxOperative.position += new Vector3(0, -4, 0);
        text.transform.localPosition += new Vector3(0, -4, 0);
        textAccept.transform.localPosition += new Vector3(0, -12, 0);
    }
}
