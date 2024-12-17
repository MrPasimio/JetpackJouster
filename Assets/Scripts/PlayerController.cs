using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float thrustForce;
    [SerializeField] private float jumpForce;
    private float horizontalInput;
    
    private bool isOnGround = true;
    private bool isAlive = true;

    private Rigidbody rb;
    private Animator animator;
    private GameManager gameManager;
    [SerializeField] private ParticleSystem thrusterFire;

    //limits
    private float horizontalMin;
    private float horizontalMax;
    private float verticalMax;


    // Start is called before the first frame update
    void Start()
    {
        //Get the components!
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //establish limits
        horizontalMin = gameManager.horizontalMin;
        horizontalMax = gameManager.horizontalMax;
        verticalMax = gameManager.verticalMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

            //horizontal movement
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);

            //horizontal limits
            if (transform.position.x > horizontalMax)
            {
                transform.position = new Vector3(horizontalMax, transform.position.y, transform.position.z);
            }

            if (transform.position.x < horizontalMin)
            {
                transform.position = new Vector3(horizontalMin, transform.position.y, transform.position.z);
            }

            //Jump if you are on the ground
            if (Input.GetButtonDown("Thrusters") && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }
    }

    private void FixedUpdate()
    {
        //JetPack
        if(Input.GetButton("Thrusters") && !isOnGround && isAlive)
        {
            if(transform.position.y < verticalMax)
            {
                rb.AddForce(Vector3.up * thrustForce);
            }
            if(!thrusterFire.isPlaying)
            {
                thrusterFire.Play();
            }
        }
        else
        {
            thrusterFire.Stop();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        gameManager.LoseLife();
    }

   
}
