//Created: Sprint 6
//Last Edited: Sprint 6
//Purpose: Control the score display.

using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    // Update is called once per frame
    void Update()
    {
        // display score
        textBox.text = "SCORE: " + (PlayerPrefs.GetInt("Score")).ToString() ;
    }
}
