using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    public ParticleSystem muzzleFLash;
    public float fireRate = 15f;
    public float damage;
    public GameObject impactEffect, enemyHitEffect;
    PlayerController player;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;

    public Animator anim;

    public Camera playerCam;
    private float nextTimeToShoot = 0f;
    public AudioClip reloadSound;
    //public Health ammoTest;
    public WeaponSwitcher ammoTest;


    AudioSource ak47;
    
    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<PlayerController>();

        ammoTest = GameObject.Find("WeaponHolder").GetComponent<WeaponSwitcher>();

        ak47 = GetComponent<AudioSource>();

        currentAmmo = maxAmmo;
        ammoTest.ammo = currentAmmo;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(Input.GetKeyDown(KeyCode.R))
{
            StartCoroutine(Reload());
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireRate;
           
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFLash.Play();
        ak47.Play();

        currentAmmo--;
        ammoTest.ammo = currentAmmo;
        Vector3 point = new Vector3(playerCam.pixelWidth / 2, playerCam.pixelHeight / 2, 0);
        Ray ray = playerCam.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            EnemyShot target = hitObject.GetComponent<EnemyShot>();
            if (target != null)
            {
                target.GotShot(damage);
                GameObject enemyBlood = Instantiate(enemyHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(enemyBlood, 2f);

            } else
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
           
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        anim.SetBool("Reloading", true);

        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(reloadSound);
        
        yield return new WaitForSeconds(reloadTime);

        anim.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        ammoTest.ammo = currentAmmo;
        isReloading = false;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = playerCam.pixelWidth / 2 - size / 3;
        float posY = playerCam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

}
