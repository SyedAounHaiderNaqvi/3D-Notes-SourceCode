using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Project : MonoBehaviour
{
	public string title;
	
	public string projectPath;
	GameObject hider;
	public UI_FollowMouse script;

	private void Awake()
	{
		hider = GameObject.FindGameObjectWithTag("Player");
		script = FindObjectOfType<UI_FollowMouse>();
		script.UIs.Add(transform.Find("ripple"));

		AddEventTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerEnter, ShowLocation);
		AddEventTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerExit, HideLocation);
	}

    void ShowLocation(BaseEventData data)
    {
        transform.Find("ripple").gameObject.SetActive(true);
    }

    void HideLocation(BaseEventData data)
    {
        transform.Find("ripple").gameObject.SetActive(false);
    }

    public static void AddEventTriggerListener(EventTrigger trigger,
                                            EventTriggerType eventType,
                                            System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }

    public void StartThis(string folderName)
	{
		title = folderName;
		if (Directory.Exists($"{Application.persistentDataPath}/{title}"))
		{
			projectPath = $"{Application.persistentDataPath}/{title}";
			transform.Find("t").GetComponent<TextMeshProUGUI>().text = folderName.Remove(0,1);
			transform.Find("location").GetComponent<TextMeshProUGUI>().text = $"{Application.persistentDataPath}/{folderName.Remove(0, 1)}";
		}
		else
		{
			Directory.CreateDirectory($"{Application.persistentDataPath}/{title}");
			projectPath = $"{Application.persistentDataPath}/{title}";
			transform.Find("t").GetComponent<TextMeshProUGUI>().text = folderName.Remove(0,1);
			transform.Find("location").GetComponent<TextMeshProUGUI>().text = $"{Application.persistentDataPath}/{folderName.Remove(0, 1)}";
		}
	}

	public void DisplayInfoOnMainPanel()
	{
		script = FindObjectOfType<UI_FollowMouse>();
		foreach (GameObject strip in GameObject.FindGameObjectsWithTag("GameController"))
		{
			strip.SetActive(false);
		}
		transform.Find("strip").gameObject.SetActive(true);
		script.SpawnPopImage(transform);
		hider.SetActive(false);
	}
}
