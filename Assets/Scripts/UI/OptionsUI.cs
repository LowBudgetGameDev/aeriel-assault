using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public event EventHandler OnClose;

    [Header("Music UI")]
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Button musicVolumeDownButton;
    [SerializeField] private Button musicVolumeUpButton;

    [Header("Sound UI")]
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private Button soundVolumeDownButton;
    [SerializeField] private Button soundVolumeUpButton;

    [Header("Close")]
    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            OnClose?.Invoke(this, EventArgs.Empty);
            Hide();
        });

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

    public void Show()
    {
        gameObject.SetActive(true);

        soundVolumeText.SetText(AudioManager.Instance.GetSoundVolume().ToString());
        musicVolumeText.SetText(AudioManager.Instance.GetMusicVolume().ToString());

        AudioManager.Instance.SetLowpass(5000);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.SetLowpass();
    }
}
