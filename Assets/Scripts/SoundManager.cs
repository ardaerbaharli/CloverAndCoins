using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource boughtPowerUp;
    [SerializeField] private AudioSource failed;
    [SerializeField] private AudioSource game1Background;
    [SerializeField] private AudioSource game2Background;
    [SerializeField] private AudioSource wonBonus;
    [SerializeField] private AudioSource outsideGameBackground;
    [SerializeField] private AudioSource usingPowerUp;
    [SerializeField] private AudioSource won;


    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetSound(PlayerPrefsX.GetBool("Sound", true));
    }


    public void SetSound(bool value)
    {
        PlayerPrefsX.SetBool("Sound", value);
        mixer.SetFloat("Master", value ? Config.MaxSoundVolume : -80);
    }


    public void PauseResume(bool value)
    {
    }

    public void PlayMenuSong()
    {
        // if already playing, return
        if (outsideGameBackground.isPlaying) return;
        outsideGameBackground.Play();
    }

    public void BoughtPowerUp()
    {
        boughtPowerUp.Play();
    }

    public void Failed()
    {
        failed.Play();
    }

    public void Won()
    {
        won.Play();
    }

    public void PauseMenuSong()
    {
        outsideGameBackground.Pause();
    }

    public void Play1of3Song()
    {
        // if already playing, return
        if (game1Background.isPlaying) return;
        game1Background.Play();
    }

    public void PlayBiggerSong()
    {
        // if already playing, return
        if (game2Background.isPlaying) return;
        game2Background.Play();
    }

    public void ActivatePowerUp()
    {
        usingPowerUp.Play();
    }

    public void PauseAllBackgroundSongs()
    {
        PauseMenuSong();
        PauseGame1Background();
        PauseGame2Background();
    }

    public void PauseGame2Background()
    {
        game2Background.Pause();
    }

    public void PauseGame1Background()
    {
        game1Background.Pause();
    }
}