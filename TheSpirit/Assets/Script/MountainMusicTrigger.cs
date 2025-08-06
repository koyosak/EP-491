using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class MountainMusicTrigger : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event playMountainMusicEvent;
    public AK.Wwise.Event playExplorationMusicEvent;

    [Header("RTPC Settings")]
    public string rtpcName = "Player_Altitude";
    public float minAltitude = 20f;
    public float maxAltitude = 100f;

    private Transform playerTransform;
    private bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            Debug.Log("Entered Trigger: " + other.name);

            playerTransform = other.transform;
            playerInside = true;

            // Play Mountain music (overrides exploration)
            playMountainMusicEvent?.Post(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerInside)
        {
            playerInside = false;
            playerTransform = null;

            // Play Exploration music (overrides mountain music)
            playExplorationMusicEvent?.Post(gameObject);

            // Optional: reset RTPC value
            AkSoundEngine.SetRTPCValue(rtpcName, 0f);
        }
    }

    void Update()
    {
        if (playerInside && playerTransform != null)
        {
            float altitude = playerTransform.position.y;
            float normalized = Mathf.InverseLerp(minAltitude, maxAltitude, altitude);
            float rtpcValue = normalized * 100f;

            AkSoundEngine.SetRTPCValue(rtpcName, rtpcValue);
        }
    }
}
