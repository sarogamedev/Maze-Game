using UnityEngine;

namespace Assets.Scripts
{
	public class ChestTrigger : MonoBehaviour
	{
		private UI ui;
		private GameManager gm;
		private static readonly int ChestOpen = Animator.StringToHash("chestOpen");

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			var player = other.gameObject.GetComponent<PlayerMovement>();
			player.enabled = false;
			player.animator.SetBool(player.IsRunning, false);

			ui = FindObjectOfType<UI>();
			gm = FindObjectOfType<GameManager>();
			
			ui.showPathButton.SetActive(false);	
			
			gameObject.GetComponentInParent<Animator>().SetBool(ChestOpen, true);

			if (gm.isCustomMaze)
			{
				Invoke(nameof(gm.LoadMainMenu), 2f);
			}
			else
			{
				Invoke(nameof(LevelComplete), 2f);
			}
		}
	
		private void LevelComplete()
		{	
			if(gm.currentLevel == gm.levelCount && gm.levelCount < 23)
			{
				gm.levelCount++;
			}

			SaveSystem.SaveGame(gm);
			
			UI.DisableMe(ui.II);
			UI.DisableMe(ui.joyStick);
			UI.EnableMe(ui.levelComplete);
		}
	}
}
