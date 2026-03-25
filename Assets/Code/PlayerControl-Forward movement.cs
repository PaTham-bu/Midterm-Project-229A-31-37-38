using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 120f;
    public float jumpForce = 12f;

    Rigidbody rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Rotate();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Forward / Backward movement (W/S)
        float v = Input.GetAxis("Vertical");
        Vector3 forwardMove = transform.forward * v * moveSpeed;
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);
    }

    void Rotate()
    {
        //Left / Right rotation (A/D)
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
