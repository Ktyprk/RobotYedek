using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArm : MonoBehaviour
{
    public Collider kolUcuCollider; // Robot kolunun ucu i�in collider
    private Transform tutulanItem; // Ta��nan item

    private void OnTriggerEnter(Collider other)
    {
        // Kol ucundaki collider bir itema de�di�inde
        if (other.CompareTag("Carry"))
        {
            tutulanItem = other.transform;

            // Item'� kolun alt�nda tut
            tutulanItem.SetParent(transform);
        }
    }
}
