using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Linq.Expressions;

public class RangedWeapon : WeaponBase, IShoot
{
    public static event EventHandler OnPlayerShoot;
    public static event EventHandler OnPlayerWeaponChange;

    public Texture2D weaponIcon;
    public Texture2D projectileIcon;
    public Texture2D ammoCountIcon;


    public Transform shootPoint;
    public int maxActiveProjectiles;
    public int maxAmmo;
    public int currentAmmo;


    private float time;

    private bool canShoot;

    public bool EnemyVersion;

    [SerializeField] private GameObject muzzleFlashObj;

    private ParticleSystem muzzleFlash;

    [SerializeField] private GameObject muzzleLight;

    private void Awake()
    {
        setStats();
        if (!projectilePrefab)
        {
            Debug.LogWarning("No projectile prefab found");
        }
    }
    void Start()
    {
        time = fireRate;
        //setStats();
        //if (!projectilePrefab)
        //{
        //    Debug.LogWarning("No projectile prefab found");
        //}

        muzzleFlash = muzzleFlashObj.GetComponentInChildren<ParticleSystem>();        

        muzzleFlash.gameObject.transform.position = shootPoint.position;

        currentAmmo = maxAmmo;
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        //reset fire rate WHEN shot
        if (time < fireRate)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = fireRate;
        }
    }

    GameObject item;
    public void Shoot()
    {
        //Instantiate projectile prefab that we have
        GameObject newProjectile = projectilePrefab;
        AudioManager.PlayClipAtPosition(stats.fireWeaponSound, shootPoint.position);


        Instantiate(newProjectile, shootPoint.position, shootPoint.rotation);
        //Instantiate(newProjectile, shootPoint.position, Quaternion.LookRotation(Vector3.up, gameObject.transform.forward));
        
        muzzleFlash.Play();
        StartCoroutine(flashEffector());
    }

    public void PlayerShoot()
    {
        //Instantiate projectile prefab that we have
        GameObject newProjectile = projectilePrefab;
        AudioManager.PlayClipAtPosition(stats.fireWeaponSound, shootPoint.position);

        Instantiate(newProjectile, shootPoint.position, shootPoint.rotation);

        currentAmmo--;
        //Instantiate(newProjectile, shootPoint.position, Quaternion.LookRotation(Vector3.up, gameObject.transform.forward));        

        OnPlayerShoot?.Invoke(this, EventArgs.Empty);

        muzzleFlash.Play();

        StartCoroutine(flashEffector());
    }

    IEnumerator flashEffector()
    {
        muzzleLight.gameObject.SetActive(true);
        yield return new WaitForSeconds(.1f);
        muzzleLight.gameObject.SetActive(false);
        yield return null;
    }

    public void GainAmmo(int amountToGain)
    {
        currentAmmo = (currentAmmo + amountToGain <= maxAmmo) ? currentAmmo + amountToGain : maxAmmo;
    }


    public void HandleShooting()
    {
        if (time >= fireRate)
        {
            time = 0.0f;
            Shoot();
        }
    }

    protected override void setStats()
    {
        if (!stats)
        {
            Debug.LogWarning("No stats found");
        }
        else
        {
            if (!EnemyVersion)
            {
                weaponName = stats.weaponName;
                fireRate = stats.fireRate;
                walkMultiplier = stats.walkMultiplier;
                projectilePrefab = stats.projectilePrefab;
                maxActiveProjectiles = stats.maxActiveAmount;

                weaponIcon = stats.weaponIcon;
                projectileIcon = stats.projectileIcon;
                ammoCountIcon = stats.ammoCountIcon;
                maxAmmo = stats.maxAmmo;
                currentAmmo = maxAmmo;
            }
            else
            {
                projectilePrefab = stats.projectilePrefab;
            }

        }
    }


}
