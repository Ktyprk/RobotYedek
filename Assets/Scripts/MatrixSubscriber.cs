using RosSharp.RosBridgeClient;
using UnityEngine;
using System.Collections.Generic;
using std_msgs = RosSharp.RosBridgeClient.MessageTypes.Std;

public class MatrixSubscriber : MonoBehaviour
{
    private RosSocket rosSocket;
    public string topic = "/matrix_data";
    public int UpdateTime = 1;

    public GameObject tentacle; // Reference to the "Tentacle" GameObject
    private GameObject[] bones; // Array to store all the bone GameObjects

    private List<Vector3> points = new List<Vector3>(); // Unity's Vector3 type to store the points

    void Start()
    {
        rosSocket = transform.GetComponent<RosConnector>().RosSocket;
        rosSocket.Subscribe<std_msgs.Float32MultiArray>(topic, ReceiveMessage);
        InvokeRepeating("UpdateMessage", 1, UpdateTime);

        // Initialize bone GameObject array and populate it
        bones = new GameObject[tentacle.transform.childCount];
        for (int i = 0; i < tentacle.transform.childCount; i++)
        {
            bones[i] = tentacle.transform.GetChild(i).gameObject;
        }
    }

    private void UpdateMessage()
    {
        Quaternion prefabOrientationOffset = Quaternion.Euler(90, 0, 0); // 90-degree offset on the x-axis

        // Update all bone GameObjects
        for (int i = 0; i < Mathf.Min(bones.Length, points.Count) - 1; i++)  // subtract 1 to avoid out-of-range error
        {
            // Calculate the new position and direction in world space
            Vector3 position = tentacle.transform.TransformPoint(points[i]);
            Vector3 directionToNext = tentacle.transform.TransformPoint(points[i + 1]) - position;

            bones[i].transform.position = position;
            bones[i].transform.rotation = Quaternion.LookRotation(directionToNext) * prefabOrientationOffset;
        }

        // Handle the last bone separately since it doesn't have a 'next' bone
        if (bones.Length == points.Count)
        {
            bones[bones.Length - 1].transform.position = tentacle.transform.TransformPoint(points[points.Count - 1]);
            // Keep its rotation the same as the second last bone or handle separately if needed
            bones[bones.Length - 1].transform.rotation = bones[bones.Length - 2].transform.rotation;
        }
    }

    private void ReceiveMessage(std_msgs.Float32MultiArray message)
    {
        float[] data = message.data;
        points.Clear();

        for (int i = 0; i < data.Length; i += 3)
        {
            Vector3 point = new Vector3(data[i + 1], data[i + 2], data[i]); // Translate ROS coordinates to Unity coordinates
            points.Add(point);
        }
    }
}

