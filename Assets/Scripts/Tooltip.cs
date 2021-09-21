using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
	public GameObject tooltip;
	RectTransform backgroundRectTransform;
	TextMeshProUGUI txt;
	public Vector3 offset;

	private void Awake()
	{
		backgroundRectTransform = tooltip.transform.Find("border").GetComponent<RectTransform>();
		txt = tooltip.transform.Find("Text").GetComponent<TextMeshProUGUI>();
	}

	public void ShowTooltip(string tooltipString)
	{
		StartCoroutine(Show(tooltipString));
	}

	IEnumerator Show(string textToDisplay)
	{
		yield return new WaitForSecondsRealtime(0.78f);
		tooltip.transform.position = Input.mousePosition + offset;
		tooltip.SetActive(true);
		txt.text = textToDisplay;
		Vector2 bgSize = new Vector2(txt.preferredWidth + 12, txt.preferredHeight + 4);
		backgroundRectTransform.sizeDelta = bgSize;
	}

	public void HideTooltip()
	{
		StopAllCoroutines();
		tooltip.SetActive(false);
	}
}
