using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public const string Mixer_Master = "MasterVolume";
    public const string Mixer_Music = "MusicVolume";
    public const string Mixer_SFX = "SFXVolume";
    

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.Master_Key, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.Music_Key, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.Sfx_Key, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.Master_Key, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.Music_Key, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.Sfx_Key, sfxSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(Mixer_Master, Mathf.Log10(value) * 20);
    }
    
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(Mixer_Music, Mathf.Log10(value) * 20);
    }
    
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(Mixer_SFX, Mathf.Log10(value) * 20);
    }

    public void SaveVolumeButton()
    {
        float masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        float musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        float sfxVolume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void Death()
    {
        sfxSlider.value = 0f;
    }

}
