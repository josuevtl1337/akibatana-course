using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Transform cameraTransform;
    public float interactionLenght;
    private RaycastHit hit;
    public GameObject detection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionLenght))
            {
                detection = hit.collider.gameObject;

                IInteraction interaction = detection.transform.root.GetComponent<IInteraction>();
                if (interaction != null)
                {
                    interaction.Interact();
                }

            }
        }

    }
}
