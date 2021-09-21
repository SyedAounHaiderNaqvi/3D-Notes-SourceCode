using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UI_FollowMouse : MonoBehaviour
{
    public List<Transform> UIs;
    Canvas myCanvas;
	Object[] otherStrips;

	public GameObject radialPrefab;
	public Settings settings;

	public Transform blueStrip;
	public Vector3 stripProjectXYZ;
	public Vector3 stripSettingsXYZ;
	public float stripMoveSpeed = 4f;
	float fraction = 0f;

	private void Awake()
	{
        myCanvas = GetComponent<Canvas>();
		otherStrips = Resources.FindObjectsOfTypeAll(typeof(CanvasGroup));
		foreach (CanvasGroup go in otherStrips as CanvasGroup[])
		{
			UIs.Add(go.transform);
		}
		settings.ToggleBlurOnStart();
	}

	public void ExitGame() { Application.Quit(); }

	// Update is called once per frame
	void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
		for (int i = 0; i < UIs.Count; i++)
		{
			if (UIs[i] != null)
				UIs[i].position = myCanvas.transform.TransformPoint(pos);
			else
				UIs.RemoveAt(i);
		}
    }
	
	public void SpawnPopImage(Transform parent)
    {
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
		GameObject clone = Instantiate(radialPrefab, myCanvas.transform.TransformPoint(pos), Quaternion.identity, parent);
		clone.transform.SetSiblingIndex(0);
		Destroy(clone, 2f);
    }

	public void ClickedButtonAnimateStrip(bool TrueTomoveToSetting)
    {
		StopCoroutine(MoveStrip(false));
		StartCoroutine(MoveStrip(TrueTomoveToSetting));
    }

	IEnumerator MoveStrip(bool movedToSettings)
    {
		float yPos = blueStrip.localPosition.y;
		while (fraction < 1)
		{
			fraction += Time.deltaTime * stripMoveSpeed;
			fraction = Mathf.Pow(fraction, 1.01f);

			// go to project button
			if (!movedToSettings)
			{
				blueStrip.localPosition = Vector3.Lerp(blueStrip.localPosition, stripProjectXYZ, fraction);
				yield return null;

			}
			else
			{
				// go to settings
				blueStrip.localPosition = Vector3.Lerp(blueStrip.localPosition, stripSettingsXYZ, fraction);
				yield return null;
			}
		}
		blueStrip.localPosition = new Vector3(blueStrip.localPosition.x, yPos, blueStrip.localPosition.z);
		fraction = 0f;
    }
}
