using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button startOverButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += (object sender, EventArgs e) =>
        {
            if (GameManager.Instance.GetCurrentState() == GameManager.GameState.GameOver) Show();
        };

        startOverButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainScene);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        AudioManager.Instance.SetLowpass(2000);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.SetLowpass();
    }
}
