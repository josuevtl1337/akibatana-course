using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Projectiles type Settings")]
    public bool useHitScanProjectiles;
    public Camera playerCamera;

    [Header("Projectiles Settings")]
    public PhysicProjectile physicProjectile;
    public HitScanProjectile hitScanProjectile;

    [Header("Projectiles cadency Settings")]
    public float shootCadency;
    private float shootTimming;

    [Header("Magazine Settings")]
    public int magazineSize = 30;
    public int currentMagazineSize;

    [Header("Reload Settings")]
    public float reloadTime;
    private bool reloading;


    [Header("Projectiles Pos Settings")]
    public Transform canonPos;
    public float projectileSpeed;

    private RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMagazineSize = magazineSize;

        if (HUDPlayerWeapon.Instance != null)
        {
            HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, magazineSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading) { return; }
        if (Input.GetKeyDown("r")) 
        {
            reloading = true;
            HUDPlayerWeapon.Instance.UpdateReloading();
            Invoke(nameof(Reload), reloadTime);

        }

        shootTimming += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (shootTimming>=shootCadency && currentMagazineSize>0)
            {
                if (useHitScanProjectiles)
                {

                    ShootHitScan();
                }
                else
                {
                    Shoot();
                }
                shootTimming = 0;
                currentMagazineSize--;
                HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, magazineSize);
            }
          
        }
    }
    void Reload()
    {
        currentMagazineSize=magazineSize;
        reloading = false;
        HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, magazineSize);
    }
    void ShootHitScan()
    {
        if (Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward, out hit, 100)) 
        {
            Health targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth) {
                targetHealth.ApplyDamage(25);
            }


            HitScanProjectile cloneObj = BulletObjectPooling.Instance.GetBullet().GetComponent<HitScanProjectile>();
            cloneObj.transform.position = canonPos.position;
            //HitScanProjectile cloneObj = Instantiate(hitScanProjectile, canonPos.position, canonPos.rotation);
            cloneObj.MoveProjectile(hit.point);
            cloneObj.gameObject.SetActive(true);
        }

    }

    void Shoot()
    {     
        GameObject cloneObj = Instantiate(physicProjectile, canonPos.position, canonPos.rotation).gameObject;
        cloneObj.GetComponent<Rigidbody>().AddForce(canonPos.forward * projectileSpeed);

    }
}
