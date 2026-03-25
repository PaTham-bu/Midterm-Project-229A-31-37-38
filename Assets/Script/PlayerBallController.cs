using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sideForce = 150f;        // How strong A/D movement is
    public float forwardForce = 20f;      // Small forward boost
    public float maxSideSpeed = 10f;      // Limit left/right speed

    [Header("Speed Scaling")]
    public float speedMultiplier = 1f;
    public float speedIncreaseRate = 0.1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Recommended Rigidbody settings
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        HandleMovement();
        IncreaseDifficulty();
        LimitSideSpeed();
    }

    void Update()
    {
        // If player falls below certain height → lose
        if (transform.position.y < -10f)
        {
            GameOver();
        }
    }
    void HandleMovement()
    {
        float moveX = 0f;

        // A = left
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;

        // D = right
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // Apply sideways force
        rb.AddForce(Vector3.right * moveX * sideForce * speedMultiplier);

        // Small forward force (keeps game fast like Slope)
        rb.AddForce(Vector3.forward * forwardForce);
    }

    void IncreaseDifficulty()
    {
        // Gradually increase speed over time
        speedMultiplier += speedIncreaseRate * Time.fixedDeltaTime;
    }

    void LimitSideSpeed()
    {
        // Prevent player from sliding too fast sideways
        Vector3 velocity = rb.velocity;

        velocity.x = Mathf.Clamp(velocity.x, -maxSideSpeed, maxSideSpeed);

        rb.velocity = velocity;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        // Stop time
        Time.timeScale = 0f;

        // Optional: destroy player
        // Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }
}