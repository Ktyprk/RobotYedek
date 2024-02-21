using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrabPipe : MonoBehaviour
{
    [SerializeField] private Color color;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CircleGrab>() != null)
        {
            other.GetComponent<Renderer>().material.color = color;

            other.transform.position = transform.position;
            other.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-90, 0, 0));
        }
    }
}
