using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public float height = 10f;      // Height of the camera above the player
    public float smoothSpeed = 0.125f;  // Speed for smooth camera movement

    void LateUpdate()
    {
        // Calculate the desired camera position (fixed above the player)
        Vector3 targetPosition = new Vector3(player.position.x, height, player.position.z);

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Ensure the camera is always looking straight down at the player
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 90 degrees on X-axis for top-down view
    }
}
