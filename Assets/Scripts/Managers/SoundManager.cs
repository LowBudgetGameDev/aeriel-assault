using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        ButtonPress,
        ButtonSelect,
        Die,
        Die2,
        Die3,
        Explode,
        Explode2,
        Explode3,
        Fly,
        Hit,
        Hit2,
        Hit3,
        Lose,
        Powerup,
        Powerup2,
        Powerup3,
        Shoot,
        Shoot2,
        Shoot3,
        Warning
    }

    public enum SoundType
    {
        Die,
        Explode,
        Hit,
        Powerup,
        Shoot
    }

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioMixerGroup audioMixerGroup;

    private AudioSource audioSource;

    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private Dictionary<SoundType, List<AudioClip>> soundTypeAudioClipDictionary;

    private float volume = 1f;

    private Dictionary<AudioClip, GameObject> stoppableSoundObjectDictionary;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        soundTypeAudioClipDictionary = new Dictionary<SoundType, List<AudioClip>>();
        stoppableSoundObjectDictionary = new Dictionary<AudioClip, GameObject>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
        {
            List<AudioClip> audioClipList = new List<AudioClip>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                if (sound.ToString().Contains(soundType.ToString())) audioClipList.Add(soundAudioClipDictionary[sound]);
            }

            soundTypeAudioClipDictionary[soundType] = audioClipList;
        }
    }

    public void PlaySound(Sound sound)
    {
        PlaySound(soundAudioClipDictionary[sound]);
    }

    public void PlaySoundType(SoundType soundType)
    {
        int randomIndex = Random.Range(0, soundTypeAudioClipDictionary[soundType].Count);

        PlaySound(soundTypeAudioClipDictionary[soundType][randomIndex]);
    }

    public void PlayStoppableSound(Sound sound, bool loop = false)
    {
        PlayStoppableSound(soundAudioClipDictionary[sound], loop);
    }

    public void StopStoppableSound(Sound sound)
    {
        StopStoppableSound(soundAudioClipDictionary[sound]);
    }

    private void PlaySound(AudioClip soundClip)
    {
        float pitch = Random.Range(0.8f, 1.3f);

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(soundClip, volume);
    }

    // Stoppable sounds are sounds that are able to be stopped midway through playing
    // Only the warning and fly sounds will be used here so this function will be simple
    private void PlayStoppableSound(AudioClip soundClip, bool loop)
    {
        GameObject stoppableSoundObject = new GameObject("StoppableSound", typeof(AudioSource));

        AudioSource source = stoppableSoundObject.GetComponent<AudioSource>();
        source.resource = soundClip;
        source.volume = volume;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;

        GameManager.Instance.OnGameStateChanged += (object sender, EventArgs e) =>
        {
            if (source == null) return;

            if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Paused)
            {
                source.Pause();
            }
            else
            {
                source.UnPause();
            }
        };

        source.Play();

        stoppableSoundObjectDictionary[soundClip] = stoppableSoundObject;
    }

    private void StopStoppableSound(AudioClip soundClip)
    {
        if (stoppableSoundObjectDictionary[soundClip] == null) return;

        stoppableSoundObjectDictionary[soundClip].GetComponent<AudioSource>().Stop();
        Destroy(stoppableSoundObjectDictionary[soundClip]);
    }
}
