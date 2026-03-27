//
//
//

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
        levelsBeaten = PlayerPrefs.GetString("LevelsBeaten").ToCharArray();
        if (levelsBeaten.Length > 0)
        {
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

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
