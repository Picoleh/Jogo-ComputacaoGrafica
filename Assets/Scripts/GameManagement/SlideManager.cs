using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private List<SlideInfo> slides;
    [SerializeField] private Image bg;
    [SerializeField] private Image fade;
    [SerializeField] private AudioClip tonclayVoice;

    private int slideIndex = -1;

    private void OnEnable() {
        if (SaveManager.instance == null)
            SceneManager.LoadScene("MainMenu");

        StartCoroutine(StartIntroRoutine());
    }

    private IEnumerator StartIntroRoutine() {
        yield return new WaitForSeconds(0.1f);

        NextSlide();
    }

    private void NextSlide() {
        DialogueSystem.instance.OnDialogueEnd -= NextSlide;

        if (++slideIndex < slides.Count)
            StartCoroutine(PlaySlide(slides[slideIndex]));
        else
            StartCoroutine(MenuManager.instance.LoadSceneRoutine("Game", OnGameSceneLoaded));
    }

    private IEnumerator PlaySlide(SlideInfo slideInfo) {
        // Fade preto entra
        if(slideIndex != 0)
            yield return FadeIn(0.5f);

        // Troca imagem
        bg.sprite = slideInfo.image;
        // Fade preto sai revelando o slide
        yield return FadeOut(0.5f);

        // Mostra diálogo
        yield return new WaitForSeconds(1f);
        DialogueSystem.instance.StartDialogue("Tonclay", slideInfo.lines, tonclayVoice);
        DialogueSystem.instance.OnDialogueEnd += NextSlide;
    }

    private IEnumerator FadeIn(float duration) {
        float t = 0;
        Color c = fade.color;

        while (t < duration) {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / duration);
            fade.color = c;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float duration, System.Action onComplete = null) {
        float t = 0;
        Color c = fade.color;

        while (t < duration) {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / duration);
            fade.color = c;
            yield return null;
        }

        onComplete?.Invoke();
    }


    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
        InputMapManager.instance.GetInputReferences();
        LoadingScript.instance.HideLoadScreen();
        MenuManager.instance.CloseMenu();
    }
}
