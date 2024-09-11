using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private GameObject muzzleEffect;

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
    [SerializeField] public float spreadIntensity;

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
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
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

        // Update the ammo display
        if (AmmoManager.Instance.ammoDisplay!= null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
        }
    }

    private void FireWeapon()
    {
        // decrease bullets left each time shot is fired
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManager.Instance.shootingSound.Play();
        
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
        bulletsLeft = magazineSize;
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

