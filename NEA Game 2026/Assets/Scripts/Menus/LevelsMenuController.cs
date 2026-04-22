//Created: Sprint 5
//Last Edited: Sprint 6
//Purpose: Control the buttons that lead to each level.

using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuController : MonoBehaviour
{

    public Button tutorial;
    public Button level1;
    public Button level2;
    public Button level3;
    private char[] levelsBeaten;

    // Update is called once per frame
    void Update()
    {
        // Check that all levels haven't been beaten which would cause the win screen to load
        levelsBeaten = PlayerPrefs.GetString("LevelsBeaten").ToCharArray();
        if (levelsBeaten.Length > 0)
        {
            // Cross out all beaten levels
            for (int i = 0; i < levelsBeaten.Length; i++)
            {
                switch (levelsBeaten[i])
                {
                    case 'T':
                        tutorial.enabled = false;
                        tutorial.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        break;
                    case '1':
                        level1.enabled = false;
                        level1.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        break;
                    case '2':
                        level2.enabled = false;
                        level2.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        break;
                    case '3':
                        level3.enabled = false;
                        level3.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        break;
                }
            }
        }
        if (levelsBeaten.Length >= 4)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }

    // Method called by each button with interchangable variable so only one method is required
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
