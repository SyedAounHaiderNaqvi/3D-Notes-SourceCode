using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragMessages : MonoBehaviour
{
    Vector3 screenSpace;
    Vector3 offset;

    public MeshRenderer colorStrip;
    public ParticleSystem shredParticleSystem;

    public bool isDark = false;
    public string folderChoice = "DarkNotes";
    string projectName;

	private void Awake()
	{
        shredParticleSystem = Resources.FindObjectsOfTypeAll<ParticleSystem>()[0];
        projectName = PlayerPrefs.GetString("Project");
	}

    void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    public void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        if (curPosition.z >= -0.001f)
            curPosition.z = -0.002f;
        
        transform.position = curPosition;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isDark)
                colorStrip.material.color = Random.ColorHSV(0, 1, 0.3f, 1, 0.3f, 1);
            else
                colorStrip.material.color = Random.ColorHSV(0, 1, 0.3f, 0.6f, 0.9f, 1);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
		{
            // Remove card from scene
            ParticleSystem s = Instantiate(shredParticleSystem, transform.position, Quaternion.identity);
            s.transform.eulerAngles = new Vector3(90, s.transform.eulerAngles.y, s.transform.eulerAngles.z);
            s.Play();
            Destroy(gameObject);
		}
    }

    public void SaveNote()
	{
        SaveLoadManager.SaveNoteData(this, gameObject.name, folderChoice, projectName);
	}

    public void LoadNote(string noteToLoad)
	{
        NoteData loadedData = SaveLoadManager.LoadNoteData(noteToLoad);
        transform.position = new Vector3(loadedData.worldSpacePosition[0], loadedData.worldSpacePosition[1], loadedData.worldSpacePosition[2]);
        colorStrip.material.color = new Color(loadedData.stripColor[0], loadedData.stripColor[1], loadedData.stripColor[2]);
        transform.Find("title").GetComponent<TextMeshPro>().text = loadedData.titleText;
        transform.Find("body").GetComponent<TextMeshPro>().text = loadedData.descriptionText;
	}
}
