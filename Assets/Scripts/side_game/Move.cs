using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [Tooltip("The speed at which the GameObject moves")]
    [SerializeField] float speed = 20f;

    [SerializeField] Vector3 initialPosition;
    [SerializeField] int moneyEarned = 10;

    private MoneyManager moneyManager;

    private void Start()
    {
        initialPosition = transform.position;
        moneyManager = FindObjectOfType<MoneyManager>();
    }

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
            transform.position = initialPosition;
        }

        // Check if the object that entered the trigger has the tag "Finish".
        else if (other.tag == "Finish")
        {
            moneyManager.WonMoney();
            GameManager.Instance.AddCoins(moneyEarned);
            Transform child = transform.GetChild(0);
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                StartCoroutine(ToggleSpriteRenderer(sr));
            }
        }
    }

    private IEnumerator ToggleSpriteRenderer(SpriteRenderer sr)
    {
        sr.enabled = true;
        yield return new WaitForSeconds(1.2f); // Wait for 0.2 seconds
        sr.enabled = false;
    }
}
