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
            }
            else
            {
                Hide();
            }
        };

        musicVolumeDownButton.onClick.AddListener(() =>
        {
            VolumeManager.Instance.ChangeMusicVolume(-1);
            musicVolumeText.SetText(VolumeManager.Instance.GetMusicVolume().ToString());
        });

        musicVolumeUpButton.onClick.AddListener(() =>
        {
            VolumeManager.Instance.ChangeMusicVolume(1);
            musicVolumeText.SetText(VolumeManager.Instance.GetMusicVolume().ToString());
        });

        soundVolumeDownButton.onClick.AddListener(() =>
        {
            VolumeManager.Instance.ChangeSoundVolume(-1);
            soundVolumeText.SetText(VolumeManager.Instance.GetSoundVolume().ToString());
        });

        soundVolumeUpButton.onClick.AddListener(() =>
        {
            VolumeManager.Instance.ChangeSoundVolume(1);
            soundVolumeText.SetText(VolumeManager.Instance.GetSoundVolume().ToString());
        });

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        soundVolumeText.SetText(VolumeManager.Instance.GetSoundVolume().ToString());
        musicVolumeText.SetText(VolumeManager.Instance.GetMusicVolume().ToString());
        Time.timeScale = 0f;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
