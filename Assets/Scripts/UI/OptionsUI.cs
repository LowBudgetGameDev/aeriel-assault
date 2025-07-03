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

    public void Show()
    {
        gameObject.SetActive(true);

        soundVolumeText.SetText(VolumeManager.Instance.GetSoundVolume().ToString());
        musicVolumeText.SetText(VolumeManager.Instance.GetMusicVolume().ToString());
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
