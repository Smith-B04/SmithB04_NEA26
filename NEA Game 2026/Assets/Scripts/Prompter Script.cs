using System.Threading;
using TMPro;
using UnityEngine;

public class PrompterScript : MonoBehaviour
{
    public TextMeshProUGUI prompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerCharacter")
        {
            switch (this.name) 
            {
                case "left/right":
                    prompt.text = "You can move left and right by using the keys A and D. Press return to continue...";
                    break;
                case "jump":
                    prompt.text = "Jump by pressing space bar.";
                    break;
                case "sprint":
                    prompt.text = "Get places faster using left shift to sprint.";
                    break;
                case "settings":
                    prompt.text = "Press escape to pause the game so you can change your key binds.";
                    break;
                case "attack":
                    prompt.text = "There are enemies up ahead. Use left and right click to use your sword/bow to kill them.";
                    break;
                case "meleeEnemy":
                    prompt.text = "This melee enemy will run at you and attack. Make sure you press R to use your healing flasks.";
                    break;
                case "rangedEnemy":
                    prompt.text = "This ranged enemy will run away and throw axes. Make sure you use left ctrl to dodge its attacks.";
                    break;
                case "grace":
                    prompt.text = "This is a site of grace. It will heal you to full health and restore your healing flasks.";
                    break;
                case "DP":
                    prompt.text = "Interact with dormant powers using E to upgrade your stats.";
                    break;
                case "tree":
                    prompt.text = "Enter the spirit tree to finish the level.";
                    break;
            }

            //Time.timeScale = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerCharacter")
        {
            prompt.text = "";
            Time.timeScale = 1.0f;
        }
    }
}
