using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrabPipe : MonoBehaviour
{
    [SerializeField] private bool changeColor;
    [SerializeField] private Color color;

    [SerializeField] private Transform placePos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CircleGrab>() != null)
        {
            if(changeColor)
                other.GetComponent<Renderer>().material.color = color;

            other.transform.position = placePos != null ? placePos.position : transform.position;
            other.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-90, 0, 0));
        }
    }
}
