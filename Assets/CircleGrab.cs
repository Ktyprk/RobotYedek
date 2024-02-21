using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrab : MonoBehaviour
{
    private GameObject robot;
    private Transform robotArm;

    public string robotArmTag = "Robot",
                  circleGrabPipeTag = "Pipe";

    private bool isBeingCarried, isFinished;

    private void Start()
    {
        robot = SubmarineController.Instance.gameObject;
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
