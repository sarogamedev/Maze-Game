using UnityEngine;

namespace Assets.Scripts
{
	public class UITweenManager : MonoBehaviour
	{
		[SerializeField] private float duration;
		[SerializeField] private LeanTweenType easeType;
		[SerializeField] private Vector3 scale = new Vector3(1, 1, 1);
	
		private void OnEnable()
		{
			transform.localScale = Vector3.zero;
			LeanTween.scale(gameObject, scale, duration).setEase(easeType);
		}
	}
}
