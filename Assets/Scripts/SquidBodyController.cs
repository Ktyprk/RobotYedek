using System;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;

public class SquidBodyController : MonoBehaviour
{
    public RosConnector rosConnector; // Set in Unity Editor
    public string topicName = "Squid_Body_Movement"; // ROS topic name
    public float movementSpeed = 1.0f; // Multiplier for movement speed
    public float rotationSpeed = 1.0f; // Multiplier for rotation speed
    public float verticalSpeed = 1.0f; // Multiplier for vertical movement speed

    private Rigidbody rb; // Reference to the Rigidbody component
    private UnityEngine.Vector3 linearMovement = UnityEngine.Vector3.zero;
    private float angularRotation = 0.0f;
    private string subscriptionId;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ensure a Rigidbody component is attached to the GameObject
        subscriptionId = rosConnector.RosSocket.Subscribe<Twist>(topicName, ReceiveMessage);
    }

    private void ReceiveMessage(Twist message)
    {
        // Adjust linearMovement to include vertical movement based on angular.z
        // Assuming the D-Pad UP should move the object forward (along the z-axis in Unity),
        // and D-Pad RIGHT should move the object to the right (along the x-axis in Unity).
        // Here, angular.z is used for vertical movement.
        //linearMovement = new UnityEngine.Vector3((float)message.linear.y, (float)message.angular.z, (float)message.linear.x) * movementSpeed;
        linearMovement = new UnityEngine.Vector3((float)message.linear.y, (float)message.linear.z, (float)message.linear.x) * movementSpeed;

        // Handle rotation
        angularRotation = (float)message.angular.z * rotationSpeed; // Adjust rotation based on angular.z
        
    }

    void FixedUpdate()
    {
        // Transform linearMovement from local space to world space
        UnityEngine.Vector3 worldSpaceMovement = transform.TransformDirection(linearMovement);

        // Calculate the new position based on world-space movement vector
        UnityEngine.Vector3 newPosition = rb.position + worldSpaceMovement * Time.fixedDeltaTime;
        
        // Handle rotation: calculate the delta rotation
        UnityEngine.Quaternion deltaRotation = UnityEngine.Quaternion.Euler(0, angularRotation * Time.fixedDeltaTime, 0);
        UnityEngine.Quaternion newRotation = rb.rotation * deltaRotation;

        // Use Rigidbody methods to move and rotate, respecting physics interactions
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