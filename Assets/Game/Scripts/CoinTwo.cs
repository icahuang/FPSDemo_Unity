using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTwo : MonoBehaviour
{
    //private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinPickUpAudio;

    //private void Start()
    //{
    //    _audioSource = GetComponent<AudioSource>();
    //    _audioSource.loop = false;
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            PlayerTwo player = other.GetComponent<PlayerTwo>();
            if (player != null)
            {
                player.getCoin();
                AudioSource.PlayClipAtPoint(_coinPickUpAudio, transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
