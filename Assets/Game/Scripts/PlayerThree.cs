using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThree : MonoBehaviour
{
    [Header("Test button")]
    [SerializeField] [Tooltip("In test mode, the weapon will be shown without picking the coin")] public bool test = true;

    private float _gravity = 9.81f;

    private int _currentAmmo;
    [SerializeField] private int _maxAmmo = 50;
    private bool _hasGun;
    private bool _isReloading;
    private UIManager _uiManager;
    [SerializeField] private bool _hasCoin;

    private GameObject _weapon;

    // Start is called before the first frame update
    void Start() {
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
    void Update() {

        if(Input.GetKey(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(Reload());
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