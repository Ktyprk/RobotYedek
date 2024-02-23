using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrab : MonoBehaviour
{
    private GameObject robot;
    private Transform robotArm;
    public Transform grabPoint; // Added grab point reference

    public string robotArmTag = "Robot",
        circleGrabPipeTag = "Pipe";

    private bool isBeingCarried, isFinished;

    private void Start()
    {
        robot = SubmarineController.Instance.gameObject;
        grabPoint = robot.transform.GetChild(0);
    }

    private void Drop()
    {
        isBeingCarried = false;

        transform.parent = null;
        robotArm = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(robotArmTag) && !isFinished && !isBeingCarried)
        {
            robotArm = other.transform;

            isBeingCarried = true;

            transform.SetParent(robot.transform);
            transform.position = grabPoint.position; // Teleport to grab point
        }

        if (other.CompareTag(circleGrabPipeTag) && !isFinished)
        {
            isBeingCarried = false;
            isFinished = true;

            transform.parent = null;
            robotArm = null;
        }
    }
}