using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float fadeTime = 10f;
    private float musicVolume = 0.6f;
    private float ambientVolume = 0.6f;

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
    private bool isFading;
    public static SoundManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        lastMusic = null;
        lastAmbient = null;
        isFading = false;
    }

    private void Start() {
        PlayMusic();
        musicSource.volume = 0;
        StartCoroutine(FadeIn(musicSource, musicVolume));
    }

    private void Update() {
        if (musicSource.isPlaying) {
            if (Mathf.RoundToInt(musicSource.clip.length) - Mathf.RoundToInt(musicSource.time) < fadeTime) {
                if (!isFading) {
                    StartCoroutine(FadeOut(musicSource));
                    PlayAmbientSound();
                    StartCoroutine(FadeIn(ambientSource, ambientVolume));
                }
            }
        }
        if (ambientSource.isPlaying) {
            if (Mathf.RoundToInt(ambientSource.clip.length) - Mathf.RoundToInt(ambientSource.time) < fadeTime) {
                if (!isFading) {
                    StartCoroutine(FadeOut(ambientSource));
                    PlayMusic();
                    StartCoroutine(FadeIn(musicSource, musicVolume));
                }
            }
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

    IEnumerator FadeOut(AudioSource source) {
        isFading = true;
        Debug.Log("FadeOut: " + source.name);
        float startVolume = source.volume;
        float t = 0f;

        while (t < fadeTime) {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();

        isFading = false;
    }

    IEnumerator FadeIn(AudioSource source, float volumeTarget) {
        isFading = true;
        Debug.Log("FadeIn: " + source.name);
        float startVolume = source.volume;
        float t = 0f;

        while (t < fadeTime) {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, volumeTarget, t / fadeTime);
            yield return null;
        }

        source.volume = volumeTarget;

        isFading = false;
    }

    public void SetConfig(VolumesData volumes) {
        musicVolume = volumes.musicVolume / 100f;
        musicSource.volume = musicVolume;
    }
}
