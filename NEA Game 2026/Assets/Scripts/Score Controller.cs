using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    // Update is called once per frame
    void Update()
    {
        textBox.text = "SCORE: " + (PlayerPrefs.GetInt("Score")).ToString() ;
    }
}
