using System.Collections;
using UnityEngine;

public class CircleGrab : MonoBehaviour
{
    public static CircleGrab Instance { get; private set; }
    private GameObject robot;
    private Transform robotArm; // This will now represent the specific tentacle that touched the ring.

    public string robotArmTag = "Robot",
                  circleGrabPipeTag = "Pipe";

    private bool isBeingCarried, isFinished, dropCooldown;

    private CircleGrabPipe currentPipe;

    [SerializeField] private Vector3 grabRotation;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        robot = SubmarineController.Instance.gameObject;
    }

    private void Update()
    {
        if (isBeingCarried)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentPipe != null)
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

    public void Drop()
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

            // Find the attachment point under the tentacle that touched the ring.
            Transform attachPoint = robotArm.Find("AttachPoint"); // Ensure each tentacle has an AttachPoint GameObject.
            if (attachPoint != null)
            {
                isBeingCarried = true;
                transform.SetParent(attachPoint);
                transform.localPosition = Vector3.zero; // Optionally, adjust this to match the exact desired position relative to the attachPoint.
                transform.localRotation = Quaternion.Euler(grabRotation);

                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (other.CompareTag(circleGrabPipeTag) && !isFinished)
        {
            currentPipe = other.GetComponent<CircleGrabPipe>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(circleGrabPipeTag) && !isFinished && other.GetComponent<CircleGrabPipe>() == currentPipe)
        {
            currentPipe = null;
 
}
}
}