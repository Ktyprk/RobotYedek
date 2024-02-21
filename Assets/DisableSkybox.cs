using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSkybox : MonoBehaviour
{
    void Start()
    {
        // Disable the skybox by assigning an empty material or transparent shader
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        GetComponent<Camera>().backgroundColor = Color.black; // Or any desired color
    }
}
