using UnityEngine;

public class TentacleCreator : MonoBehaviour
{
    public GameObject bonePrefab;  // Drag your bone prefab here in Unity Editor
    public float totalLength = 0.6f;  // Total length is 60 cm
    public int totalBones = 601;

    // Define the start and end scales
    private Vector3 startScale = new Vector3(0.06f, 0.001f, 0.06f);  // Base bone dimensions
    private Vector3 endScale = new Vector3(0.02f, 0.001f, 0.02f);  // Last bone dimensions

    void Start()
    {
        // Create the main Tentacle GameObject
        GameObject tentacle = new GameObject("Tentacle");

        // Since the bone length is constant, we can define it outside the loop
        float boneLength = startScale.y;

        float cumulativeBoneLength = 0f;

        for (int i = 1; i <= totalBones; i++)
        {
            // Instantiate a bone
            GameObject bone = Instantiate(bonePrefab);
            
            // Set the bone's name
            bone.name = "Bone" + i;

            // Set the bone's scale
            float t = (float)(i - 1) / (totalBones - 1);
            bone.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // Position the bone along the tentacle
            cumulativeBoneLength += boneLength;
            bone.transform.position = new Vector3(0, 0, cumulativeBoneLength);

            // Make the bone a child of the Tentacle GameObject
            bone.transform.SetParent(tentacle.transform);
        }
    }
}

