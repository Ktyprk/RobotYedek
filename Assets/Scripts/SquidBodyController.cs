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

    private UnityEngine.Vector3 linearMovement = UnityEngine.Vector3.zero;
    private float angularRotation = 0.0f;
    private string subscriptionId;

    void Start()
    {
        subscriptionId = rosConnector.RosSocket.Subscribe<Twist>(topicName, ReceiveMessage);
    }

    private void ReceiveMessage(Twist message)
    {
        // Apply the speed multiplier to the movement and rotation values
        // Invert y-axis movement
        linearMovement = new UnityEngine.Vector3((float)message.linear.y, (float)message.linear.z, (float)message.linear.x) * movementSpeed;
        angularRotation = (float)message.angular.z * rotationSpeed;
    }

    void Update()
    {
        transform.Translate(linearMovement * Time.deltaTime);
        transform.Rotate(0, angularRotation * Time.deltaTime, 0);
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