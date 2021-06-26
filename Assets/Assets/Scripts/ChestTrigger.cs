using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			gameObject.GetComponentInParent<Animator>().SetBool("chestOpen", true);
		}
	}
}
