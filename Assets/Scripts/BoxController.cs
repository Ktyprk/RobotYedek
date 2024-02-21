using System;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;

public class BoxController : MonoBehaviour
{
    public RosConnector rosConnector; // Set in Unity Editor
    public string topicName = "Squid_Body_Movement"; // ROS topic name
    public float movementSpeed = 1.0f; // Multiplier for movement speed
    public float rotationSpeed = 1.0f; // Multiplier for rotation speed

    public Rigidbody rigidbody; // Reference to the Rigidbody component

    private UnityEngine.Vector3 linearMovement = UnityEngine.Vector3.zero;
    private float angularRotation = 0.0f;
    private string subscriptionId;

    void Start()
    {
        // Ensure a Rigidbody is attached
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("SquidBodyController requires a Rigidbody component to be attached.");
            return;
        }

        // Subscribe to ROS topic
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
        if (angularRotation == 0.0f)
        {
            rigidbody.angularVelocity = UnityEngine.Vector3.zero; // Angular momentum durdurma
        }
        else
        {
            rigidbody.AddTorque(UnityEngine.Vector3.up * angularRotation * Time.deltaTime);
        }

        if (linearMovement == UnityEngine.Vector3.zero)
        {
            rigidbody.velocity = UnityEngine.Vector3.zero; // Lineer momentum durdurma
        }
        else
        {
            rigidbody.AddForce(linearMovement * Time.deltaTime, ForceMode.VelocityChange);
        }
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