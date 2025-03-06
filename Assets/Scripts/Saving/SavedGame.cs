using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelText = null;

    [SerializeField]
    private TMP_Text sceneText = null;

    [SerializeField]
    private TMP_Text HPText = null;

    [SerializeField]
    private TMP_Text MPText = null;

    [SerializeField]
    private TMP_Text ExpText = null;

    [SerializeField]
    private GameObject visuals = null;

    [SerializeField]
    private int index = 0;

    public int MyIndex { get => index; }

    public void ShowInfo(SaveData data)
    {
        visuals.SetActive(true);

        sceneText.text = string.Format("Poziom: {0}", data.MyScene);
        levelText.text = string.Format("Lvl: {0}", data.MyLevelingData.MyLevel);
        HPText.text = string.Format("HP: {0}/{1}", data.MyPlayerData.MyCurrentHealth, data.MyPlayerData.MyMaxHealth);
        MPText.text = string.Format("MP: {0}/{1}", data.MyPlayerData.MyCurrentMana, data.MyPlayerData.MyMaxMana);
        ExpText.text = string.Format("Exp: {0}/{1}", data.MyLevelingData.MyCurrentXp, data.MyLevelingData.MyRequiredXp);
    }

    public void HideVisuals()
    {
        visuals.SetActive(false);
    }
}
