using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class GameManager : MonoBehaviour
	{
		public int width;

		public int height;
	
		[SerializeField] private GameObject loadScreen;

		public int levelCount = 0;
		public int currentLevel = 0;

		private static GameManager instance;

		public bool isCustomMaze;
		
		[SerializeField] private LayerMask gridObstacles;

		[SerializeField] private GameObject AI;

		public bool showPathCoolDown = true;

		[SerializeField] private float showPathCoolDownTime;
		
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

			RenderSettings.ambientMode = AmbientMode.Flat;
			RenderSettings.ambientLight = new Color(0.3686275f, 0.3686275f, 0.3686275f, 1f);
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

		public IEnumerator PathFinding()
		{
			AstarData data = AstarPath.active.data;

			// This creates a Grid Graph
			GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

			// Setup a grid graph with some values
			float nodeSize = 0.2f;

			var center = new Vector3();

			if (height % 2 == 0)
			{
				center.z = -0.5f;
			}
			else
			{
				center.z = 0;
			}

			if (width % 2 == 0)
			{
				center.x = -0.5f;
			}
			else
			{
				center.x = 0;
			}

			center.y = -0.5f;
			
			gg.center = center;

			// Updates internal size from the above values
			gg.SetDimensions(width * 5, height * 5, nodeSize);

			gg.collision.heightMask = gg.collision.heightMask ^ (1 << LayerMask.NameToLayer("Obstacles"));

			gg.collision.mask = gridObstacles;

			// Scans all graphs
			AstarPath.active.Scan();

			var player = GameObject.FindWithTag("Player");
			
			Instantiate(AI, player.transform.position, Quaternion.identity);

			showPathCoolDown = false;

			yield return new WaitForSeconds(showPathCoolDownTime);
			
			showPathCoolDown = true;

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
