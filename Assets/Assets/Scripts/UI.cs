using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class UI : MonoBehaviour
	{
		private static bool gameIsPaused = false;
	
		private GameManager gm;
    
		[SerializeField] private GameObject settingsMenu;
		[SerializeField] private GameObject levelSelectMenu;
		[SerializeField] private GameObject pauseMenu;
		[SerializeField] private GameObject levels1;
		[SerializeField] private GameObject levels2;
		[SerializeField] private GameObject nextButton;
		[SerializeField] private GameObject previousButton;

		public GameObject joyStick;
		public GameObject levelComplete;
		public GameObject II;
	
		private int Level4x4 = 4;
		private int Level6x6 = 6;
		private int Level8x8 = 8;
		private int Level10x10 = 10;
		private int Level12x12 = 12;
		private int Level15x15 = 15;

		private void Start()
		{
			gm = FindObjectOfType<GameManager>();

			if (gm == null)
			{
				Debug.LogError("There is no game manager in the scene");
			}
		}

		public void ExitButton()
		{
			GameManager.ExitApplication();
		}

		public void MainMenu()
		{
			Time.timeScale = 1f;
			GameManager.LoadMainMenu();
		}
	
		public void NextLevel()
		{	
			if(gm.levelCount == 0)
			{
				One();
			}
			if(gm.levelCount == 1)
			{
				Two();
			}
			if(gm.levelCount == 2)
			{
				Three();
			}
			if(gm.levelCount == 3)
			{
				Four();
			}
			if(gm.levelCount == 4)
			{
				Five();
			}
			if(gm.levelCount == 5)
			{
				Six();
			}
			if(gm.levelCount == 6)
			{
				Seven();
			}
			if(gm.levelCount == 7)
			{
				Eight();
			}
			if(gm.levelCount == 8)
			{
				Nine();
			}
			if(gm.levelCount == 9)
			{
				Ten();
			}
			if(gm.levelCount == 10)
			{
				Eleven();
			}
			if(gm.levelCount == 11)
			{
				Twelve();
			}
			if(gm.levelCount == 12)
			{
				Thirteen();
			}
			if(gm.levelCount == 13)
			{
				Fourteen();
			}
			if(gm.levelCount == 14)
			{
				Fifteen();
			}
			if(gm.levelCount == 15)
			{
				Sixteen();
			}
			if(gm.levelCount == 16)
			{
				Seventeen();
			}
			if(gm.levelCount == 17)
			{
				Eighteen();
			}
			if(gm.levelCount == 18)
			{
				Nineteen();
			}
			if(gm.levelCount == 19)
			{
				Twenty();
			}
			if(gm.levelCount == 20)
			{
				Twentyone();
			}
			if(gm.levelCount == 21)
			{
				TwentyTwo();
			}
			if(gm.levelCount == 22)
			{
				TwentyThree();
			}
			if(gm.levelCount == 23)
			{
				TwentyFour();
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
				/*
			foreach (GameObject child in levels1.transform)
			{
				child.GetComponent<Button>().interactable = true;
			}
			*/
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
	
		//Calling UI methods

		public void StartButton()
		{
			EnableMe(levelSelectMenu);
		}
    
		public void SettingsButton()
		{
			EnableMe(settingsMenu);
		}
	
		public void PauseButton()
		{
			EnableMe(pauseMenu);
			Invoke(nameof(Pause), 1f);
			DisableMe(joyStick);
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
			Resume();
			EnableMe(joyStick);
			CloseMenu(pauseMenu);
			EnableMe(II);
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
	
	
		public void Pause()
		{
			//Time.timeScale = 0f;
			gameIsPaused = true;
		}

		public void Resume()
		{
			Time.timeScale = 1f;
			gameIsPaused = false;
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
