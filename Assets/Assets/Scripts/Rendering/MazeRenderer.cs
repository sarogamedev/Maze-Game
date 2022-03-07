using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public class MazeRenderer : MonoBehaviour
	{
		private int width;
		private int height;
		private Vector3 playerPos;

		[SerializeField] private float size;
		[SerializeField] private float scaleFix;
		[SerializeField] private float mapCamSize;
		[SerializeField, Range(0, 10)] private int doorProbability;
		[SerializeField] private float groundMaterialTilingConstant;

		[SerializeField] private Transform wallPrefab;
		[SerializeField] private Transform floorPrefab;
		[SerializeField] private Transform playerPrefab;
		[SerializeField] private Transform doorPrefab;
		[SerializeField] private Transform chestPrefab;
		[SerializeField] private Transform mapCam;
		
		[SerializeField] private GameObject batchRoot;
		[SerializeField] private GameObject doorRoot;

		[SerializeField] private Material groundMaterial;
		
		private GameManager gm;

		void Awake()
		{
			gm = FindObjectOfType<GameManager>();
			width = gm.width;
			height = gm.height;
			var maze = MazeGenerator.Generate(width, height);
			Draw(maze);
		}

		private void Start()
		{
			if (gm.isCustomMaze)
			{
				var ui = FindObjectOfType<UI>();

				ui.showMap.isOn = gm.showMapCustom;
				ui.showPath.isOn = gm.showPathCustom;
				ui.showPathButton.SetActive(gm.showPathCustom);
				ui.map.SetActive(gm.showMapCustom);
			}
		}
		private void Draw(WallState[,] maze)
		{
	
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var cell = maze[i, j];
					var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);
					var random = Random.Range(0, doorProbability);
					
					if (cell.HasFlag(WallState.Up))
					{
						if (random < doorProbability - 1 || j == height - 1)
						{
							var topWall = Instantiate(wallPrefab, batchRoot.transform);
							topWall.position = position + new Vector3(0, 0, size);
							var localScale = topWall.localScale;
							localScale = new Vector3(scaleFix, localScale.y, localScale.z + 0.01f);
							topWall.localScale = localScale;
						}
						else if(random >= doorProbability - 1 && j < height - 1)
						{
							var topWall = Instantiate(doorPrefab, doorRoot.transform);
							topWall.position = position + new Vector3(size , -size, size + 0.05f);
						}
					}

					if (cell.HasFlag(WallState.Left))
					{
						if (random < doorProbability - 1 || i == 0)
						{
							var leftWall = Instantiate(wallPrefab, batchRoot.transform);
							leftWall.position = position + new Vector3(-size, 0, 0);
							leftWall.eulerAngles = new Vector3(0, 0, 90);
							var localScale = leftWall.localScale;
							localScale = new Vector3(scaleFix + 0.005f, localScale.y, localScale.z);
							leftWall.localScale = localScale;
						}
						else if(random >= doorProbability - 1)
						{
							var leftWall = Instantiate(doorPrefab, doorRoot.transform);
							leftWall.position = position + new Vector3(-size + 0.05f, -size, -size);
							leftWall.eulerAngles = new Vector3(0, 90, 0);
						}
					}

					if (i == width - 1)
					{
						if (cell.HasFlag(WallState.Right))
						{
							var rightWall = Instantiate(wallPrefab, batchRoot.transform);
							rightWall.position = position + new Vector3(size, 0, 0);
							rightWall.eulerAngles = new Vector3(0, 0, 90);
							var localScale = rightWall.localScale;
							localScale = new Vector3(scaleFix + 0.005f, localScale.y, localScale.z);
							rightWall.localScale = localScale;
						}
					}

					if (j != 0) continue;
					{
						if (!cell.HasFlag(WallState.Down)) continue;
						var topWall = Instantiate(wallPrefab, batchRoot.transform);
						topWall.position = position + new Vector3(0, 0, -size);
						var localScale = topWall.localScale;
						localScale = new Vector3(scaleFix, localScale.y, localScale.z + 0.01f);
						topWall.localScale = localScale;
					}
				}
			}
			
			//batchRoot.GetComponent<CombineMeshes>().Combine();
			StaticBatchingUtility.Combine(batchRoot);
			GenerateLevelAssets();
		}
		
		private void GenerateLevelAssets()
		{
			const float yCorrection = -0.5f;
			var floor = Instantiate(floorPrefab, transform);
			var pos = floor.position;
			pos = new Vector3(pos.x, -0.5f, pos.z);
			floor.position = pos;
			floor.localScale = new Vector3(width, 1, height);

			var max = math.max(width, height);
			groundMaterial.mainTextureScale = new Vector2(max * groundMaterialTilingConstant, max * groundMaterialTilingConstant);
			
			playerPos.x = -width/2;
			playerPos.z = -height/2;
			playerPos.y = yCorrection;

			var player = Instantiate(playerPrefab, transform);
			player.position = playerPos;
        
			var chest = Instantiate(chestPrefab, transform);
			chest.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			
			var chestPosition1 = new Vector3();
			var chestPosition2 = new Vector3();
			var chestPosition3 = new Vector3();
			var mapPosition = new Vector3();
			
			if (width % 2 == 0)
			{
				chestPosition1.x = -playerPos.x - 1;
				chestPosition2.x = playerPos.x;
				chestPosition3.x = -playerPos.x - 1;
				mapPosition.x = -0.5f;
			}
			else
			{
				chestPosition1.x = -playerPos.x;
				chestPosition2.x = playerPos.x;
				chestPosition3.x = -playerPos.x;
			}
			if(height % 2 == 0)
			{
				chestPosition1.z = -playerPos.z - 1;
				chestPosition2.z = -playerPos.z - 1;
				chestPosition3.z = playerPos.z;
				mapPosition.z = -0.5f;
			}
			else
			{
				chestPosition1.z = -playerPos.z;
				chestPosition2.z = -playerPos.z;
				chestPosition3.z = playerPos.z;
			}

			chestPosition1.y = yCorrection;
			chestPosition2.y = yCorrection;
			chestPosition3.y = yCorrection;

			Vector3[] chestPositions = {chestPosition1, chestPosition2, chestPosition3};
			
			chest.position = chestPositions[Random.Range(0, chestPositions.Length)];
			
			mapPosition.y = 5f;
			mapCam.position = mapPosition;
			mapCam.GetComponent<Camera>().orthographicSize = Mathf.Max(width, height) * mapCamSize;
			
		}
	}
}
