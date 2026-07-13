using UnityEngine;
using System.Collections.Generic;

public static class HighscoreManager
{
    public static void AddHighscore(string name, int score)
    {
        string json = PlayerPrefs.GetString("HighscoreTable", "{}");
        HighscoreList list = JsonUtility.FromJson<HighscoreList>(json);
        if (list == null || list.highscoreEntries == null)
            list = new HighscoreList();

        list.highscoreEntries.Add(new HighscoreEntry(name, score));
        list.highscoreEntries.Sort((a, b) => b.score.CompareTo(a.score));

        if (list.highscoreEntries.Count > 10)
            list.highscoreEntries.RemoveRange(10, list.highscoreEntries.Count - 10);

        PlayerPrefs.SetString("HighscoreTable", JsonUtility.ToJson(list));
        PlayerPrefs.Save();
    }

}
