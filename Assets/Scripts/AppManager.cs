using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public FollowMouse mouse;

    public GameObject titledialog;
    public GameObject descdialog;
    public GameObject blind;

    public DynamicDepthOfField dynamicDepth;
    public PostProcessVolume volume;
    DepthOfField dof;
    public string folderChoice = "DarkNotes";
    public string tagChoice = "Dark";
    string project;

    public GameObject shadowsIndicator;
    public GameObject dofIndicator;
    public GameObject shadowsIndicatorstrip;
    public GameObject dofIndicatorstrip;

    public Image sideBarBG;
    public Material blurMat;
    public PostProcessLayer layer;

    private void Awake()
    {
        project = PlayerPrefs.GetString("Project");
        volume.profile.TryGetSettings(out dof);
        if (!Directory.Exists($"{Application.persistentDataPath}/{project}/{folderChoice}"))
            Directory.CreateDirectory($"{Application.persistentDataPath}/{project}/{folderChoice}");
        LoadAllNotesFromPrevSessionIfAny();

        // Removes existing notes, so they don't interfere later
        string[] files = Directory.GetFiles($"{Application.persistentDataPath}/{project}/{folderChoice}");
		foreach (string file in files)
		{
            File.Delete(file);
		}
        if (Convert.ToInt16(SystemInfo.graphicsMemorySize) < 1400)
        {
            // The GPU is not good so disable blurring by default
            sideBarBG.material = null;
        }

        else
        {
            if (PlayerPrefs.GetInt("Blur", 1).Equals(1))
            {
                sideBarBG.material = blurMat;
            }
            else
            {
                sideBarBG.material = null;
            }
        }

        layer.enabled = false;
        Invoke("tt", 0.1f);
    }


    void tt()
    {
        layer.enabled = true;
    }

    void LoadAllNotesFromPrevSessionIfAny()
	{
        string[] files = Directory.GetFiles($"{Application.persistentDataPath}/{project}/{folderChoice}");
		for (int i = 0; i < files.Length; i++)
		{
            GameObject loadedCard = ScriptedAddCard();
            loadedCard.GetComponent<DragMessages>().LoadNote(files[i]);
            loadedCard.name = UnityEngine.Random.Range(-9999999, 999999).ToString();
		}
	}

	private void OnApplicationQuit()
	{
        GameObject[] allNotes = GameObject.FindGameObjectsWithTag(tagChoice);
		foreach (GameObject note in allNotes)
		{
            note.GetComponent<DragMessages>().SaveNote();
		}
        PlayerPrefs.DeleteKey("Project");
	}


	public static bool areShadowsVisible = true;
    public static bool isFocusOn = false;
    public Light mainLight;

    float z = -0.4f;
    Vector3 pos = Vector3.zero;

    public void MouseEntersUI()
	{
        BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
        foreach (BoxCollider item in craps)
        {
            item.enabled = false;
        }
    }

    public void EnableShadowsOrDisable()
	{
        if (areShadowsVisible)
		{
            mainLight.shadows = LightShadows.None;
            shadowsIndicator.SetActive(false);
            shadowsIndicatorstrip.SetActive(false);
            areShadowsVisible = false;
		} else
		{
            mainLight.shadows = LightShadows.Hard;
            shadowsIndicator.SetActive(true);
            shadowsIndicatorstrip.SetActive(true);
            areShadowsVisible = true;
		}
	}

    public void EnableDOFOrDisable()
    {
        if (isFocusOn)
        {
            dynamicDepth.enabled = false;
            dofIndicator.SetActive(false);
            dofIndicatorstrip.SetActive(false);
            dof.active = false;
            isFocusOn = false;
        }
        else
        {
            dynamicDepth.enabled = true;
            dofIndicator.SetActive(true);
            dofIndicatorstrip.SetActive(true);
            dof.active = true;
            isFocusOn = true;
        }
    }

    public void MouseExitsUI()
	{
        BoxCollider[] craps = Resources.FindObjectsOfTypeAll<BoxCollider>();
        foreach (BoxCollider item in craps)
        {
            item.enabled = true;
        }
    }

    public void AddCardToScene()
    {
        titledialog.SetActive(true);
        descdialog.SetActive(true);
        blind.SetActive(true);
        pos.z = z;
        GameObject newCard = Instantiate(cardPrefab, pos, Quaternion.identity);
        z += -0.02f;
        newCard.name += Time.deltaTime.ToString();
        mouse.pos = new Vector3(0, 0, mouse.pos.z);
        DragMessages script = newCard.GetComponent<DragMessages>();
        if (!script.isDark)
            script.colorStrip.material.color = UnityEngine.Random.ColorHSV(0, 1, 0.3f, 1, 0.3f, 1);
        else
            script.colorStrip.material.color = UnityEngine.Random.ColorHSV(0, 1, 0.3f, 0.6f, 0.9f, 1);

        Invoke("ActualAdd", 0.08f);
    }

    public GameObject ScriptedAddCard()
	{
        titledialog.SetActive(true);
        descdialog.SetActive(true);
        blind.SetActive(true);
        pos.z = z;
        GameObject newCard = Instantiate(cardPrefab, pos, Quaternion.identity);
        z += -0.02f;
        newCard.name += Time.deltaTime.ToString();
        mouse.pos = new Vector3(0, 0, mouse.pos.z);
        DragMessages script = newCard.GetComponent<DragMessages>();
        if (!script.isDark)
            script.colorStrip.material.color = UnityEngine.Random.ColorHSV(0, 1, 0.3f, 1, 0.3f, 1);
        else
            script.colorStrip.material.color = UnityEngine.Random.ColorHSV(0, 1, 0.3f, 0.6f, 0.9f, 1);

        Invoke("ActualAdd", 0.08f);
        return newCard;
    }

    void ActualAdd()
    {
        titledialog.SetActive(false);
        descdialog.SetActive(false);
        blind.SetActive(false);
    }

    public void GoHome()
	{
        OnApplicationQuit();
        SceneManager.LoadScene(0);
	}

    public void EnlargeStrip(Animator strip)
	{
        strip.CrossFadeInFixedTime("stripenlarge", 0.01f);
	}

    public void ShrinkStrip(Animator strip)
	{
        strip.CrossFadeInFixedTime("stripshrink", 0.01f);
    }
}
