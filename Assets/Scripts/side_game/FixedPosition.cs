using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    public Vector3 fixedWorldPosition;

    void Update()
    {
        transform.position = fixedWorldPosition;
    }
}
