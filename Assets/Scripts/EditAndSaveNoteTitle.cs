using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditAndSaveNoteTitle : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject titleDialog;
    public FollowMouse camScript;
    public TMP_InputField inputField;

    public Color activeColor;
    public Color inactiveColor;

    void Awake()
    {
        titleDialog = GameObject.Find("AskingForTitle");
        camScript = GameObject.Find("Main Camera").GetComponent<FollowMouse>();
        inputField = GameObject.FindGameObjectWithTag("ifTITLE").GetComponent<TMP_InputField>();
    }

    void OnMouseEnter()
    {
        text.color = activeColor;
    }

    void OnMouseExit()
    {
        text.color = inactiveColor;
    }

    void OnMouseDown()
    {
        camScript.enabled = false;
        inputField.text = "";
        inputField.ActivateInputField();
        titleDialog.SetActive(true);
        titleDialog.GetComponentInChildren<Button>().onClick.AddListener(ConfirmEdit);

        BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
        foreach (BoxCollider item in craps)
        {
            item.enabled = false;
        }
    }

    public void ConfirmEdit()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = "Default Title";
        }
        else
        {
            text.text = inputField.text;
            titleDialog.SetActive(false);
            camScript.enabled = true;
            BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
            foreach (BoxCollider item in craps)
            {
                item.enabled = true;
            }
            titleDialog.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }
}
