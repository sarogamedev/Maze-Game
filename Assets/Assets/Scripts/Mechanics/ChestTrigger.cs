using UnityEngine;

namespace Assets.Scripts
{
	public class ChestTrigger : MonoBehaviour
	{
		private UI ui;
		private GameManager gm;

		[SerializeField] private GameObject chestParticle;
		private static readonly int ChestOpen = Animator.StringToHash("chestOpen");

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			Instantiate(chestParticle, transform);
			var player = other.gameObject.GetComponent<PlayerMovement>();
			player.enabled = false;
			player.animator.SetBool(player.IsRunning, false);

			ui = FindObjectOfType<UI>();
			gm = FindObjectOfType<GameManager>();
			
			ui.showPathButton.SetActive(false);	
			
			gameObject.GetComponentInParent<Animator>().SetBool(ChestOpen, true);

			gm.gameIsPaused = true;
			
			Invoke(gm.isCustomMaze ? nameof(CustomLevelComplete) : nameof(LevelComplete), 2f);
			
		}

		private void CustomLevelComplete()
		{
			gm.isCustomMaze = false;
			
			UI.DisableMe(ui.II);
			UI.DisableMe(ui.joyStick);
			UI.EnableMe(ui.levelComplete);
			UI.DisableMe(ui.nextLevelButton);
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
