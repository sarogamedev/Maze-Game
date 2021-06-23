using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	[Range(1, 1000)] public int width;

	[Range(1, 1000)] public int height;
	
	public static GameManager instance;
	
	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		instance = this;
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
	}
	
	public void GenerateNewMaze(int wid, int ht)
	{
		width = wid;
		height = ht;
		SceneManager.UnloadSceneAsync("MainMenu");
		SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
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
