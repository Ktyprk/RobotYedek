using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RosSharp.Urdf.Link;

public class LightGrab : MonoBehaviour
{
    private GameObject robot;
    private Transform robotArm;

    public string robotArmTag = "Robot",
        circleGrabPipeTag = "Pipe";

    private bool isBeingCarried, isFinished, dropCooldown;

    private CircleGrabPipe currentPipe;

    [SerializeField] private Vector3 grabRotation;
    [SerializeField] private float grabScale = 0.5f;
    [SerializeField] private Transform submarineDropPos;

    private Vector3 defScale;

    private bool isInsideSubmarine;

    private void Start()
    {
        defScale = transform.localScale;

        robot = SubmarineController.Instance.gameObject;
    }

    private void Update()
    {
        if(isBeingCarried)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (currentPipe != null)
                {
                    if (currentPipe.isRobotArmColliding) return;

                    isBeingCarried = false;
                    isFinished = true;

                    transform.parent = null;
                    robotArm = null;

                    transform.localScale = defScale * 1.5f;

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

        transform.localScale = defScale;

        if (isInsideSubmarine)
            transform.position = submarineDropPos.position;

        StartCoroutine(DropCooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Denizalti")
        {
            isInsideSubmarine = true;
        }

        if (other.CompareTag(robotArmTag) && !isFinished && !isBeingCarried && !dropCooldown)
        {
            robotArm = other.transform;

            isBeingCarried = true;

            transform.localScale = defScale * grabScale;

            transform.SetParent(RobotCarryPoint.carryPoint);
            transform.position = RobotCarryPoint.carryPoint.position;
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
        if (other.gameObject.tag == "Denizalti")
        {
            isInsideSubmarine = false;
        }

        if (other.CompareTag(circleGrabPipeTag) && !isFinished && other.GetComponent<CircleGrabPipe>() == currentPipe)
        {
            currentPipe = null;
        }
    }
}
