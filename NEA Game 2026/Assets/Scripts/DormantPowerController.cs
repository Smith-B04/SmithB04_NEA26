//Created: Sprint 4
//Last Edited: Sprint 5
//Purpose: Control Dormant Powerz.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DormantPowerController : MonoBehaviour
{
    public Canvas canvas;
    public TMPro.TextMeshProUGUI prompt;
    private bool interactable;
    public CapsuleCollider2D playerCollider;
    private bool open;
    private Button[] buttons;
    private bool active;

    public class UpgradeInfo
    {
        public string name { get; set; }
        public string description { get; set; }
        public string stat { get; set; }
        public float increase { get; set; }
    }

    private Dictionary<int, UpgradeInfo> UpgradeDict = new Dictionary<int, UpgradeInfo>
    {
        [0] = new UpgradeInfo { name = "Health Upgrade", description = "increases max health by 15%.", stat = "health", increase = 1.15f},
        [1] = new UpgradeInfo { name = "Stamina Upgrade", description = "increases max stamina by 30%.", stat = "stamina", increase = 1.30f },
        [2] = new UpgradeInfo { name = "Armour Upgrade", description = "increases physical resistance by 20%.", stat = "physical", increase = 1.20f },
        [3] = new UpgradeInfo { name = "Sword Upgrade", description = "increases sword damage by 40%.", stat = "sword", increase = 1.40f },
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = Instantiate(canvas, playerCollider.gameObject.transform);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        buttons = canvas.GetComponentsInChildren<Button>();
        canvas.enabled = false;
        interactable = false;
        open = false;
        active = true;

        for (int i = 0; i < buttons.Count(); i++)
        {
            buttons[i].onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (active)
            {
                prompt.text = "Interact";
            }
            interactable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            prompt.text = "";
            canvas.enabled = false;
            interactable = false;
            playerCollider.gameObject.GetComponent<CharacterActions>().busy = false;
        }
    }

    private void OnInteract()
    {
        if (interactable && !open && active)
        {
            open = true;
            canvas.enabled = true;
            prompt.text = "";
            playerCollider.gameObject.GetComponent<CharacterActions>().busy = true;
            for (int i = 0; i < buttons.Count(); i++)
            {
                if (buttons[i].name == "Unassigned")
                {
                    buttons[i].name = UpgradeDict[Random.Range(0, UpgradeDict.Count())].name;
                    for (int j = 0; j < UpgradeDict.Count(); j++)
                        if (UpgradeDict[j].name == buttons[i].name)
                            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = UpgradeDict[j].description;
                }
            }
        }
        else if (interactable && open) 
        {
            open = false;
            canvas.enabled = false;
            if (active)
            {
                prompt.text = "Interact";
            }
            playerCollider.gameObject.GetComponent<CharacterActions>().busy = false;
        }
    }

    public void OnClick()
    {
        // I got this line of code that finds the button clicked from this forum: https://discussions.unity.com/t/how-to-get-the-button-gameobject-when-it-is-clicked/649845/2
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;  
        for (int i = 0; i < UpgradeDict.Count(); i++) 
        {
            if (UpgradeDict[i].name == button.name)
            {
                switch (UpgradeDict[i].stat)
                {
                    case "health":
                        playerCollider.gameObject.GetComponent<CharacterHealth>().maxHealth *= UpgradeDict[i].increase;
                        playerCollider.gameObject.GetComponent<CharacterHealth>().loadBar();
                        break;
                    case "stamina":
                        playerCollider.gameObject.GetComponent<CharacterStamina>().maxStamina *= UpgradeDict[i].increase;
                        playerCollider.gameObject.GetComponent<CharacterStamina>().loadBar();
                        break;
                    case "physical":
                        playerCollider.gameObject.GetComponent<CharacterHealth>().damageResistances["physical"] *= (1/UpgradeDict[i].increase);
                        break;
                    case "sword":
                        playerCollider.gameObject.GetComponent<CharacterActions>().swordDamage *= UpgradeDict[i].increase;
                        break;
                    default:
                        break;
                }
            }
        }
        playerCollider.gameObject.GetComponent<CharacterActions>().busy = false;
        canvas.enabled = false;
        prompt.text = "";

        for (int i = 0; i < buttons.Count(); i++)
        {
            buttons[i].name = "Unassigned";
        }

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}