using UnityEngine;

public class RedCircleController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float fleeDistance = 3f;

    public float maxSpeed = 5f;       // Max speed for the red circle
    public float damping = 0.98f;     // Damping factor to simulate slight resistance   
    private Vector2 screenBounds;
    private Vector3 screenCenter;
    private Vector3 velocity; 

    void Start()
    {
        // Calculate screen bounds in world coordinates
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Set the center of the screen in world coordinates
        screenCenter = Vector3.zero;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // // Move away from the player if within flee distance
        // if (distance < fleeDistance)
        // {
        //     Vector3 direction = (transform.position - player.position).normalized;
        //     transform.Translate(direction * moveSpeed * Time.deltaTime);
        // }

        // Apply a force to move away from the player if within flee distance
        if (distance < fleeDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            velocity += direction * moveSpeed * Time.deltaTime;
        }

        // Clamp the velocity to prevent the red circle from moving too fast
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Apply damping to the velocity to prevent indefinite sliding
        velocity *= damping;

        // Update the position based on velocity
        transform.position += velocity * Time.deltaTime;

        // Bounce off screen edges
        BounceOffScreenEdges();

        // Clamp the position to stay within screen bounds
        ClampPositionWithinScreen();

        // Check if the red circle is in a corner and move it to the center if it is
        TeleportToCenterIfInCorner();
    }

    void ClampPositionWithinScreen()
    {
        Vector3 pos = transform.position;

        // Ensure the red circle stays within the screen bounds
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
    }

    void TeleportToCenterIfInCorner()
    {
        Vector3 pos = transform.position;

        // Define how close to the corner the red circle needs to be to trigger the teleport
        float cornerThreshold = 0.1f;

        // Check if the red circle is close to any corner and teleport to the center if true
        if ((pos.x <= -screenBounds.x + cornerThreshold && pos.y >= screenBounds.y - cornerThreshold) ||    // Top-left corner
            (pos.x >= screenBounds.x - cornerThreshold && pos.y >= screenBounds.y - cornerThreshold) ||    // Top-right corner
            (pos.x <= -screenBounds.x + cornerThreshold && pos.y <= -screenBounds.y + cornerThreshold) ||  // Bottom-left corner
            (pos.x >= screenBounds.x - cornerThreshold && pos.y <= -screenBounds.y + cornerThreshold))     // Bottom-right corner
        {
            transform.position = screenCenter; // Move to center of the screen
            velocity = Vector3.zero;           // Reset velocity upon teleporting
        }
    }

    void BounceOffScreenEdges()
    {
        Vector3 pos = transform.position;

        // Check for horizontal screen bounds and reverse x velocity if at an edge
        if (pos.x <= -screenBounds.x || pos.x >= screenBounds.x)
        {
            velocity.x = -velocity.x;
            // Clamp the position to ensure the object stays within bounds after bounce
            pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        }

        // Check for vertical screen bounds and reverse y velocity if at an edge
        if (pos.y <= -screenBounds.y || pos.y >= screenBounds.y)
        {
            velocity.y = -velocity.y;
            // Clamp the position to ensure the object stays within bounds after bounce
            pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);
        }

        // Update position after bounce checks
        transform.position = pos;
    }
}
