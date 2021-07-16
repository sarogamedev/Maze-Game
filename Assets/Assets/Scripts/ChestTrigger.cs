using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
	[SerializeField] private GameObject levelComplete;
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			gameObject.GetComponentInParent<Animator>().SetBool("chestOpen", true);
			Invoke("LevelComplete", 2f);
		}
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
		ui.DisableMe(ui.II);
		ui.DisableMe(ui.joyStick);
		ui.EnableMe(ui.levelComplete);
	}
}
