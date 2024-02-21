using UnityEngine;

public class FollowLight : MonoBehaviour
{
    public Transform target; // Assign this to the Bone601 of Arm2 transform in the inspector
    public Transform previousBone; // Assign this to the Bone600 of Arm2 transform in the inspector

    void LateUpdate()
    {
        if (target != null && previousBone != null)
        {
            // Set light position to match target
            transform.position = target.position;

            // Calculate the direction from Bone600 to Bone601
            Vector3 forwardDirection = target.position - previousBone.position;

            // Ensure we have a valid direction
            if (forwardDirection != Vector3.zero)
            {
                // Set the rotation of the light to look in this direction
                transform.rotation = Quaternion.LookRotation(forwardDirection);
            }
        }
    }
}

