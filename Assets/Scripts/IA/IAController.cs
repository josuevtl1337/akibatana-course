using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform targetTransform;
    public float targetDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform)
        {
            agent.destination = targetTransform.position;
            targetDistance  = Vector3.Distance(targetTransform.position, transform.position);
        }
    }
}
