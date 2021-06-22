using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
	private GameManager gm;
    
	[SerializeField] private GameObject settingsMenu;
	[SerializeField] private GameObject levelSelectMenu;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject x;
	
	private int Level4x4 = 4;
	private int Level6x6 = 6;
	private int Level8x8 = 8;
	private int Level10x10 = 10;
	
    
    private void Start()
    {
	    gm = FindObjectOfType<GameManager>();
	    
	    if (gm == null)
	    {
	    	Debug.LogError("There is no game manager in the scene");
	    }
    }
    
	public void LevelOneToThree()
	{
		gm.GenerateNewMaze(Level4x4, Level4x4);
	}
	
	public void LevelFourToSeven()
	{
		gm.GenerateNewMaze(Level6x6, Level6x6);
	}
	
	public void LevelEightToTwelve()
	{
		gm.GenerateNewMaze(Level8x8, Level8x8);
	}

    public void StartButton()
    {
	    gm.Menu(levelSelectMenu);
    }

    public void ExitButton()
    {
        gm.ExitApplication();
    }
    
	public void SettingsButton()
	{
		gm.Menu(settingsMenu);
	}
	
	public void PauseButton()
	{
		gm.Menu(pauseMenu);
		gm.DisableMe(x);
	}
	
	public void XS()
	{
		gm.CloseMenu(settingsMenu);
	}
	
	public void XL()
	{
		gm.CloseMenu(levelSelectMenu);
	}
	
	public void XP()
	{
		gm.CloseMenu(pauseMenu);
		gm.Menu(x);
	}
	
	public void MainMenu()
	{
		gm.LoadMainMenu();
	}
}
