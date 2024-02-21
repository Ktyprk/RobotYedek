using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class XRKameraSabitTutucu : MonoBehaviour
{
    // Sabitlenmi� konum
    public Vector3 sabitKonum;

    // Kamera g�ncelleme s�kl��� (saniyede)
    public float kameraGuncellemeSikligi = 0.1f;

    // Ba�lang��ta XR Origin pozisyonunu sabitle
    void Start()
    {
        // XR Origin nesnesini belirli bir konumda ba�lat
        SabitKonumGuncelle();
    }

    // Belirli aral�klarla XR Origin pozisyonunu kontrol et
    void Update()
    {
        // XR Origin pozisyonunu belirli aral�klarla g�ncelle
        if (Time.time % kameraGuncellemeSikligi < Time.deltaTime)
        {
            SabitKonumGuncelle();
        }
    }

    // XR Origin pozisyonunu g�ncelle
    void SabitKonumGuncelle()
    {
        // XR Origin pozisyonunu sabit konumda tut
        transform.position = sabitKonum;

        // XR Origin nesnesini sabit konumda g�ncelle
        var xrOrigin = GetComponent<XROrigin>();
        if (xrOrigin != null)
        {
            xrOrigin.MatchOriginUpOriginForward(Vector3.up, Vector3.forward);
        }
    }
}

