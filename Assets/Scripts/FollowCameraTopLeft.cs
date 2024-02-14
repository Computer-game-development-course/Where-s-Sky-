using UnityEngine;

public class FollowCameraTopLeft : MonoBehaviour
{
    [Tooltip("Offset from the camera's top-left corner to position the GameObject.")]
    [SerializeField] Vector3 offset;

    // LateUpdate is called after all Update functions have been called.
    // This ensures the camera has finished its movements.
    void LateUpdate()
    {
        // Find the active main camera every frame, in case the active camera changes during gameplay.
        Camera camera = Camera.main; // This finds the main camera tagged as "MainCamera"

        if (camera != null) // Check if a main camera is found to prevent errors if no camera is found.
        {
            // Convert the top-left screen position (0, Screen.height) to a world point.
            // This is used as the base point to position the GameObject.
            Vector3 point = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, camera.nearClipPlane));

            // Adjust the GameObject's position using the calculated world point, the specified offset,
            // and moving it forward in front of the camera based on the camera's forward direction.
            // The '10' is an arbitrary distance to ensure the GameObject appears in front of the camera.
            transform.position = point + offset + camera.transform.forward * 10; // Adjust this value to set how far in front of the camera the object should appear

            // Adjusts the GameObject to face the camera directly.
            // This makes the object not only follow the camera positionally but also rotate to face the camera.
            transform.LookAt(camera.transform);

            // Resets the rotation's x and z axes to 0, maintaining only the y axis rotation.
            // This ensures the GameObject only rotates to face the camera horizontally, keeping its upright orientation.
            transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
        }
    }
}