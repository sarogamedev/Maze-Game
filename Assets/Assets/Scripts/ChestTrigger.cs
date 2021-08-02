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
			gameObject.GetComponentInParent<Animator>().SetBool(ChestOpen, true);
			ui = FindObjectOfType<UI>();
			gm = FindObjectOfType<GameManager>();

			if (gm.isCustomMaze)
			{
				Debug.Log("Custom Level Complete!");
			}
			else
			{
				Invoke(nameof(LevelComplete), 2f);
			}
		}
	
		private void LevelComplete()
		{	
			if(gm.currentLevel == gm.levelCount)
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
