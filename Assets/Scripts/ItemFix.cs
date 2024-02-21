using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFix : MonoBehaviour
{
    public GameObject cameraLock, item, item2, particle1;
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
        if (this.gameObject.CompareTag("SpotLight"))
        {
            if (other.CompareTag("SpotLight"))
            {
                item.SetActive(true);
                item2.SetActive(false);
                
               
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.CompareTag("Carry"))
            {
                Debug.Log("AA");
            particle1.SetActive(true);
            return;
            }
    }
}
