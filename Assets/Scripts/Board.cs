using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public string robotArmTag = "Robot"; // Robot kolunun tag'�
    public string pipeColliderTag = "Pipe"; // Pipe collider'�n�n tag'�
    public GameObject robot;
    private Transform robotArm; // Robot kolunun transform'u
    private bool isBeingCarried = false; 
    private bool isMissionCompleted = false; 
    public GameObject kopekb1, kopekb2;

    private void Start()
    {
        robot = SubmarineController.Instance.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Giren objenin tag'�n� kontrol et
        if (other.CompareTag(robotArmTag) && !isMissionCompleted)
        {
            Debug.Log(("Triggered"));
            robotArm = other.transform;

            // Robot kolunun collider'�na girdiyse ta��ma i�lemini ba�lat
            isBeingCarried = true;

            // Item'� robot kolunun alt�na yerle�tir
            transform.SetParent(robot.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ��kan objenin tag'�n� kontrol et
        if (other.CompareTag(pipeColliderTag))
        {
            isMissionCompleted = true;
            Debug.Log(("Trigger exit"));
            // Pipe collider'�ndan ��karsa ta��ma i�lemini iptal et
            CancelCarry();
            kopekb1.SetActive(false);
            kopekb2.SetActive(true);
            
            
            
        }
    }

    private void CancelCarry()
    {
        // Ta��ma i�lemini iptal et
        isBeingCarried = false;

        // Item'� parent'�ndan ��kar
        transform.SetParent(null);

        // Robot kolunu null'a ayarla
        robotArm = null;
    }

}
