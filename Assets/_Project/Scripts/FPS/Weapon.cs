using Deliverance;
using Deliverance.Gameplay.UI;
using Deliverance.InteractableObjects.Weapon;
using FWGameLib.Common.Audio;
using FWGameLib.Common.StateMachine;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public bool isActiveWeapon;
    public Animator animator;
    [SerializeField] public GameObject muzzleEffect;
    [SerializeField] public bool isADS;

    [SerializeField] public WeaponDataSO data;

    // Bullet
    [Header("Bullet")]
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;

    //Burst (probably don't need for this project, but will add just in case)
    [Header("Burst")]
    [SerializeField] public int burstBulletsLeft;

    // Spread
    [Header("Spread")]
    [SerializeField] public float hipSpreadIntensity;
    [SerializeField] public float ADSSpreadIntensity;
    private float spreadIntensity;

    // Reload
    [Header("Reload")]
    [SerializeField] public int bulletsLeft;

    private StateMachine weaponStateMachine;
    private IdleState idleState;
    private WeaponCooldownState weaponCooldownState;
    private ShootingState shootingState;
    private ReloadingState reloadingState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletsLeft = data.magazineSize;
        spreadIntensity = hipSpreadIntensity;
        InitializeWeaponStateMachine();
    }

    private void InitializeWeaponStateMachine()
    {
        weaponStateMachine = new StateMachine();

        idleState = new IdleState(this);
        weaponCooldownState = new WeaponCooldownState(this);
        shootingState = new ShootingState(this);
        reloadingState = new ReloadingState(this);

        weaponStateMachine.AddTransition(idleState, shootingState, idleState.CanTransitionShooting);
        weaponStateMachine.AddTransition(idleState, reloadingState, idleState.CanTransitionReloading);
        weaponStateMachine.AddTransition(shootingState, weaponCooldownState, () => true);
        weaponStateMachine.AddTransition(reloadingState, idleState, reloadingState.CanTransitionReloadCompleted);
        weaponStateMachine.AddTransition(weaponCooldownState, idleState, weaponCooldownState.CanTransitionIdleOutOfBullets);
        weaponStateMachine.AddTransition(weaponCooldownState, idleState, weaponCooldownState.CanTransitionIdleSingleShot);
        weaponStateMachine.AddTransition(weaponCooldownState, shootingState, weaponCooldownState.CanTransitionShootingContinueBurst);
        weaponStateMachine.AddTransition(weaponCooldownState, idleState, weaponCooldownState.CanTransitionIdleStoppedShootingAuto);
        weaponStateMachine.AddTransition(weaponCooldownState, shootingState, weaponCooldownState.CanTransitionShootingContinueAuto);
        weaponStateMachine.AddTransition(weaponCooldownState, reloadingState, weaponCooldownState.CanTransitionReloading);

        weaponStateMachine.SetState(idleState);
    }

    void Update()
    {
        if (isActiveWeapon)
        {
            weaponStateMachine.Tick();

            /*
            // // Our weapon is never pickupable so it won't have the outline script, but if we did want to have that would need this line
            // GetComponent<Outline>().enabled = false;
            */

            if (DeliveranceGameManager.Instance.InputSystem.weaponInteractions.ADS.WasPressedThisFrame())
            {
                EnterADS();
            }
            if (DeliveranceGameManager.Instance.InputSystem.weaponInteractions.ADS.WasReleasedThisFrame())
            {
                ExitADS();
            }

            // Update the ammo display
            new UpdateAmmoDisplayEvent(bulletsLeft, InventoryManager.Instance.CheckAmmoLeft()).Invoke();
        }
    }

    public void FireWeapon()
    {
        // Update ammo
        bulletsLeft--;
        if (data.shootingMode == ShootingMode.Burst)
        {
            burstBulletsLeft--;
        }

        // Visuals
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        AudioSystem.Instance.Play(data.shootingSound, firePoint);
        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else
        {
            animator.SetTrigger("RECOIL");
        }

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(shootingDirection));
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.bulletDamage = data.damage;
        bullet.maxLifetime = data.bulletMaxLifetime;
        bulletObject.GetComponent<Rigidbody>().AddForce(shootingDirection * data.bulletVelocity, ForceMode.Impulse);
    }

    public void ReloadStart()
    {
        AudioSystem.Instance.Play(data.reloadingSound, firePoint);
        animator.SetTrigger("RELOAD");
    }

    public void UpdateAmmoAfterReload()
    {
        int reserveAmmo = InventoryManager.Instance.CheckAmmoLeft();
        if (reserveAmmo + bulletsLeft > data.magazineSize)
        {
            int difference = data.magazineSize - bulletsLeft;
            InventoryManager.Instance.UpdateAmmo(-difference);
            bulletsLeft = data.magazineSize;
        }
        else
        {
            InventoryManager.Instance.UpdateAmmo(-bulletsLeft);
            bulletsLeft += reserveAmmo;
        }
    }

    public void PlayEmptyMagazine()
    {
        AudioSystem.Instance.Play(data.emptyMagazineSound, firePoint);
    }

    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        animator.ResetTrigger("exitADS");
        isADS = true;
        spreadIntensity = ADSSpreadIntensity;
    }

    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        animator.ResetTrigger("enterADS");
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
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0); // Add some spread to the shooting
    }
}
