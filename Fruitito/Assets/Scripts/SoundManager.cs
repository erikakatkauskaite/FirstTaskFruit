using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private const string BACKGROUND_SOUND   = "Background";
    private const string WIN_SOUND          = "Win";
    private const string COLLECT_SOUND      = "Collect";

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
        Play(BACKGROUND_SOUND);

        Berry.OnCollected += PlayCollectedSound;
        GameManager.OnWin += PlayWinSounds;
    }

    private void PlayWinSounds()
    {
        Stop(BACKGROUND_SOUND);
        Play(WIN_SOUND);
    }

    private void  PlayCollectedSound()
    {
        Play(COLLECT_SOUND);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
