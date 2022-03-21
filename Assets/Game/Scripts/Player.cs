using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Test button")]
    [SerializeField] public bool test = true;

    private CharacterController _controller;
    [SerializeField] private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField] private ParticleSystem _muzzleMesh;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private AudioSource _shootingAudio;

    private int _currentAmmo;
    [SerializeField] private int _maxAmmo = 50;
    private bool _hasGun;
    private bool _isReloading;
    private UIManager _uiManager;
    [SerializeField] bool _hasCoin;

    private GameObject _weapon;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _muzzleMesh.Stop();

        _hasGun = false;

        _isReloading = false;

        _uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        _uiManager.updateAmmo(_maxAmmo);
        _currentAmmo = _maxAmmo;

        _hasCoin = false;

        // 会自动添加tag为"Weapon"的对象到_weapon中
        _weapon = GameObject.FindGameObjectWithTag("Weapon");
        _weapon.SetActive(false);

        if (test == true)
        {
            _hasGun = true;
            _weapon.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            shoot();
        }
        else
        {
            _muzzleMesh.Stop();
            _shootingAudio.Stop();
        }

        if (Input.GetKey(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }

        // Press "Space" to hide/show mouse cursor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Cursor.visible.Equals(false))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        // Translate the local space to world space. So the movement will not be backwards even the rotation of axis y is 180 degree
        velocity = transform.transform.TransformDirection(velocity);

        //Vector3 velocity = direction;
        // In order to slow down the movement, need to multiply with (Time.deltaTime).
        // If we don't multiply velocity, the player will move 1 meter/sec.
        _controller.Move(velocity * Time.deltaTime);
    }

    void shoot()
    {
        /* 1. Raycast Part */
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        //if(Physics.Raycast(rayOrigin, Mathf.Infinity)) {
        //    Debug.Log("Raycast hit something!");
        //}

        // Output what we hit
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
        }


        if (_hasGun)
        {
            /* 2. Particle system part */
            _muzzleMesh.Play();

            /* 4. Play sound */
            if (_shootingAudio.isPlaying == false)
            {
                _shootingAudio.Play();
            }

            /* 5. decrease ammo */
            _currentAmmo--;
            _uiManager.updateAmmo(_currentAmmo);

            /* 3. Hit Maker effect part */
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // delete the hitMaker after 1 sec
            Destroy(hitMarker, 1f);

            /* 6. Destrucable */
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    IEnumerator Reload()
    {
        // wait for 1.5s
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _uiManager.updateAmmo(_currentAmmo);
        _isReloading = false;
    }

    public void getCoin()
    {
        _hasCoin = true;
        _uiManager.gainCoin();
    }

    public void useCoin()
    {
        _hasCoin = false;
    }

    public bool hasCoin()
    {
        return _hasCoin;
    }

    public void enableWeapon()
    {
        _weapon.SetActive(true);
        _hasGun = true;
    }
}