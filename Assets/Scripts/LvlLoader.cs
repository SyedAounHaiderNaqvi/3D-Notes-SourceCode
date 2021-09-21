using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LvlLoader : MonoBehaviour
{
	public GameObject loadingScr;
	public ParticleSystem system;
	public GameObject projectPrefab;
	public Transform content;

	public GameObject hider;

	[Header("Creating a project manually")]
	public TMP_InputField titleField;
	public Toggle darkModeToggle;
	public GameObject createScreen;

	public UI_FollowMouse followMouse;

	private void Awake()
	{
		// Check for existing projects, if they are there, include them in the content
		string[] pathsToProjects = Directory.GetDirectories(Application.persistentDataPath);
		if (pathsToProjects.Length > 0)
		{
			foreach (string pth in pathsToProjects)
			{
				// Add to content
				GameObject prefab = Instantiate(projectPrefab, content);
				followMouse.UIs.Add(prefab.transform.Find("mousefollow (1)"));
				Project p = prefab.GetComponent<Project>();
				p.StartThis(Path.GetFileName(pth.Replace('\\', '/')));
			}
		}
	}

	public void CreateProject()
	{
		if (!string.IsNullOrEmpty(titleField.text))
		{
			GameObject prefab = Instantiate(projectPrefab, content);
			int choiceOfMode = darkModeToggle.isOn ? 1 : 0;
			prefab.transform.Find("t").GetComponent<TextMeshProUGUI>().text = choiceOfMode + titleField.text;
			Project p = prefab.GetComponent<Project>();
			p.title = choiceOfMode.ToString() + titleField.text;
			followMouse.UIs.Add(prefab.transform.Find("mousefollow (1)"));
			p.StartThis(p.title);
			titleField.text = "";
			darkModeToggle.isOn = false;
			createScreen.SetActive(false);
		}
	}

	public void LoadLevel()
	{
		string theName = GameObject.FindGameObjectWithTag("GameController").transform.parent.gameObject.GetComponent<Project>().title;
		int index = theName[0] == '1' ? 2 : 1;
		StartCoroutine(Load(index, theName));
	}

	IEnumerator Load(int i, string theName)
	{
		PlayerPrefs.SetString("Project", theName);
		AsyncOperation operation = SceneManager.LoadSceneAsync(i);
		loadingScr.SetActive(true);
		system.Play();
		while (!operation.isDone)
		{
			yield return null;
		}
	}

	public void DeleteProject()
	{
		// destroy folder
		string[] subFolders = Directory.GetDirectories(GameObject.FindGameObjectWithTag("GameController").transform.parent.GetComponent<Project>().projectPath);
		foreach (string item in subFolders)
		{
			foreach (string file in Directory.GetFiles(item))
			{
				if (file != null)
					File.Delete(file);
			}
			Directory.Delete(item);
		}
		Directory.Delete(GameObject.FindGameObjectWithTag("GameController").transform.parent.GetComponent<Project>().projectPath, true);
		//panelTitle.text = "";
		Destroy(GameObject.FindGameObjectWithTag("GameController").transform.parent.gameObject);
		hider.SetActive(true);
	}
}
