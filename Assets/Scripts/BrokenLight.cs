using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLight : MonoBehaviour
{
    public string robotArmTag = "Robot"; 
    public string Fixed = "SpotLight"; 
    public GameObject robot, fixedPart;
    private Transform robotArm; 
    private bool isBeingCarried = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Giren objenin tag'�n� kontrol et
        if (other.CompareTag(robotArmTag))
        {
            robotArm = other.transform;

            // Robot kolunun collider'�na girdiyse ta��ma i�lemini ba�lat
            isBeingCarried = true;

            // Item'� robot kolunun alt�na yerle�tir
            transform.SetParent(robot.transform);
        }

        if(other.CompareTag(Fixed) ) 
        {
           Destroy(this.gameObject);
            fixedPart.SetActive(true);

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
