using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer masterMixer;

    [SerializeField, Header("Music")]
    private AudioMixerSnapshot[] _snapshots;

    [SerializeField, Range(0f, 2f)]
    private float fadeTime;

    [SerializeField, Header("Effects")]
    private AudioSource effectsSource;

    [SerializeField]
    private AudioClip[] effects;
    
    [SerializeField]
    private EffectProfile[] _effectProfiles;
    
    //================================================================================================================//
    
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        SetMusic(MUSIC.NONE, 0f);
        yield return null;
        SetMusic(MUSIC.DEFAULT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //================================================================================================================//

    public void SetMusic(MUSIC music)
    {
        SetMusic(music, fadeTime);

    }
    public void SetMusic(MUSIC music, float time)
    {
        switch (music)
        {
            case MUSIC.NONE:
                masterMixer.TransitionToSnapshots(_snapshots, new[] {1f, 0f}, time);
                break;
            case MUSIC.DEFAULT:
                masterMixer.TransitionToSnapshots(_snapshots, new[] {0f, 1f}, time);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(music), music, null);
        }
        
    }
    
    //================================================================================================================//

    public void PlaySoundEffect(SOUND effect, float volumeScale = 1f)
    {
        var index = (int) effect;

        var pitchRange = _effectProfiles[index].pitchRange;

        masterMixer.SetFloat("EffectPitch", Random.Range(pitchRange.x, pitchRange.y));
        effectsSource.PlayOneShot(effects[index], volumeScale);
    }
    
    //================================================================================================================//
    
    public void SetMasterVolume(float volume)
    {
        SetVolume("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        SetVolume("MusicVolume", volume);
    }
    public void SetEffectsVolume(float volume)
    {
        SetVolume("EffectVolume", volume);
    }

    private void SetVolume(string parameterName, float volume)
    {
        volume = Mathf.Clamp01(volume);
        masterMixer.SetFloat(parameterName, Mathf.Log10(volume) * 40);
    }

}

[Serializable]
public struct EffectProfile
{
    public string name;
    public Vector2 pitchRange;
}

public enum SOUND
{
    
}

public enum MUSIC
{
    NONE,
    DEFAULT
}