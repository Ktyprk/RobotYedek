using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    public PlayerInput playerInput;

    private void Awake()
    {
        Instance = this;

        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
    }
}
