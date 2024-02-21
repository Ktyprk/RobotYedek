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
        // Giren objenin tag'ýný kontrol et
        if (other.CompareTag(robotArmTag))
        {
            robotArm = other.transform;

            // Robot kolunun collider'ýna girdiyse taþýma iþlemini baþlat
            isBeingCarried = true;

            // Item'ý robot kolunun altýna yerleþtir
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
        // Taþýma iþlemini iptal et
        isBeingCarried = false;

        // Item'ý parent'ýndan çýkar
        transform.SetParent(null);

        // Robot kolunu null'a ayarla
        robotArm = null;
    }
}
