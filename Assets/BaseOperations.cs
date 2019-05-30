using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseOperations : MonoBehaviour
{
    public Button getHealButton,getBulletButton,getCoinButton;
    public int _currentCoin;


    private void Start()
    {
        getHealButton.onClick.AddListener(() => healButtonFunc());
        
        getBulletButton.onClick.AddListener(() => bulletButtonFunc());
        
        getCoinButton.onClick.AddListener(()=> coinButtonFunc());
    }


    // Update is called once per frame
    void Update()
    {
        _currentCoin = FindObjectOfType<GeneralController>().coin;
    }
    
    void coinButtonFunc()
    {
        getCoin();
    }
    
    void healButtonFunc(int _itemCoin = 15)
    {
        if (canBuy(_itemCoin))
        {
            if(getHeal())
                FindObjectOfType<GeneralController>().setCoin(-_itemCoin);
        }
    }
    
    void bulletButtonFunc(int _itemCoin = 5)
    {
        if (canBuy(_itemCoin))
        {
            getBullet();
            FindObjectOfType<GeneralController>().setCoin(-_itemCoin);

        }
    }
    
    bool canBuy(int _itemCoin)
    {
        if (_currentCoin >= _itemCoin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void getCoin(int amount = 50)
    {
        FindObjectOfType<GeneralController>().setCoin(amount);
    }
    
    void getBullet(int amount = 10)
    {
        FindObjectOfType<GeneralController>().setBullet(amount);
    }
    
    bool getHeal(float amount = 10f)
    {
        if (FindObjectOfType<GeneralController>().currentHealth < 100f)
        {
            FindObjectOfType<GeneralController>().setHealth(amount);
            return true;
        }
        else
        {
            return false;
        }

    }
    
    void healInBase()
    {
        if (FindObjectOfType<GeneralController>().currentHealth < 100)
        {
            FindObjectOfType<GeneralController>().setHealth(0.3f);
        }
    }
}