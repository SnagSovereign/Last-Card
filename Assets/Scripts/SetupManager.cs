using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupManager : MonoBehaviour {

    const string players = "players";
    const string cards = "cards";

    int defaultPlayers = 3;
    int defaultCards = 7;

    [SerializeField]
    Slider playerSlider, cardSlider;

    [SerializeField]
    TMP_Text playersValueText, cardsValueText;

    void Start()
    {
        SetDefaultValues();
        UpdateSliders();
        UpdateValueText();
    }

    void SetDefaultValues()
    {
        // if the player prefs have never been set, set them to the default values
        if (!PlayerPrefs.HasKey(players))
        {
            PlayerPrefs.SetInt(players, defaultPlayers);
        }

        if (!PlayerPrefs.HasKey(cards))
        {
            PlayerPrefs.SetInt(cards, defaultCards);
        }

        PlayerPrefs.Save();
    }

    void UpdateSliders()
    {
        playerSlider.value = PlayerPrefs.GetInt(players);
        cardSlider.value = PlayerPrefs.GetInt(cards);
    }

    public void UpdatePlayersPlayerPrefs()
    {
        PlayerPrefs.SetInt(players, (int)playerSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateCardsPlayerPrefs()
    {
        PlayerPrefs.SetInt(cards, (int)cardSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateValueText()
    {
        playersValueText.text = playerSlider.value.ToString();
        cardsValueText.text = cardSlider.value.ToString();
    }
}