//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control stamina of player character object

using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;

public class CharacterStamina : MonoBehaviour
{
    public UnityEngine.UI.Image staminaBar;
    public float maxStamina;
    public float stamina;
    public float staminaTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaTimer < 0)
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime * 100;
                loadBar();
            }
            else
            {
                stamina = maxStamina;
                loadBar();
            }
        }
        else
        {
            staminaTimer -= Time.deltaTime;
        }
    }

    //Reloads the stamina bar
    public void loadBar()
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }
}
