using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource gameBackgroundSound;

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
        gameBackgroundSound.Play();
    }


    public void SetSound(bool value)
    {
        PlayerPrefsX.SetBool("Sound", value);
        mixer.SetFloat("Master", value ? Config.MaxSoundVolume : -80);
    }



    public void PauseResume(bool value)
    {
        if (value)
            gameBackgroundSound.UnPause();
        else
            gameBackgroundSound.Pause();
    }
}