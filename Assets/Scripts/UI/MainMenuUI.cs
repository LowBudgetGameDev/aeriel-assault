using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;

    [SerializeField] private OptionsUI optionsUI;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainScene);
        });

        optionsButton.onClick.AddListener(() =>
        {
            optionsUI.Show();
            gameObject.SetActive(false);
        });

        optionsUI.OnClose += (object sender, EventArgs e) =>
        {
            gameObject.SetActive(true);
        };
    }
}
