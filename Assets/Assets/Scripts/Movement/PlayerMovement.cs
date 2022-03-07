using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float smoothMovement;
        [SerializeField] private Rigidbody rb;
        public Animator animator;
    
        private Vector3 turnSmooth;
        private Vector3 movement;

        private float horizontal;
        private float vertical;
        private Transform player;
        public readonly int IsRunning = Animator.StringToHash("isRunning");


        private void Start()
        {
            GameObject obj = GameObject.FindWithTag("VirtualJoystick");
            joystick = obj.GetComponent<Joystick>();
            rb = GetComponent<Rigidbody>();
            player = GetComponent<Transform>();
            animator = GetComponent<Animator>();
        }

	
        private void Update()
        {
            turnSmooth.x = joystick.Direction.x;
            turnSmooth.z = joystick.Direction.y;
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;

            if (horizontal != 0 || vertical != 0)
            {
                animator.SetBool(IsRunning, true);
                player.rotation = Quaternion.LookRotation(turnSmooth, player.up);
            }
            else
            {
                animator.SetBool((IsRunning), false);
            }
        }

        private void FixedUpdate()
        {
            var mag = smoothMovement * Time.fixedDeltaTime;
            
            if (horizontal != 0)
            {
                HorizontalMovement(mag);
            }
            
            if (vertical != 0)
            {
                VerticalMovement(mag);
            }
        }

        private void Move()
        {
            
        }
        
        private void HorizontalMovement(float mag)
        {
            //player.rotation = Quaternion.LookRotation(turnSmooth, player.up);
            player.Translate (Vector3.right * (horizontal * mag), Space.World);
        }
        
        private void VerticalMovement(float mag)
        {
            //player.rotation = Quaternion.LookRotation(turnSmooth, player.up);
            player.Translate (Vector3.forward * (vertical * mag), Space.World);
        }
    }
}
