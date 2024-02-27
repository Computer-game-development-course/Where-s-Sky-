using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    // This function is called when the collider attached to this GameObject
    // begins to touch another collider
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggeringTag && enabled)
        {
            Destroy(other.gameObject);
        }
    }
}
