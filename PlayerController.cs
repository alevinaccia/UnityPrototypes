using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement values")]
    public float speed;
    public float jumpForce;
    public int jumps;
    public cameraMovement camera;

    private bool isGrounded;
    private int currentJumps;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() 
    {
        speed = 10.0f;
        jumpForce = 10.0f;
        jumps = 2;
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position += new Vector3(speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"),0));

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            camera.offset.x = -5;
        }else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            camera.offset.x = +5;
        }

        if (Input.GetKeyDown("space"))
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        physics();
    }

    void jump()
    {   
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
            isGrounded = false;
            currentJumps++;
        }else if(currentJumps < jumps)
        {
            currentJumps++;
            rb.AddForce(new Vector3(0, jumpForce * 0.6f), ForceMode.Impulse);
            isGrounded = false;
        }
            
    }

    void physics()
    {
        if (!isGrounded)
        {
            if(rb.velocity.y < 0)
            {
                rb.drag = 0.1f;
            }else if(rb.velocity.y > 0 || rb.velocity.y == 0)
            {
                rb.drag = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Groundable")
        {
            isGrounded = true;
            currentJumps = 0;
            rb.drag = 1;
        }

        if (collision.collider.tag == "Coin")
        {
            print("Score++");
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Coin")
        {
            print("HEy");
            Destroy(col);
        }
    }
}
