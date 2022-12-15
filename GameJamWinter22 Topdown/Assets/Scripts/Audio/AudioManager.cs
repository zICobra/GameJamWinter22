using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public const string Master_Key = "masterVolume";
    public const string Music_Key = "musicVolume";
    public const string Sfx_Key = "sfxVolume";

    private void Awake()
    {
        LoadVolume();
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(Master_Key, 1f);
        float musicVolume = PlayerPrefs.GetFloat(Music_Key, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(Sfx_Key, 1f);
        mixer.SetFloat(VolumeSettings.Mixer_Master, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.Mixer_Music, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.Mixer_SFX, Mathf.Log10(sfxVolume) * 20);
    }
    
}

