using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Projectiles type Settings")]
    public bool useHitScanProjectiles;
    public Camera playerCamera;

    [Header("Weapon SO Settings")]
    public WeaponSO primaryWeapon;
    public WeaponSO secondaryWeapon;
    public Transform weaponModelContainer;

    [Header("Projectiles Settings")]
    public PhysicProjectile physicProjectile;
    public HitScanProjectile hitScanProjectile;

    [Header("Projectiles cadency Settings")]
    private float shootTimming;

    //[Header("Magazine Settings")]
    //public int currentWeaponSO.MagazineSize;

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

    private WeaponSO currentWeaponSO;
    private WeaponModel currentWeaponModel;



    void Start()
    {
        primaryWeapon = Instantiate(primaryWeapon);
        secondaryWeapon = Instantiate(secondaryWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        shootTimming += Time.deltaTime;
        if (reloading) { return; }

        if (Input.GetKeyDown("1")) {
            ChangeWeapon(primaryWeapon);
        }
        if (Input.GetKeyDown("2")) {
            ChangeWeapon(secondaryWeapon);
        }

        if (!currentWeaponSO) return;

        if (Input.GetKeyDown("r"))
        {
            reloading = true;
            animator.SetTrigger("Reload");
            HUDPlayerWeapon.Instance.UpdateReloading();
            audioSourceFXs.clip = reloadAudioClip;
            audioSourceFXs.Play();

            Invoke(nameof(Reload), currentWeaponSO.ReloadTime);

        }

        if (Input.GetButton("Fire1"))
        {
            if (shootTimming >= currentWeaponSO.Cadency && currentWeaponSO.BulletsOnMagazineSize > 0)
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
                currentWeaponSO.BulletsOnMagazineSize--;
                UpdateWeaponHUDInfo();
            }
          
        }
    }



    void ChangeWeapon(WeaponSO weapon)
    {
        if (currentWeaponModel)
        {
            Destroy(currentWeaponModel.gameObject);
        }
        WeaponModel weaponModel = Instantiate(weapon.WeaponModel, weaponModelContainer);
        currentWeaponSO = weapon;
        currentWeaponModel = weaponModel;
        canonPos = weaponModel.canonPos;
        animator.runtimeAnimatorController = currentWeaponSO.AnimatorOverrideController;
        UpdateWeaponHUDInfo();
    }
     
    void UpdateWeaponHUDInfo()
    {
        HUDPlayerWeapon.Instance.UpdateMagazineWeapon(currentWeaponSO.BulletsOnMagazineSize, currentWeaponSO.MaxBulletsOnMagazineSize);
    }

    void Reload()
    {
        currentWeaponSO.BulletsOnMagazineSize = currentWeaponSO.MaxBulletsOnMagazineSize;
        reloading = false;
        UpdateWeaponHUDInfo();
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
