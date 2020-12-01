using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity can't show Dictionaries in the inspector so that's why I use an array and a privately hidden dictionary.
/// The first AudioSource component is the 3D SFX one, the second one is the 2D SFX one, the third one is the Music AudioSource.
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioMgr : Singleton<AudioMgr>
{
    [SerializeField] private SFXPair[] EditorSFX = new SFXPair[0];
    private readonly Dictionary<ESFX, AudioClip> AllSFX = new Dictionary<ESFX, AudioClip>();

    [SerializeField] private MusicPair[] EditorMusic = new MusicPair[0];
    private readonly Dictionary<EMusic, AudioClip> AllMusic = new Dictionary<EMusic, AudioClip>();

    /// <summary>
    /// Used for playing all 3D effects.
    /// </summary>
    private AudioSource SFXSource;

    /// <summary>
    /// Used for playing all 2D effects.
    /// </summary>
    private AudioSource SFXSource_2D;

    /// <summary>
    /// Used for playing all Music.
    /// </summary>
    private AudioSource MusicSource;

    private const float DefaultVolumes = 0.5f;

    public float MasterVolume
    {
        get { return AudioListener.volume; }
        set
        {
            if (AudioListener.volume == value) { return; }
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(Constants.Prefs.VolumeMaster, value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Value must be between 0f and 1f.
    /// </summary>
    public float SFXVolume
    {
        get { return SFXSource.volume; }
        set
        {
            if (SFXSource.volume == value) { return; }
            SFXSource.volume = value;
            PlayerPrefs.SetFloat(Constants.Prefs.VolumeSFX, value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Value must be between 0f and 1f.
    /// </summary>
    public float MusicVolume
    {
        get { return MusicSource.volume; }
        set
        {
            if (MusicSource.volume == value) { return; }
            MusicSource.volume = value;
            PlayerPrefs.SetFloat(Constants.Prefs.VolumeMusic, value);
            PlayerPrefs.Save();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        // Copy the SFX configuration into a lookup table and perform a safety check.
        for (int i = 0; i < EditorSFX.Length; i++)
        {
            if (EditorSFX[i].Clip == null) { throw new System.NullReferenceException(string.Format("The Clip for SFX {0} is null. Please assign it in the editor.", EditorSFX[i].SFX)); }
            AllSFX.Add(EditorSFX[i].SFX, EditorSFX[i].Clip);
        }
        // Copy the Music configuration into a lookup table and perform a safety check.
        for (int i = 0; i < EditorMusic.Length; i++)
        {
            if (EditorMusic[i].Clip == null) { throw new System.NullReferenceException(string.Format("The Clip for Music {0} is null. Please assign it in the editor.", EditorMusic[i].Music)); }
            AllMusic.Add(EditorMusic[i].Music, EditorMusic[i].Clip);
        }

        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length < 2) { throw new System.Exception("This class needs AudioSource x2."); }
        SFXSource = audioSources[0];
        SFXSource_2D = audioSources[1];
        MusicSource = audioSources[2];

        LoadVolumes();
    }

    private void LoadVolumes()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(Constants.Prefs.VolumeMaster, DefaultVolumes);
        SFXSource.volume = PlayerPrefs.GetFloat(Constants.Prefs.VolumeSFX, DefaultVolumes);
        MusicSource.volume = PlayerPrefs.GetFloat(Constants.Prefs.VolumeMusic, DefaultVolumes);
    }

    public void PlaySFX(ESFX sfx, Vector3 audioPosition)
    {
        AudioSource.PlayClipAtPoint(AllSFX[sfx], audioPosition);
    }

    public void PlaySFX_2D(ESFX sfx)
    {
        SFXSource_2D.PlayOneShot(AllSFX[sfx]);
    }

    public void StopAllSFX()
    {
        SFXSource.Stop();
    }

    public void StopEverything()
    {
        StopMusic();
        StopAllSFX();
    }

    public void PlayMusic(EMusic music)
    {
        MusicSource.clip = AllMusic[music];
        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }
}

public enum ESFX
{
    None = 0,
    GUI_Click,
    Cat_Death,
}

public enum EMusic
{
    None = 0,
}

[System.Serializable]
public struct SFXPair
{
    public ESFX SFX;
    public AudioClip Clip;
}

[System.Serializable]
public struct MusicPair
{
    public EMusic Music;
    public AudioClip Clip;
}