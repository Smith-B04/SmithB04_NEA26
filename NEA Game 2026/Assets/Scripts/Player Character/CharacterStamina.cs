//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control stamina of player character object

using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class CharacterStamina : MonoBehaviour
{
    public UnityEngine.UI.Image staminaBar;
    public float maxStamina;
    public float stamina;
    public float staminaTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stamina = maxStamina; //starts stamina at max value
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaTimer < 0)
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime * 100; //This will refill stamina bar from 0 to full in a standard amount of time for any computer
                loadBar();
            }
            else
            {
                stamina = maxStamina; //Ensures stamina doesn't go over max stamina
                loadBar();
            }
        }
        else
        {
            staminaTimer -= Time.deltaTime; //Removes time from the cool down to start refilling stamina
        }
    }

    //Reloads the stamina bar
    public void loadBar()
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }
}
