using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHolder : MonoBehaviour
{
    public bool playerFrozen = false;
    public MainMenu mainMenu;
    public float fearSlider = 0f;
    public float playerSpeed = 5f;
    public int ghostLifeCount = 3;
    public int maxOuterLight = 5;
    public int maxInnerLight = 3;
    public bool monsterChase;
    public bool fwoosh;
    public int oilCount;
    public int defeatedCount;
    public int ghostsInPlace;
    public bool playerHit = false;

    private void Start()
    {
        fwoosh = false;
        monsterChase = false;
        defeatedCount = 0;
        oilCount = 1;
    }

    private void FixedUpdate()
    {
        if (fearSlider > 1f)
        {
            fearSlider = 1f;
            playerFrozen = true;
        }
        else if(fearSlider < 0f) {
            fearSlider = 0f;
        }
        if (playerSpeed < 0f) 
        {
            playerSpeed = 0f;
        }
        if(defeatedCount == 4) 
        {
            mainMenu.WIN();
        }
    }
}
