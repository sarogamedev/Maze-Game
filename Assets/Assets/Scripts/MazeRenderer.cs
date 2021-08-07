using System;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts
{
	public class MazeRenderer : MonoBehaviour
	{
		private int width;

		private int height;

		[SerializeField] private float size = 1f;
    
		[SerializeField] private float scaleFix = 50f;

		[SerializeField] private float mapCamSize;

		[SerializeField] private Transform wallPrefab;

		[SerializeField] private Transform floorPrefab;

		[SerializeField] private Transform playerPrefab;

		private Vector3 playerPos;
    
		[SerializeField] private Transform chestPrefab;

		[SerializeField] private Transform mapCam;

		[SerializeField] private GameObject batchRoot;
		
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
			
			Grid();
		}

		
		
		private void Draw(WallState[,] maze)
		{
	
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var cell = maze[i, j];
					var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

					if (cell.HasFlag(WallState.Up))
					{
						var topWall = Instantiate(wallPrefab, batchRoot.transform);
						topWall.position = position + new Vector3(0, 0, size/2);
						var localScale = topWall.localScale;
						localScale = new Vector3(scaleFix, localScale.y, localScale.z);
						topWall.localScale = localScale;
					}

					if (cell.HasFlag(WallState.Left))
					{
						var leftWall = Instantiate(wallPrefab, batchRoot.transform);
						leftWall.position = position + new Vector3(-size / 2, 0, 0);
						leftWall.eulerAngles = new Vector3(0, 0, 90);
						var localScale = leftWall.localScale;
						localScale = new Vector3(scaleFix, localScale.y, localScale.z);
						leftWall.localScale = localScale;
					}

					if (i == width - 1)
					{
						if (cell.HasFlag(WallState.Right))
						{
							var rightWall = Instantiate(wallPrefab, batchRoot.transform);
							rightWall.position = position + new Vector3(size / 2, 0, 0);
							rightWall.eulerAngles = new Vector3(0, 0, 90);
							var localScale = rightWall.localScale;
							localScale = new Vector3(scaleFix, localScale.y, localScale.z);
							rightWall.localScale = localScale;
						}
					}

					if (j != 0) continue;
					{
						if (!cell.HasFlag(WallState.Down)) continue;
						var topWall = Instantiate(wallPrefab, batchRoot.transform);
						topWall.position = position + new Vector3(0, 0, -size/2);
						var localScale = topWall.localScale;
						localScale = new Vector3(scaleFix, localScale.y, localScale.z);
						topWall.localScale = localScale;
					}
				}
			}

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
		
			playerPos.x = -width/2;
			
			playerPos.z = -height/2;
			
			playerPos.y = yCorrection;

			var player = Instantiate(playerPrefab, transform);
			player.position = playerPos;
        
			var chest = Instantiate(chestPrefab, transform);
			chest.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

			var chestPosition = new Vector3();
			var mapPosition = new Vector3();
			
			if (width % 2 == 0)
			{
				chestPosition.x = -playerPos.x - 1;
				mapPosition.x = -0.5f;
			}
			else
			{
				chestPosition.x = -playerPos.x;
			}
			if(height % 2 == 0)
			{
				chestPosition.z = -playerPos.z - 1;
				mapPosition.z = -0.5f;
			}
			else
			{
				chestPosition.z = -playerPos.z;
			}

			chestPosition.y = yCorrection;

			mapPosition.y = 5f;
			mapCam.position = mapPosition;
			mapCam.GetComponent<Camera>().orthographicSize = Mathf.Max(width, height) * mapCamSize;
			
			chest.position = chestPosition;
			
		}

		private void Grid()
		{
			

		}
	}
}
