using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBars : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;

    public TMP_Text healthText;
    public TMP_Text manaText;

    PlayerStats playerStats;
    void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.MyCurrentHealth;

        manaSlider.maxValue = playerStats.maxMana;
        manaSlider.value = playerStats.currentMana;

        healthText.text = playerStats.MyCurrentHealth + "/" + playerStats.maxHealth;
        manaText.text = playerStats.currentMana + "/" + playerStats.maxMana;
    }
}
