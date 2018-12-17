using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    [Header("UI Elements")]

    [SerializeField] Slider hpSlider;
    [SerializeField] Text hpText;

    [SerializeField] Slider comboSlider;
    [SerializeField] Text comboText;

    [SerializeField] Slider countdownSlider;
    [SerializeField] Text countdownText;

    [Space(5)]
    [Header("UI Variables")]

    public int playerMaxHP;
    public int playerCurrentHp, maxCombo, currentCombo;

    [SerializeField] GameObject comboUI, deflectUI;

    [Space(5)]
    [Header("Fade Settings")]
    
    [SerializeField] Image fadeImage;

    [SerializeField] float fadeTime;

    Color fadeColour;


    float comboCountdown;

    void Start() {
        LevelManager.instance.uiHandler = this;

        fadeColour = new Color(0, 0, 0, 1);
        StartCoroutine("FadeIn");
    }

    void Update ()  {
        if (LevelManager.instance.currentGameState != LevelManager.GameState.MENU) {
            hpSlider.maxValue = playerMaxHP;
            comboSlider.maxValue = maxCombo;
            countdownSlider.maxValue = LevelManager.instance.player.stats.comboTime;

            hpSlider.value = playerCurrentHp;
            comboSlider.value = currentCombo;
            countdownSlider.value = comboCountdown;

            hpText.text = playerCurrentHp + "/" + playerMaxHP;
            comboText.text = currentCombo + "/" + maxCombo;
            countdownText.text = comboCountdown.ToString();
        }
    }

    void FixedUpdate() {
        if (LevelManager.instance.currentGameState == LevelManager.GameState.COMBO) {
            comboCountdown -= Time.deltaTime;
            comboCountdown = (float)System.Math.Round(comboCountdown, 2);
        }
    }

    public void UIMode(LevelManager.GameState state) {
        switch (state) {

            case LevelManager.GameState.MENU:
                break;

            case LevelManager.GameState.DEFLECT:
                deflectUI.SetActive(true);
                comboUI.SetActive(false);
                break;

            case LevelManager.GameState.COMBO:
                deflectUI.SetActive(false);
                comboUI.SetActive(true);
                comboCountdown = countdownSlider.maxValue;
                break;

        }
    }

    public void StartFadeOut(int newSceneIndex) {
        StartCoroutine("FadeOut", newSceneIndex);
    }

    public void StartImageFade(Image image) {
        StartCoroutine("ImageFade", image);
    }

    public void StartTextFade(Text text) {
        StartCoroutine("TextFade", text);
    }

    public void StartImageFadeIn(Image image) {
        StartCoroutine("ImageFadeIn", image);
    }

    public void StarTextFadeIn(Text text) {
        StartCoroutine("TextFadeIn", text);
    }

    IEnumerator ImageFade(Image imageToFade) {
        imageToFade.gameObject.SetActive(true);

        Color imageColour = imageToFade.color;
        imageColour.a = 1;

        while (imageColour.a >= 0) {
            imageColour.a -= 0.025f;
            imageToFade.color = imageColour;

            yield return new WaitForSeconds(fadeTime);
        }

        imageToFade.gameObject.SetActive(false);
    }

    IEnumerator TextFade(Text textToFade) {
        textToFade.gameObject.SetActive(true);

        Color textColour = textToFade.color;
        textColour.a = 1;

        while (textColour.a >= 0) {
            textColour.a -= 0.025f;
            textToFade.color = textColour;

            yield return new WaitForSeconds(fadeTime);
        }

        textToFade.gameObject.SetActive(false);
    }

    IEnumerator ImageFadeIn(Image imageToFade) {
        imageToFade.gameObject.SetActive(true);

        Color imageColour = imageToFade.color;
        imageColour.a = 0;

        while (imageColour.a <= 1) {
            imageColour.a += 0.025f;
            imageToFade.color = imageColour;

            yield return new WaitForSeconds(fadeTime);
        }
    }

    IEnumerator TextFadeIn(Text textToFade) {
        textToFade.gameObject.SetActive(true);

        Color textColour = textToFade.color;
        textColour.a = 0;

        while (textColour.a <= 1) {
            textColour.a += 0.025f;
            textToFade.color = textColour;

            yield return new WaitForSeconds(fadeTime);
        }
    }

    IEnumerator FadeIn() {
        fadeImage.gameObject.SetActive(true);

        while (fadeColour.a >= 0) {
            fadeColour.a -= 0.025f;
            fadeImage.color = fadeColour;

            yield return new WaitForSeconds(fadeTime);
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(int newSceneIndex) {
        fadeImage.gameObject.SetActive(true);

        fadeColour.a = 0;

        fadeImage.color = fadeColour;

        while (fadeColour.a <= 1) {
            fadeColour.a += 0.025f;
            fadeImage.color = fadeColour;

            yield return new WaitForSeconds(fadeTime);
        }

        LevelManager.instance.ChangeScene(newSceneIndex);
    }

}
