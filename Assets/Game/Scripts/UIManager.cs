using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text _ammoText;

    private GameObject _coinIcon;

    // Start is called before the first frame update
    void Start()
    {
        //_ammoText = GetComponent<Text>();
        _ammoText = GameObject.Find("Ammo_Text").GetComponent<Text>();
        _coinIcon = GameObject.Find("Coin_Icon");
        _coinIcon.SetActive(false);
    }

    public void updateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void gainCoin()
    {
        _coinIcon.SetActive(true);
    }

    public void useCoin()
    {
        _coinIcon.SetActive(false);
    }
}