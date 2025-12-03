

using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsController : MonoBehaviour
{
    public Slider difficultySlider;
    public PlayerInput playerInput;
    public InputActionAsset defaultActions;
    private InputActionAsset actions;
    public TextMeshProUGUI prompt;
    public TextMeshProUGUI currentDifficulty;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation; //copied from docs

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;

        if (playerInput != null)
        {
            actions = playerInput.actions;
        }
        else
        {
            actions = defaultActions;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rebind(string actionName) {
        InputAction action = actions.FindAction(actionName);
        if (action == null)
        {
            Debug.Log("not found");
            return;
        }

        prompt.text = "PRESS A KEY (ESC TO CANCEL)";

        action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding() //copied from docs
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation => Finish(action))
            .OnCancel(operation => Cancel(action))
            .Start();
    }

    private void Finish(InputAction action)
    {
        prompt.text = "NEW BIND: " + action.bindings[0].effectivePath;
        action.Enable();

        rebindingOperation.Dispose(); //copied from docs
        SaveRebinds();
    }

    private void Cancel(InputAction action) 
    {
        prompt.text = "CANCELLED";
        action.Enable();

        rebindingOperation.Dispose();
    }

    public void SaveRebinds()
    {
        string actionSaves =  actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", actionSaves);
    }

    public void LoadRebinds()
    {
        if (PlayerPrefs.HasKey("rebinds"))
        {
            actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds")); 
        }
    }

    public void Close()
    {
        this.GetComponentInParent<Canvas>().enabled = false;
        Time.timeScale = 1.0f;
    }

    public void Difficulty()
    {
        int difficulty = (int)(difficultySlider.value);
        switch (difficulty)
        {
            case 1:
                PlayerPrefs.SetInt("MaxHealth", 125);
                PlayerPrefs.SetInt("MaxStamina", 125);
                PlayerPrefs.SetInt("MaxFlasks", 4);
                PlayerPrefs.SetFloat("Physical", 0.75f);
                PlayerPrefs.SetFloat("Fire", 0.75f);
                PlayerPrefs.SetFloat("Magic", 0.75f);
                PlayerPrefs.SetInt("SwordDamage", 20);
                currentDifficulty.text = "DIFFICULTY: EASY";
                //Debug.Log("easy peasy");
                break;
            case 2:
                PlayerPrefs.SetInt("MaxHealth", 100);
                PlayerPrefs.SetInt("MaxStamina", 100);
                PlayerPrefs.SetInt("MaxFlasks", 3);
                PlayerPrefs.SetFloat("Physical", 1);
                PlayerPrefs.SetFloat("Fire", 1);
                PlayerPrefs.SetFloat("Magic", 1);
                PlayerPrefs.SetInt("SwordDamage", 15);
                currentDifficulty.text = "DIFFICULTY: MEDIUM";
                break;
            case 3:
                PlayerPrefs.SetInt("MaxHealth", 75);
                PlayerPrefs.SetInt("MaxStamina", 75);
                PlayerPrefs.SetInt("MaxFlasks", 2);
                PlayerPrefs.SetFloat("Physical", 1.25f);
                PlayerPrefs.SetFloat("Fire", 1.25f);
                PlayerPrefs.SetFloat("Magic", 1.25f);
                PlayerPrefs.SetInt("SwordDamage", 10);
                currentDifficulty.text = "DIFFICULTY: HARD";
                break;
        }

        PlayerPrefs.Save();
    }
}
