using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSounds : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSource;
    public float minDistance;
    public float maxDistance;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, listenerTransform.position);
        if (distance < minDistance)
        {
            audioSource.volume = 1;
        }
        else if (distance > maxDistance)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1 - ((distance - minDistance) / (maxDistance - minDistance));
        }
    }
}
