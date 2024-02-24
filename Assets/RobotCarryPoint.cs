using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCarryPoint : MonoBehaviour
{
    public static Transform carryPoint;

    private void Awake()
    {
        carryPoint = transform;
    }
}
