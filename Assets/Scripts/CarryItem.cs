using UnityEngine;
using UnityEngine.InputSystem;

public class CarryItem : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedObject;

    private InputAction interactAction;

    private void Start()
    {
        interactAction = ControlsManager.Instance.playerInput.actions.FindAction("Interact");
    }

    void Update()
    {
        // Eðer "Carry" etiketli bir nesne üzerine düþerse
        if (interactAction.WasPressedThisFrame())
        {
            if (carriedObject == null)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);

                foreach (var collider in hitColliders)
                {
                    if (collider.CompareTag("Carry")  || collider.CompareTag("SpotLight"))
                    {
                        Carry(collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                Drop();
            }
        }
    }

    void Carry(GameObject objToCarry)
    {
        isCarrying = true;
        carriedObject = objToCarry;

        // Nesneyi taþýyýcý objenin alt nesnesi olarak ayarla
        carriedObject.transform.parent = transform;
        carriedObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Drop()
    {
        isCarrying = false;

        // Nesneyi taþýyýcý objenin altýndan çýkar
        carriedObject.transform.parent = null;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;

        carriedObject = null;
    }
}
