using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sideForce = 150f;        // A/D movement strength
    public float forwardForce = 20f;      // Forward push
    public float maxSideSpeed = 10f;      // Clamp left/right speed

    [Header("Speed Scaling")]
    public float speedMultiplier = 1f;
    public float speedIncreaseRate = 0.1f;

    [Header("Speed Cap")]
    public float maxSpeedMultiplier = 3f;

    [Header("Fall Settings")]
    public float fallYLimit = -100f;      // Adjust this value

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody settings
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        HandleMovement();
        IncreaseDifficulty();
        LimitVelocity();
    }

    void Update()
    {
        CheckFall();
    }

    void HandleMovement()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
            moveX = -1f;

        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // Side movement
        rb.AddForce(Vector3.right * moveX * sideForce * speedMultiplier);

        // Forward movement
        rb.AddForce(Vector3.forward * forwardForce);
    }

    void IncreaseDifficulty()
    {
        speedMultiplier += speedIncreaseRate * Time.fixedDeltaTime;
        speedMultiplier = Mathf.Clamp(speedMultiplier, 1f, maxSpeedMultiplier);
    }

    void LimitVelocity()
    {
        Vector3 velocity = rb.velocity;

        // Clamp side speed
        velocity.x = Mathf.Clamp(velocity.x, -maxSideSpeed, maxSideSpeed);

        // Clamp forward speed
        float maxForwardSpeed = forwardForce * maxSpeedMultiplier;
        velocity.z = Mathf.Clamp(velocity.z, 0f, maxForwardSpeed);

        rb.velocity = velocity;
    }

    void CheckFall()
    {
        if (transform.position.y < fallYLimit)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        // Save final time
        GameTimer timer = FindObjectOfType<GameTimer>();

        if (timer != null)
        {
            float finalTime = timer.GetTime();
            PlayerPrefs.SetFloat("FinalTime", finalTime);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("GameTimer not found!");
        }

        // Load End Credits
        SceneManager.LoadScene("End Credits");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }
}