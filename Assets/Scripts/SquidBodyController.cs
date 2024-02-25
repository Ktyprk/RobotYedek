using System;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;

public class SquidBodyController : MonoBehaviour
{
    public RosConnector rosConnector; // Set in Unity Editor
    public string topicName = "Squid_Body_Movement"; // ROS topic name

    // Speed settings and other configurations
    public float initialMovementSpeed = 1.0f;
    public float initialRotationSpeed = 1.0f;
    public float initialVerticalSpeed = 1.0f;
    public float secondaryMovementSpeed = 2.0f;
    public float secondaryRotationSpeed = 2.0f;
    public float secondaryVerticalSpeed = 2.0f;
    public float speedChangeAfterSeconds = 10.0f;

    // Internal state
    private Rigidbody rb;
    private UnityEngine.Vector3 linearMovement = UnityEngine.Vector3.zero;
    private float angularRotation = 0.0f;
    private string subscriptionId;
    private float timeSinceStart = 0f;
    private bool hasStopped = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        subscriptionId = rosConnector.RosSocket.Subscribe<Twist>(topicName, ReceiveMessage);
    }

    private void ReceiveMessage(Twist message)
    {
        linearMovement = new UnityEngine.Vector3((float)message.linear.y, (float)message.linear.z, (float)message.linear.x);
        angularRotation = (float)message.angular.z;

        // Debug message to check angular.x value
        Debug.Log("Received angular.x: " + message.angular.x);

        // Handle drop action if angular.x == 1
        if (message.angular.x == 1)
        {
            PerformDropAction();
        }

        // Reset speed and timer if the robot has stopped
        if (linearMovement == UnityEngine.Vector3.zero && angularRotation == 0)
        {
            if (!hasStopped) // Only reset if the robot was previously moving
            {
                timeSinceStart = 0f;
                hasStopped = true;
            }
        }
        else
        {
            if (hasStopped) // If the robot starts moving again, reset the timer
            {
                timeSinceStart = 0f;
                hasStopped = false;
            }
        }
    }

    // Implement the drop action here
    private void PerformDropAction()
    {
        Debug.Log("Drop action triggered");
        // Implement your drop logic here
         CircleGrab.Instance.Drop();
        
    }

    void FixedUpdate()
    {
        timeSinceStart += Time.fixedDeltaTime;

        // Determine current speed settings based on elapsed time
        float movementSpeed = timeSinceStart <= speedChangeAfterSeconds ? initialMovementSpeed : secondaryMovementSpeed;
        float rotationSpeed = timeSinceStart <= speedChangeAfterSeconds ? initialRotationSpeed : secondaryRotationSpeed;
        float verticalSpeed = timeSinceStart <= speedChangeAfterSeconds ? initialVerticalSpeed : secondaryVerticalSpeed;

        UnityEngine.Vector3 worldSpaceMovement = transform.TransformDirection(linearMovement * movementSpeed);
        UnityEngine.Vector3 newPosition = rb.position + worldSpaceMovement * Time.fixedDeltaTime;

        UnityEngine.Quaternion deltaRotation = UnityEngine.Quaternion.Euler(0, angularRotation * rotationSpeed * Time.fixedDeltaTime, 0);
        UnityEngine.Quaternion newRotation = rb.rotation * deltaRotation;

        rb.MovePosition(newPosition);
        rb.MoveRotation(newRotation);
    }

    void OnDestroy()
    {
        try
        {
            rosConnector.RosSocket.Unsubscribe(subscriptionId);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Exception while unsubscribing: " + e.Message);
        }
    }
}