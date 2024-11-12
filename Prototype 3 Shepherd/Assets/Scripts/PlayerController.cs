using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 screenBounds;

    void Start()
    {
        // Calculate screen bounds in world coordinates
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        // Handle player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Clamp the position to stay within screen bounds
        ClampPositionWithinScreen();
    }

    void ClampPositionWithinScreen()
    {
        Vector3 pos = transform.position;

        // Ensure the player stays within the screen bounds
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
    }
}
