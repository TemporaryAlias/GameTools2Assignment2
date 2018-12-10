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

    float comboCountdown;

    void Start() {
        LevelManager.instance.uiHandler = this;
    }

    void Update ()  {
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

}
