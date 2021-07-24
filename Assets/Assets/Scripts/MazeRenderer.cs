using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
	private int width;

	private int height;

	[SerializeField] private float size = 1f;
    
	[SerializeField] private float scaleFix = 50f;

    [SerializeField] private Transform wallPrefab;

	[SerializeField] private Transform floorPrefab;

	[SerializeField] private Transform playerPrefab;

	private Vector3 playerPos;
    
	[SerializeField] private Transform chestPrefab;

	[SerializeField] private GameObject batchRoot;

    void Start()
    {
	    var gm = FindObjectOfType<GameManager>();
	    width = gm.width;
	    height = gm.height;
	    var maze = MazeGenerator.Generate(width, height);
	    Draw(maze);
    }

    private void Draw(WallState[,] maze)
	{
	
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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

                if (j == 0)
                {
                    if(cell.HasFlag(WallState.Down))
                    {
                        var topWall = Instantiate(wallPrefab, batchRoot.transform);
                        topWall.position = position + new Vector3(0, 0, -size/2);
                        var localScale = topWall.localScale;
	                    localScale = new Vector3(scaleFix, localScale.y, localScale.z);
                        topWall.localScale = localScale;
                    }
                }
            }
        }

        StaticBatchingUtility.Combine(batchRoot);
        
        GenerateLevelAssets();
	}


    private void GenerateLevelAssets()
	{
		float yCorrection = -0.5f;
		var floor = Instantiate(floorPrefab, transform);
		var pos = floor.position;
		pos = new Vector3(pos.x, -0.5f, pos.z);
		floor.position = pos;
		floor.localScale = new Vector3(width, 1, height);
		
		if (width % 2 == 0)
		{
			playerPos.x = -width/2;
		}
		else
		{
			playerPos.x = -width/2 + yCorrection;
		}

		if (height % 2 == 0)
		{
			playerPos.z = -height/2;
		}
		else
		{
			playerPos.z = -height/2 + yCorrection;
		}
		playerPos.y = yCorrection;

		var player = Instantiate(playerPrefab, transform);
		player.position = playerPos;
        
		var chest = Instantiate(chestPrefab, transform);
		chest.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		chest.position = new Vector3 (-playerPos.x - 1, yCorrection, -playerPos.z - 1);
		
	}
}
