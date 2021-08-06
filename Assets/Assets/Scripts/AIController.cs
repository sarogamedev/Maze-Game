using Pathfinding;
using UnityEngine;

namespace Assets.Scripts
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private AIPath ai;
        [SerializeField] private AIDestinationSetter des;
        
        private GameObject chest;
        void Start()
        {
            chest = GameObject.FindGameObjectWithTag("Chest");
            des.target = chest.transform;
        }

        private void Update()
        {
            if(!ai.reachedDestination) return;
            
            Destroy(gameObject);
        }
    }
}
