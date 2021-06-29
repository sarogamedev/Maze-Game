using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float smoothMovement;
    [SerializeField] private Rigidbody rb;
	[SerializeField] private Animator animator;
    
    private Vector3 turnSmooth;

    private float horizontal;
    private float vertical;
    
    
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        GameObject obj = GameObject.FindWithTag("VirtualJoystick");
        joystick = obj.GetComponent<Joystick>();
	    rb = GetComponent<Rigidbody>();
	    animator = GetComponent<Animator>();
    }

	
    private void Update()
    {
        turnSmooth.x = joystick.Direction.x;
        //turnSmooth.y = 0;
        turnSmooth.z = joystick.Direction.y;
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        
	    if (horizontal != 0 || vertical != 0)
        {
		    animator.SetBool("isRunning", true);
        }
        
        if (vertical == 0 || horizontal == 0)
        {
	        animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (horizontal == 0 || vertical == 0) return;

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
        float mag = smoothMovement * Time.fixedDeltaTime;
        
        rb.transform.rotation = Quaternion.LookRotation(turnSmooth, rb.transform.up);
        rb.transform.Translate (Vector3.right * (horizontal * mag), Space.World);
    }

    private void VerticalMovement()
    {
        float mag = smoothMovement * Time.fixedDeltaTime;

        rb.transform.rotation = Quaternion.LookRotation(turnSmooth, rb.transform.up);
        rb.transform.Translate (Vector3.forward * (vertical * mag), Space.World);
    }
}
