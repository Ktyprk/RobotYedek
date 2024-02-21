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
            // Kameran�n rotasyonunu al
            Quaternion targetRotation = cameraObject.transform.rotation;

            // Kol1'i d�nd�r ve s�n�rla
            RotateWithLimits(kol1.transform, targetRotation, verticalRotationLimits);

            // Kol2'yi d�nd�r ve s�n�rla
            RotateWithLimits(kol2.transform, targetRotation, verticalRotationLimits);
        }
        else
        {
            Debug.LogError("Bir veya daha fazla obje atanmam��.");
        }
    }

    void RotateWithLimits(Transform targetTransform, Quaternion targetRotation, Vector2 rotationLimits)
    {
        // Hedef rotasyonu s�n�rla
        targetRotation.eulerAngles = new Vector3(
            Mathf.Clamp(targetRotation.eulerAngles.x, rotationLimits.x, rotationLimits.y),
            targetRotation.eulerAngles.y,
            targetRotation.eulerAngles.z
        );

        // Yava� d�nd�rme
        targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }
}
