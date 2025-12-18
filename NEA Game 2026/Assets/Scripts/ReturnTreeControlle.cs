using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTreeControlle : MonoBehaviour
{
    public string level;
    public TMPro.TextMeshProUGUI prompt;
    public CapsuleCollider2D playerCollider;
    private bool open;
    private bool interactable;
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = false;
        open = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            prompt.text = "Interact to return home...";
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            prompt.text = "";
            interactable = false;
        }
    }

    private void OnInteract()
    {
        if (interactable && !open)
        {
            interactable = false;
            PlayerPrefs.SetInt("MaxHealth", (int)(playerCollider.gameObject.GetComponent<CharacterHealth>().maxHealth));
            PlayerPrefs.SetInt("MaxStamina", (int)(playerCollider.gameObject.GetComponent<CharacterStamina>().maxStamina));
            PlayerPrefs.SetInt("MaxFlasks", (int)(playerCollider.gameObject.GetComponent<CharacterActions>().flasksRemaining));
            PlayerPrefs.SetFloat("Physical", (playerCollider.gameObject.GetComponent<CharacterHealth>().damageResistances["physical"]));
            PlayerPrefs.SetFloat("Fire", (playerCollider.gameObject.GetComponent<CharacterHealth>().damageResistances["fire"]));
            PlayerPrefs.SetFloat("Magic", (playerCollider.gameObject.GetComponent<CharacterHealth>().damageResistances["magic"]));
            PlayerPrefs.SetInt("SwordDamage", (int)(playerCollider.gameObject.GetComponent<CharacterActions>().swordDamage));
            PlayerPrefs.SetString("LevelsBeaten", PlayerPrefs.GetString("LevelsBeaten") + level);
            PlayerPrefs.Save();
            audioSource.Play();
            playerCollider.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            playerCollider.gameObject.GetComponent<CharacterActions>().busy = true;
            playerCollider.gameObject.GetComponent<CharacterMovement>().canMove = false;
            StartCoroutine(loadNewScene());
        }
    }

    private IEnumerator loadNewScene()
    {
        int newScore = PlayerPrefs.GetInt("Score") + 100;
        PlayerPrefs.SetInt("Score", newScore);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Levels Menu");
    }
}
