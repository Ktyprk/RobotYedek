using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrab : MonoBehaviour
{
    private GameObject robot;
    private Transform robotArm;

    public string robotArmTag = "Robot",
        circleGrabPipeTag = "Pipe";

    private bool isBeingCarried, isFinished, dropCooldown;

    private CircleGrabPipe currentPipe;

    [SerializeField] private Vector3 grabRotation;

    private void Start()
    {
        robot = SubmarineController.Instance.gameObject;
    }

    private void Update()
    {
        if(isBeingCarried)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(currentPipe != null)
                {
                    if (currentPipe.isRobotArmColliding) return;

                    isBeingCarried = false;
                    isFinished = true;

                    transform.parent = null;
                    robotArm = null;

                    currentPipe.PlaceCircle(gameObject);
                }
                else
                {
                    Drop();
                }
            }
        }
    }

    private IEnumerator DropCooldown()
    {
        dropCooldown = true;

        yield return new WaitForSeconds(2f);

        dropCooldown = false;
    }

    private void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        isBeingCarried = false;

        transform.parent = null;
        robotArm = null;

        StartCoroutine(DropCooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(robotArmTag) && !isFinished && !isBeingCarried && !dropCooldown)
        {
            robotArm = other.transform;
            Transform attachPoint = robotArm.Find("AttachPoint"); 

            isBeingCarried = true;

            transform.SetParent(attachPoint);
            //transform.position = RobotCarryPoint.carryPoint.position;
            transform.rotation = Quaternion.Euler(RobotCarryPoint.carryPoint.rotation.eulerAngles + grabRotation);

            GetComponent<Rigidbody>().isKinematic = true;
        }

        if (other.CompareTag(circleGrabPipeTag) && !isFinished)
        {
            currentPipe = other.GetComponent<CircleGrabPipe>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(circleGrabPipeTag) && !isFinished && other.GetComponent<CircleGrabPipe>() == currentPipe)
        {
            currentPipe = null;
        }
    }
}
