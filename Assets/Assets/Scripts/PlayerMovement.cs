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

        private float horizontal;
        private float vertical;
        public readonly int IsRunning = Animator.StringToHash("isRunning");


        private void Start()
        {
            GameObject obj = GameObject.FindWithTag("VirtualJoystick");
            joystick = obj.GetComponent<Joystick>();
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

	
        private void Update()
        {
            turnSmooth.x = joystick.Direction.x;
            turnSmooth.z = joystick.Direction.y;
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }

        private void FixedUpdate()
        {
            if (vertical == 0 && horizontal == 0)
            {
                animator.SetBool(IsRunning, false);
            }
        
            if (horizontal == 0 || vertical == 0) return;

            animator.SetBool(IsRunning, true);
        
            if (horizontal != 0)
            {
                HorizontalMovement();
            }

            if (vertical != 0)
            {
                VerticalMovement();
            }
        }

        private void HorizontalMovement()
        {
            var mag = smoothMovement * Time.fixedDeltaTime;
        
            rb.transform.rotation = Quaternion.LookRotation(turnSmooth, rb.transform.up);
            rb.transform.Translate (Vector3.right * (horizontal * mag), Space.World);
        }

        private void VerticalMovement()
        {
            var mag = smoothMovement * Time.fixedDeltaTime;

            rb.transform.rotation = Quaternion.LookRotation(turnSmooth, rb.transform.up);
            rb.transform.Translate (Vector3.forward * (vertical * mag), Space.World);
        }
    }
}
