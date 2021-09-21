using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class counter : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Transform content;

	// Update is called once per frame
	void Update()
    {
        txt.text = $"{content.childCount} in total";
    }
}
