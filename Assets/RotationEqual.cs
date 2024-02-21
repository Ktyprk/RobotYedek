using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEqual : MonoBehaviour
{
    public GameObject cameraObject, kol1, kol2;
    public float rotationSpeed = 5.0f;
    public Vector2 verticalRotationLimits = new Vector2(-45f, 45f);

    void FixedUpdate()
    {
        if (cameraObject != null && kol1 != null && kol2 != null)
        {
            // Kameranýn rotasyonunu al
            Quaternion targetRotation = cameraObject.transform.rotation;

            // Kol1'i döndür ve sýnýrla
            RotateWithLimits(kol1.transform, targetRotation, verticalRotationLimits);

            // Kol2'yi döndür ve sýnýrla
            RotateWithLimits(kol2.transform, targetRotation, verticalRotationLimits);
        }
        else
        {
            Debug.LogError("Bir veya daha fazla obje atanmamýþ.");
        }
    }

    void RotateWithLimits(Transform targetTransform, Quaternion targetRotation, Vector2 rotationLimits)
    {
        // Hedef rotasyonu sýnýrla
        targetRotation.eulerAngles = new Vector3(
            Mathf.Clamp(targetRotation.eulerAngles.x, rotationLimits.x, rotationLimits.y),
            targetRotation.eulerAngles.y,
            targetRotation.eulerAngles.z
        );

        // Yavaþ döndürme
        targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }
}
