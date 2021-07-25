using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Range(0, 5)] private float smoothSpeed;
        [SerializeField] private Vector3 offset;
    
        private void FixedUpdate()
        {
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        
            transform.LookAt(target);
        }
    }
}