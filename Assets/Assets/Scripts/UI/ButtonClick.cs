using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Vector3 from = Vector3.one;
    [SerializeField] private Vector3 to = Vector3.one * 0.95f;
 
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Scale);
    }
    
    private void Scale()
    {
        LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.02f).setEaseInElastic().setLoopPingPong(1).setIgnoreTimeScale(true);
    }
}
