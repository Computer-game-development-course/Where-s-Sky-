using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component logs all four kinds of collisions involving its object: both 2D and 3D triggers, as well as 2D and 3D collisions.
 */
public class CollisionLogger : MonoBehaviour
{
    private void Start()
    {
        // Log a message when the CollisionLogger script starts.
        Debug.Log("Start CollisionLogger on " + this.name);
    }

    // Called when another 2D collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Log a message stating that a 2D trigger event occurred, with the names and tags of both involved objects.
        Debug.Log(this.name + " Trigger 2D with " + other.name + " tag=" + other.tag);
    }

    // Called when this object starts colliding with another 2D collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Access the Collider2D component of the object that this object collided with.
        Collider2D other = collision.collider;

        // Log a message stating that a 2D collision event occurred, including the names and tags of the objects involved.
        Debug.Log(this.name + " Collision 2D with " + other.name + " tag=" + other.tag);
    }

    // Called when another collider enters the 3D trigger area associated with this object.
    private void OnTriggerEnter(Collider other)
    {
        // Log a message stating that a 3D trigger event occurred, with the names and tags of both involved objects.
        Debug.Log(this.name + " Trigger with name=" + other.name + " tag=" + other.tag);
    }

    // Called when this object starts colliding with another collider in 3D space.
    private void OnCollisionEnter(Collision collision)
    {
        // Access the Collider component of the object that this object collided with.
        Collider other = collision.collider;

        // Log a message stating that a 3D collision event occurred, including the names and tags of the objects involved.
        Debug.Log(this.name + " Collision with " + other.name + " tag=" + other.tag);
    }
}
