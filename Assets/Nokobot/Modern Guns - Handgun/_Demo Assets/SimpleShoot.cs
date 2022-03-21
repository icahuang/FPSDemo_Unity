using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class SimpleShoot : MonoBehaviour
{
    [SerializeField] GameObject mark;


    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Shooting Sound")]
    private AudioSource audioPlayer;
    [SerializeField] private AudioClip shootingAudio;

    [Header("Settings")]
    [SerializeField] [Tooltip("Specify time to destory the casing object")] private float destroyTimer = 2f;
    [SerializeField] [Tooltip("Bullet Speed")]  private float shotPower = 500f;
    [SerializeField] [Tooltip("Casing Ejection Speed")] private float ejectPower = 150f;


    private Animator animator;


    void Start()
    {
        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        if (barrelLocation == null)
            barrelLocation = transform;

        audioPlayer = GetComponent<AudioSource>();
        //Debug.Log(audioPlayer == null);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1"))
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");

            /* 此处不用额外调用一次Shoot()。Q. 在哪里调用了？*/
            //Shoot(); 
        }

        // 按下鼠标右键时，进入aim动画；松开时恢复成normal动画
        if (Input.GetButtonDown("Aim"))
        {
            animator.SetBool("Aim", true);
            mark.SetActive(false);
        }
        if (Input.GetButtonUp("Aim"))
        {
            animator.SetBool("Aim", false);
            mark.SetActive(true);
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        audioPlayer.PlayOneShot(shootingAudio, 1.0f);

        fireCheck();

        if (muzzleFlashPrefab)
        {
            muzzle();
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    void fireCheck()
    {
        Ray rayOrigin = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        //if(Physics.Raycast(rayOrigin, Mathf.Infinity))
        //{
        //    Debug.Log("Raycast hit something!");
        //}

        // Output what we hit
        if (Physics.Raycast(rayOrigin, out hitInfo, 1000))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
        }

        /* Delete the one with Destrucable.cs */
        Destructable crate = hitInfo.transform.GetComponent<Destructable>();
        if (crate != null)
        {
            crate.DestroyCrate();
        }
    }

    void muzzle()
    {
        //Create the muzzle flash
        GameObject tempFlash;
        tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        //Destroy the muzzle flash effect
        Destroy(tempFlash, destroyTimer);
    }


    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
