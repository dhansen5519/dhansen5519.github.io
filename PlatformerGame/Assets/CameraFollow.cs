using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the character
    public Vector3 offset;   // Offset for the camera position

    void Update()
    {
        // Update camera position based on the character's position and offset
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + offset.x, offset.y, transform.position.z);
        }
    }
}
