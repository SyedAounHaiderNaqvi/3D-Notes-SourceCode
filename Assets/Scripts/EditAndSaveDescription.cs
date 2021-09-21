using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditAndSaveDescription : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject descriptionDialog;
    public FollowMouse camScript;
    public TMP_InputField descInputField;

    public Color activeColor;
    public Color inactiveColor;

    void Awake()
    {
        descriptionDialog = GameObject.Find("AskingForDesc");
        camScript = GameObject.Find("Main Camera").GetComponent<FollowMouse>();
        descInputField = GameObject.FindGameObjectWithTag("ifDESC").GetComponent<TMP_InputField>();
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
        descriptionDialog.SetActive(true);
        descriptionDialog.GetComponentInChildren<Button>().onClick.AddListener(ConfirmEdit);
        BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
        foreach (BoxCollider item in craps)
        {
            item.enabled = false;
        }
    }

    public void ConfirmEdit()
    {
        if (string.IsNullOrEmpty(descInputField.text))
        {
            descInputField.text = "Type a valid description...";
        }
        else
        {
            text.text = descInputField.text;
            descriptionDialog.SetActive(false);
            camScript.enabled = true;
            BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
            foreach (BoxCollider item in craps)
            {
                item.enabled = true;
            }
            descriptionDialog.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }
}
