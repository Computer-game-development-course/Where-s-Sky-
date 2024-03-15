using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [Tooltip("The speed at which the GameObject moves")]
    [SerializeField] float speed = 20f;

    void Update()
    {
        // If the left arrow key is held down, move the GameObject to the left.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Translate the GameObject by speed and time since last frame to the left.
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        // If the right arrow key is held down, move the GameObject to the right.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Translate the GameObject by speed and time since last frame to the right.
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        // If the up arrow key is held down, move the GameObject upwards.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Translate the GameObject by speed and time since last frame upwards.
            transform.Translate(0, speed * Time.deltaTime, 0);
        }

        // If the down arrow key is held down, move the GameObject downwards.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Translate the GameObject by speed and time since last frame downwards.
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the tag "enemy".
        if (other.tag == "enemy")
        {
            // Destroy the enemy GameObject.
            Destroy(other.gameObject);
            // Also destroy this GameObject.
            Destroy(this.gameObject);
            // Then load the "try again" scene to presumably let the player try again.
            SceneManager.LoadScene("try again");
        }
        // Check if the object that entered the trigger has the tag "Finish".
        else if (other.tag == "Finish")
        {
            // Destroy the finish GameObject.
            Destroy(other.gameObject);
            // Also destroy this GameObject.
            Destroy(this.gameObject);
            // Then load the "win" scene to show that the player has won.
            SceneManager.LoadScene("win");
        }
    }
}
