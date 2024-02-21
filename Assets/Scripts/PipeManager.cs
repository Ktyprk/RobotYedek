using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public Transform itemToMove; // Hareket ettirilecek item
    private bool isRobotInside = false; // Robot trigger i�inde mi?

    private void OnTriggerEnter(Collider other)
    {
        // Trigger alan�na giren objenin tag'�n� kontrol et
        if (other.CompareTag("Robot"))
        {
            isRobotInside = true;

            // Robot trigger i�ine girdi�inde itemi onunla birlikte hareket ettir
            if (itemToMove != null)
            {
                itemToMove.parent = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger alan�ndan ��kan objenin tag'�n� kontrol et
        if (other.CompareTag("Robot"))
        {
            isRobotInside = false;

            // Robot trigger'dan ��kt���nda itemi ay�r
            if (itemToMove != null)
            {
                itemToMove.parent = null;
            }
        }
    }
}
