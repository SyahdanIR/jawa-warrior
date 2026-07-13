using UnityEngine;
using UnityEngine.UI;

public class ScoreButtonUnlocker : MonoBehaviour
{
    public Button targetButton;
    public int requiredScore = 1000;

    void Update()
    {
        if (ScoreManager.Instance != null && targetButton != null)
        {
            if (ScoreManager.Instance.score >= requiredScore)
            {
                targetButton.interactable = true;
            }
            else
            {
                targetButton.interactable = false;
            }
        }
    }
}
