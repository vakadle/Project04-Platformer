using UnityEngine;
using TMPro;  // pokud používáš TextMeshPro pro UI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Tooltip("TextMeshPro UI text pro skóre")]
    public TMP_Text scoreText;

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int pts)
    {
        score += pts;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}