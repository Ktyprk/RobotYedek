using UnityEngine;

public class MoveToPositionOnCollision : MonoBehaviour
{
    public Vector3[] targetPositions; // Array to hold potential target positions
    public float moveSpeed = 5f; // Speed at which the cube will move to the target position

    private Vector3 currentTargetPosition;
    private bool shouldMove = false;

    private void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.Lerp(transform.position, currentTargetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, currentTargetPosition) < 0.1f)
            {
                shouldMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Robot")) 
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        int randomIndex = Random.Range(0, targetPositions.Length);
        currentTargetPosition = targetPositions[randomIndex];
        shouldMove = true;
    }


}
