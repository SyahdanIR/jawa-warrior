using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighscoreSceneManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inputPanel;
    public TMP_InputField nameInput;
    public TextMeshProUGUI highscoreText;
    public GameObject leaderboardPanel; 
    public GameObject highscoreDisplay;
    public GameObject resetButton;
    public GameObject mainMenuButton; 

    private int totalScore;

    void Start()
    {
        // Ambil total skor
        totalScore = PlayerPrefs.GetInt("Score_Level_1", 0)
                   + PlayerPrefs.GetInt("Score_Level_2", 0)
                   + PlayerPrefs.GetInt("Score_Level_3", 0);

        // Cek apakah skor ini layak masuk 10 besar
        if (IsHighscore(totalScore))
        {
            inputPanel.SetActive(true);
        }
        else
        {
            inputPanel.SetActive(false);
            leaderboardPanel.SetActive(true);
            mainMenuButton.SetActive(true);
            resetButton.SetActive(true);
        }
    }

    public void Submit()
    {
        string playerName = nameInput.text;
        if (string.IsNullOrEmpty(playerName)) playerName = "Anonymous";

        HighscoreManager.AddHighscore(playerName, totalScore);

        inputPanel.SetActive(false);
        highscoreDisplay.GetComponent<HighscoreDisplay>().ShowHighscores();

        leaderboardPanel.SetActive(true);
        mainMenuButton.SetActive(true);
        resetButton.SetActive(true);
    }

    private bool IsHighscore(int score)
    {
        string json = PlayerPrefs.GetString("HighscoreTable", "{}");
        HighscoreList list = JsonUtility.FromJson<HighscoreList>(json);
        if (list == null || list.highscoreEntries == null) return true;

        if (list.highscoreEntries.Count < 10) return true;

        foreach (var entry in list.highscoreEntries)
        {
            if (score > entry.score) return true;
        }

        return false;
    }
    public static void ResetLeaderboard()
    {
        PlayerPrefs.DeleteKey("HighscoreTable");
        PlayerPrefs.Save();
    }
    public void ResetLeaderboardAndRefresh()
    {
        ResetLeaderboard();
        highscoreDisplay.GetComponent<HighscoreDisplay>().ShowHighscores();
    }
}
