using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject _WoodenCrateCracked;

    public void DestroyCrate()
    {
        Instantiate(_WoodenCrateCracked, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
