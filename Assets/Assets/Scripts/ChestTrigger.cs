using UnityEngine;

namespace Assets.Scripts
{
	public class ChestTrigger : MonoBehaviour
	{
		[SerializeField] private GameObject levelComplete;
		private static readonly int ChestOpen = Animator.StringToHash("chestOpen");

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;
			gameObject.GetComponentInParent<Animator>().SetBool(ChestOpen, true);
			Invoke(nameof(LevelComplete), 2f);
		}
	
		private void LevelComplete()
		{	
			Debug.Log("Level Complete!");
			var gm = FindObjectOfType<GameManager>();
		
			if(gm.currentLevel == gm.levelCount)
			{
				gm.levelCount++;
			}

			SaveSystem.SaveGame(gm);
			var ui = FindObjectOfType<UI>();
			UI.DisableMe(ui.II);
			UI.DisableMe(ui.joyStick);
			UI.EnableMe(ui.levelComplete);
		}
	}
}
