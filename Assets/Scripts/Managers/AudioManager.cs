using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;

    // These can range from 0-10
    private int soundVolume;
    private int musicVolume;

    private void Awake()
    {
        Instance = this;

        soundVolume = PlayerPrefs.GetInt("SoundVolume", 10);
        musicVolume = PlayerPrefs.GetInt("MusicVolume", 10);
    }

    private void Start()
    {
        audioMixer.SetFloat("SoundVolume", VolumeToGain(soundVolume));
        audioMixer.SetFloat("MusicVolume", VolumeToGain(musicVolume));
    }

    public void ChangeSoundVolume(int amount)
    {
        soundVolume += amount;
        soundVolume = Mathf.Clamp(soundVolume, 0, 10);

        audioMixer.SetFloat("SoundVolume", VolumeToGain(soundVolume));

        PlayerPrefs.SetInt("SoundVolume", soundVolume);
    }

    public void ChangeMusicVolume(int amount)
    {
        musicVolume += amount;
        musicVolume = Mathf.Clamp(musicVolume, 0, 10);

        audioMixer.SetFloat("MusicVolume", VolumeToGain(musicVolume));

        PlayerPrefs.SetInt("MusicVolume", musicVolume);
    }

    public void SetLowpass(float cutoff = 22000f)
    {
        audioMixer.SetFloat("Lowpass", cutoff);
    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    private float VolumeToGain(int volume)
    {
        float clampedScaledVolume = Mathf.Clamp(volume / 10f, 0.001f, 1f);

        return Mathf.Log10(clampedScaledVolume) * 20f;
    }
}
