//Created: Sprint 5
//Last Edited: Sprint 5
//Purpose: Control the settings menu.

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
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation; // copied from docs

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false; // close canvas on startup

        if (playerInput != null) // find currently stored actions if already made and if not set to deafault
        {
            actions = playerInput.actions;
        }
        else
        {
            actions = defaultActions;
        }


    }

    // Method called by buttons for each action
    public void Rebind(string actionName) {
        InputAction action = actions.FindAction(actionName); // find the action attributed to the button
        if (action == null)
        {
            Debug.Log("not found");
            return;
        }

        prompt.text = "PRESS A KEY (ESC TO CANCEL)";

        action.Disable(); // turns of the action so it can't be performed whilst settings are changed though time should be frozen

        rebindingOperation = action.PerformInteractiveRebinding() // copied from docs
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation => Finish(action))
            .OnCancel(operation => Cancel(action))
            .Start();
    }

    // display effective bind, enables action and saves binds
    private void Finish(InputAction action)
    {
        prompt.text = "NEW BIND: " + action.bindings[0].effectivePath;
        action.Enable();

        rebindingOperation.Dispose(); //copied from docs
        SaveRebinds();
    }

    // enables action and displays cancellation 
    private void Cancel(InputAction action) 
    {
        prompt.text = "CANCELLED";
        action.Enable();

        rebindingOperation.Dispose();
    }

    // Saves the bindings in player prefs
    public void SaveRebinds()
    {
        string actionSaves =  actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", actionSaves);
    }

    // checks player pref exists and then rebinds everything to stored player pref
    public void LoadRebinds()
    {
        if (PlayerPrefs.HasKey("rebinds"))
        {
            actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds")); 
        }
    }

    // removes the canvas
    public void Close()
    {
        this.GetComponentInParent<Canvas>().enabled = false;
        Time.timeScale = 1.0f;
    }

    // changes difficulty and is called when slider is moved
    public void Difficulty()
    {
        int difficulty = (int)(difficultySlider.value); // get slider value
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
