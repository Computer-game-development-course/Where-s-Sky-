using UnityEngine;

public class FollowCameraTopLeft : MonoBehaviour
{
    public Vector3 offset; // Offset from the top left corner

    void LateUpdate()
    {
        // Find an active camera every frame (in case the active camera changes)
        Camera camera = Camera.main; // This finds the main camera tagged as "MainCamera"

        if (camera != null)
        {
            // Convert top left screen position to world point
            Vector3 point = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, camera.nearClipPlane));

            // Adjust position with offset
            transform.position = point + offset + camera.transform.forward * 10; // Adjust this value to set how far in front of the camera the object should appear

            // Optional: Make the object face the camera directly
            transform.LookAt(camera.transform);
            transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
        }
    }
}
