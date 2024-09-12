using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] public bool isActiveWeapon;
    private Animator animator;
    [SerializeField] private GameObject muzzleEffect;
    [SerializeField] private bool isADS;

    // Bullet
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletPrefabLifetime;
    
    // Shooting
    [Header("Shooting")]
    [SerializeField] private bool isShooting;
    [SerializeField] private bool readyToShoot;
    bool allowReset = true;
    [SerializeField] public float shootingDelay;
    [SerializeField] public enum ShootingMode 
    {
        Single,
        Burst,
        Auto
    }
    [SerializeField] private ShootingMode currentShootingMode;

    //Burst (probably don't need for this project, but will add just in case)
    [Header("Burst")]
    [SerializeField] public int bulletsPerBurst;
    [SerializeField] private int burstBulletsLeft;

    // Spread
    [Header("Spread")]
    [SerializeField] public float hipSpreadIntensity;
    [SerializeField] public float ADSSpreadIntensity;
    private float spreadIntensity;

    // Reload
    [Header("Reload")]
    [SerializeField] public float reloadTime;
    [SerializeField] private int magazineSize;
    [SerializeField] private int bulletsLeft;
    [SerializeField] private bool isReloading;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        spreadIntensity = hipSpreadIntensity;
    }

    void Update()
    {
        if (isActiveWeapon)
        {
            // // Our weapon is never pickupable so it won't have the outline script, but if we did want to have that would need this line
            // GetComponent<Outline>().enabled = false;

            // Sound
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSound.Play();
            }

            if (currentShootingMode == ShootingMode.Auto) 
            {
                // Holding Down Left Mouse Button
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                // Clicking Left Mouse Button Once for single/burst shot
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            // Clicking Right Mouse Button to Reload
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading && InventoryManager.Instance.CheckAmmoLeft() > 0)
            {
                Reload();
            }

            // // Automatic reload when magazine is empty
            // if (readyToShoot && !isShooting && isReloading == false && bulletsLeft <= 0)
            // {
            //     Reload();
            // }

            // shooting weapon
            if (isShooting && readyToShoot && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            if (Input.GetMouseButtonDown(1))
            {
                EnterADS();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                ExitADS();
            }

            // Update the ammo display
            if (AmmoManager.Instance.ammoDisplay!= null)
            {
                AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{InventoryManager.Instance.CheckAmmoLeft()}";
            }
        }
    }

    private void FireWeapon()
    {
        // decrease bullets left each time shot is fired
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        
        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else if (!isADS)
        {
            animator.SetTrigger("RECOIL");

        }

        SoundManager.Instance.PlayShootingSound();
        
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Point bullet to face the shooting direction
        bullet.transform.position = shootingDirection;

        // Shoot the bullet forward with the specified velocity
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        
        //Destroy the bullet after a specified lifetime
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private void Reload()
    {
        SoundManager.Instance.reloadingSound.Play();
        animator.SetTrigger("RELOAD");
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        isReloading = false;
        isADS = false;
        if (InventoryManager.Instance.CheckAmmoLeft() + bulletsLeft > magazineSize)
        {
            // InventoryManager.Instance.UpdateAmmo(-(magazineSize - bulletsLeft/bulletsPerBurst));
            InventoryManager.Instance.UpdateAmmo(-(magazineSize-bulletsLeft));
            bulletsLeft = magazineSize;
        }
        else 
        {
            int leftoverAmmo = InventoryManager.Instance.CheckAmmoLeft();
            InventoryManager.Instance.UpdateAmmo(-bulletsLeft);
            bulletsLeft += leftoverAmmo;
        }
    }

    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        isADS = true;
        spreadIntensity = ADSSpreadIntensity;
    }

    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        spreadIntensity = hipSpreadIntensity;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        // Shooting direction from the middle of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hiting Something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - firePoint.position;

        // Adding random spread
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0); // Add some spread to the shooting
    }
    
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float lifetime) 
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(bullet); // Destroy the bullet when it reaches its lifetime
    }
    
}

