using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCTriggerZone : MonoBehaviour
{
    public AK.Wwise.Event rtpcEvent; // Assign your Wwise Event in Inspector
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Calling Wwise Event: " + rtpcEvent.Name);
            rtpcEvent.Post(gameObject);
        }
    }
}

