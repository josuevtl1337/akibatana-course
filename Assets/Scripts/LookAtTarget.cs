using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Vector3 offset = new Vector3(1, 1, 0.6f);

    public Transform targetTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (targetTransform)
        {
            transform.LookAt(targetTransform);
            transform.position = transform.parent.position + transform.forward * offset.z;
        }
    }
}
