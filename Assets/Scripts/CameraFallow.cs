using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform takipEdilenNesne;
    public float mesafe = 0f;
    public float yukseklik = 0f;
    public float yumusaklik = 2f;

    void LateUpdate()
    {
        if (takipEdilenNesne != null)
        {
            KamerayiTakipEt();
        }
    }

    void KamerayiTakipEt()
    {
        Vector3 hedefKonum = takipEdilenNesne.position + Vector3.up * yukseklik - takipEdilenNesne.forward * mesafe;

        transform.position = Vector3.Lerp(transform.position, hedefKonum, yumusaklik * Time.deltaTime);

        transform.LookAt(takipEdilenNesne.position);
    }
}
