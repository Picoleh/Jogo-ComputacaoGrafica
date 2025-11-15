using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> loopClips;
    [SerializeField] private List<AudioClip> musicClips;
    public static SoundManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start() {
        musicSource.clip = musicClips[0];
        musicSource.Play();
    }

    public void PlaySFX(string clipName) {
        AudioClip clip = GetClipByName(clipName, sfxClips);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoop(string clipName) {
        AudioClip clip = GetClipByName(clipName, loopClips);
        if (loopSource.clip == clip && loopSource.isPlaying)
            return;

        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.Play();
    }

    public void StopLoop() {
        loopSource.Stop();
        loopSource.clip = null;
    }

    private AudioClip GetClipByName(string name, List<AudioClip> clips) {
        foreach (AudioClip clip in clips) { 
            if(clip.name == name) 
                return clip;
        }
        return null;
    }
}
