using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArm : MonoBehaviour
{
    public Collider kolUcuCollider; // Robot kolunun ucu için collider
    private Transform tutulanItem; // Taþýnan item

    private void OnTriggerEnter(Collider other)
    {
        // Kol ucundaki collider bir itema deðdiðinde
        if (other.CompareTag("Carry"))
        {
            tutulanItem = other.transform;

            // Item'ý kolun altýnda tut
            tutulanItem.SetParent(transform);
        }
    }
}
