using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;
using Input = UnityEngine.Windows.Input;

public class Button11StateListener : MonoBehaviour
{
    
    public RosConnector rosConnector; // Set in Unity Editor
    public string topicName = "Squid_Body_Movement"; // The same ROS topic name as used in SquidBodyController

    private string subscriptionId;

    void Start()
    {
        // Subscribe to the same topic to listen for Twist messages
        subscriptionId = rosConnector.RosSocket.Subscribe<Twist>(topicName, MessageReceived);
    }

    private void MessageReceived(Twist message)
    {
        // Assuming angular.x is used to indicate the button [11] press state:
        // angular.x = 1 means button [11] is pressed, angular.x = 0 means not pressed.
        if (message.angular.x == 1)
        {
            CircleGrab.Instance.DropAction();
            Debug.Log("Button [11] is pressed.");
        }
        else if (message.angular.x == 0)
        {
            Debug.Log("Button [11] is not pressed.");
        }
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
        {
            CircleGrab.Instance.DropAction();
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the ROS topic when the GameObject is destroyed
        if (rosConnector != null && !string.IsNullOrEmpty(subscriptionId))
        {
            try
            {
                rosConnector.RosSocket.Unsubscribe(subscriptionId);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Exception while unsubscribing: {e.Message}");
            }
        }
    }

}
