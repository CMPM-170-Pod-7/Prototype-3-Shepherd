using UnityEngine;

public class GreenCircleController : MonoBehaviour
{
    public GameObject redCircle; // Reference to the red circle
    public float shrinkFactor = 0.9f; // Factor by which the red circle shrinks on each touch
    private Vector2 screenBounds;

    void Start()
    {
        // Calculate screen bounds in world coordinates
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the red circle has collided with the green circle
        if (other.gameObject == redCircle)
        {
            // Shrink the red circle
            redCircle.transform.localScale *= shrinkFactor;

            // Move the green circle to a random position within the screen bounds
            MoveToRandomPosition();
        }
    }

    void MoveToRandomPosition()
    {
        // Generate a random position within the camera bounds
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);

        // Update the green circle's position
        transform.position = new Vector2(randomX, randomY);
    }
}
