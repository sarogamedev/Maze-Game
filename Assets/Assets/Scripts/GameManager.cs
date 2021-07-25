using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class GameManager : MonoBehaviour
	{
		[Range(1, 50)] public int width;

		[Range(1, 50)] public int height;
	
		[SerializeField] private GameObject loadScreen;

		public int levelCount = 0;
		public int currentLevel = 0;

		private static GameManager instance;
		
		private void Awake()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		
			if(File.Exists(SaveSystem.path))
			{
				var data = SaveSystem.LoadGame();
				levelCount = data.level;
			}

		
			instance = this;
			SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
		}

		private readonly List <AsyncOperation> scenesLoading = new List <AsyncOperation>();

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

		public static void LoadMainMenu()
		{
			SceneManager.UnloadSceneAsync("MainScene");
			SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
		}

		public static void ExitApplication()
		{
			Application.Quit();
		}
	}
}
