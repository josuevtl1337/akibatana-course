using TMPro;
using UnityEngine;

public class HitScanProjectile : MonoBehaviour
{
    public float projectileSpeed;
    private Vector3 movePos;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        distance= Vector3.Distance(transform.position, movePos);

        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, projectileSpeed * Time.deltaTime);
        }
        else {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }

    public void MoveProjectile(Vector3 pos ) 
    {
        movePos = pos;
        
    }
}
