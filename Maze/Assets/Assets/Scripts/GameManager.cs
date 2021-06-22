using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	[Range(1, 1000)] public int width;

	[Range(1, 1000)] public int height;
	
	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	public void GenerateNewMaze(int wid, int ht)
	{
		width = wid;
		height = ht;
        SceneManager.LoadScene("MainScene");
	}
    
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

    public void ExitApplication()
    {
        Application.Quit();
    }
    
	public void Menu(GameObject obj)
	{
		obj.SetActive(true);
	}
	
	public void CloseMenu(GameObject obj)
	{
		LeanTween.scale(obj, Vector3.zero, 0.2f).setOnComplete(() => DisableMe(obj));
	}
	
	public void DisableMe(GameObject obj)
	{
		obj.SetActive(false);
	}
}
