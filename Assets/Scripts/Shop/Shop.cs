using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    // variable for currentItemSelected
    public int currentSelectedItem;
    public int currentItemCost;

    private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();

            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        // 0 = flame sword
        // 1 = boots of flight
        // 2 = key to castle
        Debug.Log("SelectItem() : " + item);

        // switch between item
        switch(item)
        {
            // case 0 and so forth
            case 0:
                UIManager.Instance.UpdateShopSelection(104);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-5);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-112);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    // Buy item method
    public void BuyItem()
    {
        // check if the player gems is greater than or equal to itemCost
        if (_player.diamonds >= currentItemCost)
        {
            // if it is, then awardItem (subtract cost from players gem)
            if(currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            _player.diamonds -= currentItemCost;
            Debug.Log("Purchased " + currentSelectedItem);
            Debug.Log("Remaining Gems " + _player.diamonds);
            shopPanel.SetActive(false);
        }
        // else cancel sale
        else
        {
            Debug.Log("You do not enough gems. Closing Shop");
            shopPanel.SetActive(false);
        }
    }
}
