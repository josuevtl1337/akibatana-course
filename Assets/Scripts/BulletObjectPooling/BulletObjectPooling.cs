using NUnit.Framework;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public static BulletObjectPooling Instance;


    [Header("Bullet pool settings")]
    public int bulletPoolSize;
    public GameObject bulletPrefab;
    public System.Collections.Generic.List<GameObject> bulletList;

    void Awake()
    {
        Instance = this;

        CreateBulletObjectPool();
    }

    void CreateBulletObjectPool()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform);

            bulletList.Add(newBullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletList)
            if(!bullet.activeSelf)
            {
                return bullet;
            }
        return null;  
    }

}
