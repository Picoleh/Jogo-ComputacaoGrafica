using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> loopClips;
    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private List<AudioClip> ambientClips;

    private AudioClip lastMusic;
    private AudioClip lastAmbient;
    private bool musicTurn;
    public static SoundManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        lastMusic = null;
        lastAmbient = null;
        musicTurn = false;
    }

    private void Update() {
        if (!musicSource.isPlaying && !ambientSource.isPlaying) {
            if (musicTurn) {
                PlayMusic();
            }
            else {
                PlayAmbientSound();
            }
            musicTurn = !musicTurn;
        }
    }

    private void PlayAmbientSound() {
        if (ambientClips.Count == 1)
            lastAmbient = null;
        int r;
        do {
            r = Random.Range(0, ambientClips.Count);
        } while (ambientClips[r] == lastMusic);

        lastAmbient = ambientClips[r];
        ambientSource.clip = ambientClips[r];
        ambientSource.Play();
    }

    public void PlayMusic() {
        if(musicClips.Count == 1)
            lastMusic = null;
        int r;
        do {
            r = Random.Range(0, musicClips.Count);
        } while (musicClips[r] == lastMusic);

        lastMusic = musicClips[r];
        musicSource.clip = musicClips[r];
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
