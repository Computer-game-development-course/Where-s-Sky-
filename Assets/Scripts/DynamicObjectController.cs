using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectController : MonoBehaviour
{
    public bool isCatBehind = false;
    void OnMouseDown()
    {
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, GameManager.instance.cat.transform.position - transform.position, out hit))
        // {
        //     if (isCatBehind)
        //     {
        //         Debug.Log("Cat is behind this furniture!");

        //     }
        //     else
        //     {
        //         Debug.Log("Cat is not behind this furniture.");
        //     }
        // }
    }
}
