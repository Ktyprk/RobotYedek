using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public int targetFrameRate = 90;
    public GameObject Camera, cameraLock;
    private CameraFallow cameraFollowScript;
    private bool isCameraFollowActive = false;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        cameraFollowScript = cameraLock.GetComponent<CameraFallow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Kamera takip scripti etkinse kapat, deðilse aç
            isCameraFollowActive = !isCameraFollowActive;

            if (isCameraFollowActive)
            {
                // CameraFollow scriptini etkinleþtir
                cameraFollowScript.enabled = true;
            }
            else
            {
                // CameraFollow scriptini devre dýþý býrak
                cameraFollowScript.enabled = false;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Robot")
        {
           PostProcessLayer ppLayer = Camera.GetComponent<PostProcessLayer>();
            ppLayer.enabled = true;
        }
    }
}
