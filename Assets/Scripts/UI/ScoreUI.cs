using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += (object sender, EventArgs e) =>
        {
            scoreText.text = "Score:" + ScoreManager.Instance.GetScore().ToString();
        };

        scoreText.text = "Score:" + ScoreManager.Instance.GetScore().ToString();
    }
}
