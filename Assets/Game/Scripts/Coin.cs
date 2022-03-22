using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinPickUpAudio;
    [SerializeField] string playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == playerTag && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.getCoin();
                // 直接使用类方法AudioSource.PlayClipAtPoint()播放，就不需要额外生成一个AudioSource对象
                AudioSource.PlayClipAtPoint(_coinPickUpAudio, transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
