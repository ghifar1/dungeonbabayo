using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    { 
        get
        {
            if(_instance == null)
            {
                Debug.Log("UI Manager is Null");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImage;
    public Text gemCountText;
    public Image[] healthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        // loop through lives
        for(int i = 0; i <= livesRemaining; i++)
        {
            // do nothing till we hit the max
            if(i == livesRemaining)
            {
                // hide this one
                healthBars[i].enabled = false;
            }
        }
        // i == lives remaining
        // hide one bar
    }
}
