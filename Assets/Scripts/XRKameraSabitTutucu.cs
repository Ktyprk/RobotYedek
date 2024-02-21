using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class XRKameraSabitTutucu : MonoBehaviour
{
    // Sabitlenmiþ konum
    public Vector3 sabitKonum;

    // Kamera güncelleme sýklýðý (saniyede)
    public float kameraGuncellemeSikligi = 0.1f;

    // Baþlangýçta XR Origin pozisyonunu sabitle
    void Start()
    {
        // XR Origin nesnesini belirli bir konumda baþlat
        SabitKonumGuncelle();
    }

    // Belirli aralýklarla XR Origin pozisyonunu kontrol et
    void Update()
    {
        // XR Origin pozisyonunu belirli aralýklarla güncelle
        if (Time.time % kameraGuncellemeSikligi < Time.deltaTime)
        {
            SabitKonumGuncelle();
        }
    }

    // XR Origin pozisyonunu güncelle
    void SabitKonumGuncelle()
    {
        // XR Origin pozisyonunu sabit konumda tut
        transform.position = sabitKonum;

        // XR Origin nesnesini sabit konumda güncelle
        var xrOrigin = GetComponent<XROrigin>();
        if (xrOrigin != null)
        {
            xrOrigin.MatchOriginUpOriginForward(Vector3.up, Vector3.forward);
        }
    }
}

