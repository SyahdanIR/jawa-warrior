using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;

    void Start()
    {
        int score1 = PlayerPrefs.GetInt("Score_Level_1", 0);
        int score2 = PlayerPrefs.GetInt("Score_Level_2", 0);
        int score3 = PlayerPrefs.GetInt("Score_Level_3", 0);

        int totalScore = score1 + score2 + score3;

        totalScoreText.text = "TOTAL SCORE : " + totalScore;
    }
}