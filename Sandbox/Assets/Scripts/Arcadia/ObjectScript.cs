using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool shouldMove;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something");
        shouldMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        shouldMove = false;
        Debug.Log("Stop Hitting");
    }
}
