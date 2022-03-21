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
        // 此找对象的方法是根据对象的名字查找的，不是很好，如果名字修改了就出bug了
        _ammoText = GameObject.Find("Ammo_Text").GetComponent<Text>();
        _coinIcon = GameObject.Find("Coin_Icon");
        _coinIcon.SetActive(false);
    }

    public void updateAmmo(int count)
    {
        _ammoText.text = "子弹数 " + count;
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