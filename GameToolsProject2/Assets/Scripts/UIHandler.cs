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

    [Space(5)]
    [Header("UI Variables")]

    public int playerMaxHP;
    public int playerCurrentHp, maxCombo, currentCombo;

    [SerializeField] GameObject comboUI, deflectUI;

    void Start() {
        LevelManager.instance.uiHandler = this;
    }

    void Update ()  {
        hpSlider.maxValue = playerMaxHP;
        comboSlider.maxValue = maxCombo;

        hpSlider.value = playerCurrentHp;
        comboSlider.value = currentCombo;

        hpText.text = playerCurrentHp + "/" + playerMaxHP;
        comboText.text = currentCombo + "/" + maxCombo;
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
                break;

        }
    }

}
