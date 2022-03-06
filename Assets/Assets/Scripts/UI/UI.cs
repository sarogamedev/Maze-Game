using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class UI : MonoBehaviour
	{
		private GameManager gm;
    
		[SerializeField] private GameObject settingsMenu;
		[SerializeField] private GameObject levelSelectMenu;
		[SerializeField] private GameObject pauseMenu;
		[SerializeField] private GameObject levels1;
		[SerializeField] private GameObject levels2;
		[SerializeField] private GameObject nextButton;
		[SerializeField] private GameObject previousButton;
		[SerializeField] private GameObject customMazeMenu;
		[SerializeField] private TMP_InputField customText;
		[SerializeField] private MazeRenderer mr;

		public GameObject joyStick;
		public GameObject levelComplete;
		public GameObject II;
		public GameObject showPathButton;
		public Toggle showPath;
		public Toggle showMap;
		public GameObject map;
		public GameObject nextLevelButton;
	
		private int Level4x4 = 4;
		private int Level6x6 = 6;
		private int Level8x8 = 8;
		private int Level10x10 = 10;
		private int Level12x12 = 12;
		private int Level15x15 = 15;

		public bool checkInput;
		private int rows;
		private int columns;

		private void Start()
		{
			gm = FindObjectOfType<GameManager>();

			mr = FindObjectOfType<MazeRenderer>();
			if (gm == null)
			{
				Debug.LogError("There is no game manager in the scene");
			}
		}

		public void CustomSettingsToggle()
		{
			if(!gm.isCustomMaze) return;
			gm.showMapCustom = showMap.isOn;
			gm.showPathCustom = showPath.isOn;
		}

		public void ExitButton()
		{
			GameManager.ExitApplication();
		}

		public void MainMenu()
		{
			Time.timeScale = 1f;
			gm.LoadMainMenu();
		}
	
		public void NextLevel()
		{	
			if(gm.currentLevel == 0)
			{
				Two();
			}
			else if(gm.currentLevel == 1)
			{
				Three();
			}
			else if(gm.currentLevel == 2)
			{
				Four();
			}
			else if(gm.currentLevel == 3)
			{
				Five();
			}
			else if(gm.currentLevel == 4)
			{
				Six();
			}
			else if(gm.currentLevel == 5)
			{
				Seven();
			}
			else if(gm.currentLevel == 6)
			{
				Eight();
			}
			else if(gm.currentLevel == 7)
			{
				Nine();
			}
			else if(gm.currentLevel == 8)
			{
				Ten();
			}
			else if(gm.currentLevel == 9)
			{
				Eleven();
			}
			else if(gm.currentLevel == 10)
			{
				Twelve();
			}
			else if(gm.currentLevel == 11)
			{
				Thirteen();
			}
			else if(gm.currentLevel == 12)
			{
				Fourteen();
			}
			else if(gm.currentLevel == 13)
			{
				Fifteen();
			}
			else if(gm.currentLevel == 14)
			{
				Sixteen();
			}
			else if(gm.currentLevel == 15)
			{
				Seventeen();
			}
			else if(gm.currentLevel == 16)
			{
				Eighteen();
			}
			else if(gm.currentLevel == 17)
			{
				Nineteen();
			}
			else if(gm.currentLevel == 18)
			{
				Twenty();
			}
			else if(gm.currentLevel == 19)
			{
				Twentyone();
			}
			else if(gm.currentLevel == 20)
			{
				TwentyTwo();
			}
			else if(gm.currentLevel == 21)
			{
				TwentyThree();
			}
			else if(gm.currentLevel == 22)
			{
				TwentyFour();
			}
			else if(gm.currentLevel == 23)
			{
				gm.LoadMainMenu();
			}
	
		}

		public void InteractLevel()
		{
			if(gm.levelCount < 12)
			{
				for (int i = 0; i <= gm.levelCount; i++)
				{
					levels1.transform.GetChild(i).GetComponent<Button>().interactable = true;
				}
			}
			if(gm.levelCount >= 12)
			{
				for (int i = 0; i <= 11; i++)
				{
					levels1.transform.GetChild(i).GetComponent<Button>().interactable = true;
				}
			
				for(int i = 0; i < gm.levelCount - 11; i++)
				{
					levels2.transform.GetChild(i).GetComponent<Button>().interactable = true;
				}
			}
		}

		public void StringInput()
		{
			if (checkInput == false)
			{
				var tempString = customText.text;
				rows = Convert.ToInt32(tempString);
				columns = rows;
				Debug.Log("Row Stored as " + rows);
			}
			else
			{
				var tempString = customText.text;
				columns = Convert.ToInt32(tempString);
				Debug.Log("Column Stored " + columns);
			}
		}

		public void ShowPath()
		{
			if(!gm.showPathCoolDown) return;

			StartCoroutine(gm.PathFinding());

		}
		public void CustomNext()
		{
			if (checkInput == false)
			{
				checkInput = true;
			}
			else
			{
				gm.isCustomMaze = true;
				gm.GenerateNewMaze(rows, columns);
			}
		}
	
		//Calling UI methods

		public void StartButton()
		{
			EnableMe(levelSelectMenu);
		}
    
		public void SettingsButton()
		{
			EnableMe(settingsMenu);
		}

		public void CustomMazeButton()
		{
			EnableMe(customMazeMenu);
		}
	
		public void PauseButton()
		{
			EnableMe(pauseMenu);
			gm.Pause();
			DisableMe(joyStick);
			DisableMe(showPathButton);
			DisableMe(map);
			DisableMe(II);
		}
	
		public void XS()
		{
			CloseMenu(settingsMenu);
		}
	
		public void XL()
		{
			CloseMenu(levelSelectMenu);
		}
	
		public void XP()
		{
			gm.Resume();
			EnableMe(joyStick);
			CloseMenu(pauseMenu);
			EnableMe(II);

			if (gm.isCustomMaze)
			{
				if (gm.showPathCustom)
				{
					EnableMe(showPathButton);
				}

				if (gm.showMapCustom)
				{
					EnableMe(map);
				}
			}
			else
			{
				EnableMe(showPathButton);
				EnableMe(map);
			}
		}

		public void XC()
		{
			CloseMenu(customMazeMenu);
		}
	
		public void	Next()
		{
			CloseMenu(levels1);
			EnableMe(levels2);
			EnableMe(previousButton);
			DisableMe(nextButton);
		}
	
		public void Previous()
		{
			CloseMenu(levels2);
			EnableMe(levels1);
			EnableMe(nextButton);
			DisableMe(previousButton);
		}

		//Methods to initiate UI
		
		public static void EnableMe(GameObject obj)
		{
			obj.SetActive(true);
		}
	
		private void CloseMenu(GameObject obj)
		{
			LeanTween.scale(obj, Vector3.zero, 0.2f).setOnComplete(() => DisableMe(obj));
		}
	
		public static void DisableMe(GameObject obj)
		{
			obj.SetActive(false);
		}
	
		public void One()
		{
			gm.GenerateNewMaze(Level4x4, Level4x4);
			gm.currentLevel = 0;
		}
		public void Two()
		{
			gm.GenerateNewMaze(Level4x4, Level4x4);
			gm.currentLevel = 1;
		}
		public void Three()
		{
			gm.GenerateNewMaze(Level4x4, Level4x4);
			gm.currentLevel = 2;
		}
		public void Four()
		{
			gm.GenerateNewMaze(Level6x6, Level6x6);
			gm.currentLevel = 3;
		}
		public void Five()
		{
			gm.GenerateNewMaze(Level6x6, Level6x6);
			gm.currentLevel = 4;
		}
		public void Six()
		{
			gm.GenerateNewMaze(Level6x6, Level6x6);
			gm.currentLevel = 5;
		}
		public void Seven()
		{
			gm.GenerateNewMaze(Level6x6, Level6x6);
			gm.currentLevel = 6;
		}
		public void Eight()
		{
			gm.GenerateNewMaze(Level8x8, Level8x8);
			gm.currentLevel = 7;
		}
		public void Nine()
		{
			gm.GenerateNewMaze(Level8x8, Level8x8);
			gm.currentLevel = 8;
		}
		public void Ten()
		{
			gm.GenerateNewMaze(Level8x8, Level8x8);
			gm.currentLevel = 9;
		}
		public void Eleven()
		{
			gm.GenerateNewMaze(Level8x8, Level8x8);
			gm.currentLevel = 10;
		}
		public void Twelve()
		{
			gm.GenerateNewMaze(Level8x8, Level8x8);
			gm.currentLevel = 11;
		}
		public void Thirteen()
		{
			gm.GenerateNewMaze(Level10x10, Level10x10);
			gm.currentLevel = 12;
		}
		public void Fourteen()
		{
			gm.GenerateNewMaze(Level10x10, Level10x10);
			gm.currentLevel = 13;
		}
		public void Fifteen()
		{
			gm.GenerateNewMaze(Level10x10, Level10x10);
			gm.currentLevel = 14;
		}public void Sixteen()
		{
			gm.GenerateNewMaze(Level10x10, Level10x10);
			gm.currentLevel = 15;
		}
		public void Seventeen()
		{
			gm.GenerateNewMaze(Level12x12, Level12x12);
			gm.currentLevel = 16;
		}
		public void Eighteen()
		{
			gm.GenerateNewMaze(Level12x12, Level12x12);	
			gm.currentLevel = 17;
		}
		public void Nineteen()
		{
			gm.GenerateNewMaze(Level12x12, Level12x12);
			gm.currentLevel = 18;
		}
		public void Twenty()
		{
			gm.GenerateNewMaze(Level12x12, Level12x12);
			gm.currentLevel = 19;
		}
		public void Twentyone()
		{
			gm.GenerateNewMaze(Level15x15, Level15x15);
			gm.currentLevel = 20;
		}
		public void TwentyTwo()
		{
			gm.GenerateNewMaze(Level15x15, Level15x15);
			gm.currentLevel = 21;
		}
		public void TwentyThree()
		{
			gm.GenerateNewMaze(Level15x15, Level15x15);
			gm.currentLevel = 22;
		}
		public void TwentyFour()
		{
			gm.GenerateNewMaze(Level15x15, Level15x15);
			gm.currentLevel = 23;
		}
	}
}
