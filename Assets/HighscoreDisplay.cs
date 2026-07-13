using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighscoreDisplay : MonoBehaviour
{
    public Transform contentContainer; // Drag ke Content dalam Scroll View
    public GameObject highscoreItemPrefab; // Drag prefab item

    void Start()
    {
        ShowHighscores();
    }

    public void ShowHighscores()
    {
        string json = PlayerPrefs.GetString("HighscoreTable", "{}");
        HighscoreList list = JsonUtility.FromJson<HighscoreList>(json);

        if (list != null && list.highscoreEntries != null)
        {
            foreach (Transform child in contentContainer)
                Destroy(child.gameObject); // Bersihkan dulu

            foreach (var entry in list.highscoreEntries)
            {
                GameObject item = Instantiate(highscoreItemPrefab, contentContainer);

                TMP_Text nameText = item.transform.Find("NameText").GetComponent<TMP_Text>();
                TMP_Text scoreText = item.transform.Find("ScoreText").GetComponent<TMP_Text>();

                nameText.text = entry.name;
                scoreText.text = entry.score.ToString();
            }
        }
    }
}
