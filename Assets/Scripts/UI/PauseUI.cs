using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("Music UI")]
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Button musicVolumeDownButton;
    [SerializeField] private Button musicVolumeUpButton;

    [Header("Sound UI")]
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private Button soundVolumeDownButton;
    [SerializeField] private Button soundVolumeUpButton;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += (object sender, EventArgs e) =>
        {
            if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Paused)
            {
                Show();
                SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonPress);
            }
            else
            {
                Hide();
                if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Playing) SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonPress);
            }
        };

        musicVolumeDownButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeMusicVolume(-1);
            musicVolumeText.SetText(AudioManager.Instance.GetMusicVolume().ToString());
        });

        musicVolumeUpButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeMusicVolume(1);
            musicVolumeText.SetText(AudioManager.Instance.GetMusicVolume().ToString());
        });

        soundVolumeDownButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeSoundVolume(-1);
            soundVolumeText.SetText(AudioManager.Instance.GetSoundVolume().ToString());
        });

        soundVolumeUpButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeSoundVolume(1);
            soundVolumeText.SetText(AudioManager.Instance.GetSoundVolume().ToString());
        });

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        soundVolumeText.SetText(AudioManager.Instance.GetSoundVolume().ToString());
        musicVolumeText.SetText(AudioManager.Instance.GetMusicVolume().ToString());
        Time.timeScale = 0f;

        AudioManager.Instance.SetLowpass(5000);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        if (GameManager.Instance.GetCurrentState() != GameManager.GameState.GameOver) AudioManager.Instance.SetLowpass();
    }
}
