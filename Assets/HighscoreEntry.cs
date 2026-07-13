using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighscoreEntry
{
    public string name;
    public int score;

    public HighscoreEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

[Serializable]
public class HighscoreList
{
    public List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();
}
