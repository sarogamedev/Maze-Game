using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Range(1, 1000)] public int width;

	[Range(1, 1000)] public int height;
	
	[SerializeField] private GameObject loadScreen;

	public int levelCount = 0;
	public int currentLevel = 0;

	public static GameManager instance;

	
	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		Application.targetFrameRate = 60;
		
		if(File.Exists(SaveSystem.path))
		{
			SaveData data = SaveSystem.LoadGame();
			levelCount = data.level;
		}

		
		instance = this;
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			Debug.Log("Save Data deleted! Restart into play mode to take effect");
			SaveSystem.DebugDeleteSaveData();
		}
	}
	List <AsyncOperation> scenesLoading = new List <AsyncOperation>();

	public void GenerateNewMaze(int wid, int ht)
	{
		loadScreen.SetActive(true);

		width = wid;
		height = ht;
		
		if(SceneManager.GetSceneByName("MainMenu").isLoaded)
		{
			scenesLoading.Add (SceneManager.UnloadSceneAsync("MainMenu"));
		}
		
		if(SceneManager.GetSceneByName("MainScene").isLoaded)
		{
			scenesLoading.Add (SceneManager.UnloadSceneAsync("MainScene"));
		}
		
		scenesLoading.Add (SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive));

		StartCoroutine(GetSceneLoadProgress());
	}
    
	private IEnumerator GetSceneLoadProgress()
	{
		for (int i = 0; i < scenesLoading.Count; i++)
		{
			while(!scenesLoading[i].isDone)
			{
				yield return null;
			}
		}
		loadScreen.SetActive(false);
	}

	public void LoadMainMenu()
	{
		SceneManager.UnloadSceneAsync("MainScene");
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
	}

    public void ExitApplication()
    {
        Application.Quit();
    }
}
