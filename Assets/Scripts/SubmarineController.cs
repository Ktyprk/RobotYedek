using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SubmarineController : MonoBehaviour
{
    public static SubmarineController Instance { get; private set; }

    public float movementSpeed = 5f;
    public float rotationSpeed = 100f;
    public float underwaterMovementSpeed = 2f;

    [SerializeField] private float minNightY, maxNightY;
    [SerializeField] private PostProcessVolume nightVolume;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        MovementControl();
        RotationControl();
        NightControl();
    }

    private void MovementControl()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    private void RotationControl()
    {
        float rotationAmount = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationAmount);
    }

    private void NightControl()
    {
        float percent = minNightY / Mathf.Clamp(transform.position.y, minNightY, maxNightY) * 2;
        nightVolume.weight = Mathf.Clamp(percent, 0, 1);
    }
}