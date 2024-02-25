using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrabPipe : MonoBehaviour
{
    [SerializeField] private bool changeColor;
    [SerializeField] private Color validColor, invalidColor, defCircleColor;

    [SerializeField] private Transform placePos;

    private Renderer renderer;
    private Color defColor;

    private Renderer circleGrabRenderer;

    public bool isRobotArmColliding = false;

    private bool isFinished;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        if(renderer != null)
            defColor = renderer.material.color;
    }

    public void PlaceCircle(GameObject other)
    {
        if (changeColor)
        {
            circleGrabRenderer.material.color = validColor;
            renderer.material.color = validColor;
        }

        isFinished = true;

        other.transform.position = placePos != null ? placePos.position : transform.position;
        other.transform.rotation = Quaternion.Euler(placePos.rotation.eulerAngles + new Vector3(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        if (other.GetComponent<CircleGrab>() != null)
        {
            circleGrabRenderer = other.GetComponent<Renderer>();

            if (changeColor)
            {
                circleGrabRenderer.material.color = isRobotArmColliding ? invalidColor : validColor;
                renderer.material.color = isRobotArmColliding ? invalidColor : validColor;
            }
        }

        if (other.gameObject.tag == "Robot" && circleGrabRenderer != null)
        {
            if (changeColor)
            {
                isRobotArmColliding = true;

                circleGrabRenderer.material.color = invalidColor;
                renderer.material.color = invalidColor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFinished) return;

        if (other.GetComponent<CircleGrab>() != null)
        {
            circleGrabRenderer.material.color = defCircleColor;
            circleGrabRenderer = null;
        }

        if (other.gameObject.tag == "Robot")
        {
            isRobotArmColliding = false;

            if (changeColor)
            {
                if (circleGrabRenderer != null)
                {
                    circleGrabRenderer.material.color = validColor;
                    renderer.material.color = validColor;
                }
                else
                {
                    renderer.material.color = defColor;
                }
            }
        }
    }
}