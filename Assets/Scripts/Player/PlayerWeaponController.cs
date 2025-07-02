using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Projectiles type Settings")]
    public bool useHitScanProjectiles;
    public Camera playerCamera;

    [Header("Weapon SO Settings")]
    public WeaponSO currentWeaponSO;

    [Header("Projectiles Settings")]
    public PhysicProjectile physicProjectile;
    public HitScanProjectile hitScanProjectile;

    [Header("Projectiles cadency Settings")]
    private float shootTimming;

    [Header("Magazine Settings")]
    public int currentMagazineSize;

    [Header("Reload Settings")]
    private bool reloading;


    [Header("Projectiles Pos Settings")]
    public Transform canonPos;
    public float projectileSpeed;

    [Header("Hitscan settings")]
    public LayerMask hitLayerMask;
    private RaycastHit hit;

    [Header("Animator Settings")]
    public Animator animator;

    [Header("Sounds Settings")]
    public AudioSource audioSourceFXs;

    public AudioClip shootAudioClip;
    public AudioClip reloadAudioClip;

    private Health myHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myHealth = GetComponent<Health>();
        currentMagazineSize = currentWeaponSO.magazineSize;

        if (HUDPlayerWeapon.Instance != null)
        {
            HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, currentWeaponSO.magazineSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading) { return; }
        if (Input.GetKeyDown("r"))
        {
            reloading = true;
            animator.SetTrigger("Reload");
            HUDPlayerWeapon.Instance.UpdateReloading();
            audioSourceFXs.clip = reloadAudioClip;
            audioSourceFXs.Play();

            Invoke(nameof(Reload), currentWeaponSO.reloadTime);

        }

        shootTimming += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (shootTimming >= currentWeaponSO.cadency && currentMagazineSize>0)
            {
                if (useHitScanProjectiles)
                {

                    ShootHitScan();
                }
                else
                {
                    Shoot();
                }
                animator.SetTrigger("Shoot");

                audioSourceFXs.clip = shootAudioClip;
                audioSourceFXs.Play();

                shootTimming = 0;
                currentMagazineSize--;
                HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, currentWeaponSO.magazineSize);
            }
          
        }
    }
    void Reload()
    {
        currentMagazineSize = currentWeaponSO.magazineSize;
        reloading = false;
        HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentMagazineSize, currentWeaponSO.magazineSize);
    }
    void ShootHitScan()
    {
        if (Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward, out hit, 100, hitLayerMask)) 
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
