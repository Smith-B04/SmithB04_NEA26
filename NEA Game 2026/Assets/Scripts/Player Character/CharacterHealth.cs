//Created: Sprint 2
//Last Edited: Sprint 4
//Purpose: Control health of player character object

using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    public UnityEngine.UI.Image healthBar;
    public float maxHealth;
    public float health;
    public bool invincible;
    private Animator animator;

    // Dictionary of all the available damage types and how much to multiply the amount of health removed by
    private Dictionary<string, float> damageResistances = new Dictionary<string, float>
    {
        ["physical"] = 0.5f,
        ["fire"] = 1f,
        ["magic"] = 1f,
        ["lightning"] = 1f,
        ["holy"] = 1f,
        ["frost"] = 1f,
        ["poison"] = 1f,
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invincible = false; //starts character as not invincible
        health = maxHealth; //starts health at max value
        animator = this.GetComponent<Animator>(); //Get the animator
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Applies damage multiplied by resistance modifier unless character is currently invincible
    public void TakeDamage(int damage, string /*change to set type*/ damageType)
    {
        if (invincible != true)
        {
            this.GetComponent<CharacterActions>().busy = true;
            animator.SetTrigger("Hurt");
            health -= (float)(damage * damageResistances[damageType]);
            loadBar();
            StartCoroutine(finishTakeDamage());
        }
    }

    private IEnumerator finishTakeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<CharacterActions>().busy = false;
    }

    //Reloads the health bar
    public void loadBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
