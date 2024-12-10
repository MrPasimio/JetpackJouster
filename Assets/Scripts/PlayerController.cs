using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float thrustForce;
    [SerializeField] private float jumpForce;
    private float horizontalInput;
    
    private bool isOnGround = true;

    private Rigidbody rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //Get the components!
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);
    }

    private void FixedUpdate()
    {
        //JetPack
        if(Input.GetButton("Thrusters"))
        {
            //Jump if you are on the ground
            if (isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else  //Otherwise, float with jetpack
            {
                rb.AddForce(Vector3.up * thrustForce);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            animator.SetBool("isOnGround", isOnGround);
        }
    }
}
