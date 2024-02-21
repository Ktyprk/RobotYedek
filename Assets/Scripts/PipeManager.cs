using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public Transform itemToMove; // Hareket ettirilecek item
    private bool isRobotInside = false; // Robot trigger içinde mi?

    private void OnTriggerEnter(Collider other)
    {
        // Trigger alanýna giren objenin tag'ýný kontrol et
        if (other.CompareTag("Robot"))
        {
            isRobotInside = true;

            // Robot trigger içine girdiðinde itemi onunla birlikte hareket ettir
            if (itemToMove != null)
            {
                itemToMove.parent = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger alanýndan çýkan objenin tag'ýný kontrol et
        if (other.CompareTag("Robot"))
        {
            isRobotInside = false;

            // Robot trigger'dan çýktýðýnda itemi ayýr
            if (itemToMove != null)
            {
                itemToMove.parent = null;
            }
        }
    }
}
