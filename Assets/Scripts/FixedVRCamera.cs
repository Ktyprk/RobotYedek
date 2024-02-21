using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedVRCamera : MonoBehaviour
{
    public Transform characterHead; // Assign the character's head transform here
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Start()
    {
        // Save the initial local position and rotation
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // Constantly reset the camera's local position and rotation
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }

}
