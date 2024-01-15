using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    public int maxOilCount;
    public PlayerInput playerInput;
    public int cooldownDuration = 250;
    public float maxOuterLight = 5;
    public float maxInnerLight = 5;
    public int fwooshSpeed = 10;
    public int dimSpeed = 50;
    public Transform ghost1;
    public Transform ghost2;
    public Transform ghost3;
    public Transform ghost4;
    public GameObject brightLamp;
    public GameObject dimLamp;
    public TextMeshProUGUI cooldownTimer;
    private BoolHolder boolHolder;
    private float defaultOuterLight;
    private float defaultInnerLight;
    private Light2D playerLight;
    private int cooldown;
    private Rigidbody2D rb;
    private int lightOutCount;
    private int regrow;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boolHolder = GetComponentInParent<BoolHolder>();
        cooldown = 0;
        playerLight = GetComponentInChildren<Light2D>();
        defaultInnerLight = playerLight.pointLightInnerRadius;
        defaultOuterLight = playerLight.pointLightOuterRadius;
        boolHolder.fwoosh = false;
    }

    void OnLantern()
    {
        if(boolHolder.oilCount > 0 && cooldown == 0 && boolHolder.fearSlider < 1f)
        {
            boolHolder.oilCount--;
            cooldown = cooldownDuration;
            boolHolder.fearSlider -= .125f;
            boolHolder.fwoosh = true;
            //Debug.Log("oil count: " + oilCount + ". cooldown duration: " + cooldownDuration);

        }
        else if (boolHolder.oilCount > 0)
        {
            //Debug.Log("on cooldown");
        }
        else
        {
            //Debug.Log("out of oil");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(boolHolder.playerHit);
        if (boolHolder.playerHit)
        {
            boolHolder.playerHit = false;
            lightOutCount = 30;
        }
        if (boolHolder.playerFrozen || lightOutCount > 0)
        {
            playerLight.pointLightInnerRadius -= (maxInnerLight) / 30;
            playerLight.pointLightOuterRadius -= (maxOuterLight) / 30;
            lightOutCount--;
            if(lightOutCount == 0)
            {
                regrow = 100;
                playerLight.pointLightInnerRadius = 0;
                playerLight.pointLightOuterRadius = 0;
            }
            return;
        }
        if (regrow > 0)
        {
            playerLight.pointLightInnerRadius += (defaultInnerLight) / 100;
            playerLight.pointLightOuterRadius += (defaultOuterLight) / 100;
            regrow--;
            return;
        }
        if(boolHolder.oilCount > 0)
        {
            brightLamp.SetActive(true);
        }
        else
        {
            brightLamp.SetActive(false);
        }
        if (boolHolder.oilCount > maxOilCount)
        {
            boolHolder.oilCount = maxOilCount;
        }
        if (cooldown > 0)
        {
            cooldownTimer.text = "" + ((cooldown / 50) + 1);
            cooldown--;
        }
        else
        {
            cooldownTimer.text = "";
        }
        if (boolHolder.fwoosh)
        {
            playerLight.pointLightInnerRadius += (maxInnerLight - defaultInnerLight) / 10;
            playerLight.pointLightOuterRadius += (maxOuterLight - defaultOuterLight) / 10;
            //only hit ghosts during fwoosh
            if(playerLight.pointLightOuterRadius >= maxOuterLight)
            {
                boolHolder.fwoosh = false;
            }
        }
        //don't need to check both inner and outer because they change at the same speed
        else if (playerLight.pointLightInnerRadius > defaultInnerLight)
        {
            playerLight.pointLightInnerRadius -= (maxInnerLight - defaultInnerLight) / 50;
            playerLight.pointLightOuterRadius -= (maxOuterLight - defaultOuterLight) / 50;
        }
    }
}
