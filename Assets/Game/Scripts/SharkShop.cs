using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private UIManager _uiManager;
    [SerializeField] private AudioClip _winSoundClip;

    private void Start()
    {
        
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player != null && player.hasCoin())
            {
                player.useCoin();
                player.enableWeapon();
                _uiManager.useCoin();
                AudioSource.PlayClipAtPoint(_winSoundClip, transform.position, 2f);
            }
        }
    }
}
