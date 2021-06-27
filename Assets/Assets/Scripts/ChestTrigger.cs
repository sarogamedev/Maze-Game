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
		FindObjectOfType<GameManager>().levelCount++;
		var ui = FindObjectOfType<UI>();
		ui.EnableMe(ui.levelComplete);
	}
}
