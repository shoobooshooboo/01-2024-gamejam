using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHolder : MonoBehaviour
{
    public bool playerFrozen = false;
    public float fearSlider = 0f;
    public float playerSpeed = 5f;

    private void FixedUpdate()
    {
        if (fearSlider > 1f)
        {
            fearSlider = 1f;
        }
        if (playerSpeed < 0f) 
        {
            playerSpeed = 0f;
        }
    }
}
