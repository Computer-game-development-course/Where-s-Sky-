using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // Assuming the player's tag is "Player"
    private const string PlayerTag = "Player";
    private GameObject coin;

    void Start()
    {
        DisableAllChildren();
        EnableRandomChild();
    }

    void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void EnableRandomChild()
    {
        int count = transform.childCount;
        if (count == 0) return; // Exit if there are no children

        int index = Random.Range(0, count);
        coin = transform.GetChild(index).gameObject;
        coin.SetActive(true);
    }

    public void WonMoney()
    {
        coin.SetActive(false);
        // Wait for a frame to ensure the deactivation is processed before enabling another child
        Invoke("EnableRandomChild", 0);
    }

    // // This method assumes children have a Collider component set to trigger
    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.CompareTag(PlayerTag))
    //     {
    //         // Deactivate the collected money object
    //         collider.gameObject.SetActive(false);

    //         // Wait for a frame to ensure the deactivation is processed before enabling another child
    //         Invoke("EnableRandomChild", 0);
    //     }
    // }
}
