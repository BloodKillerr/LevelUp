using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    #region Singleton
    public static SkillTree instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        ResetTalents();
    }
    #endregion

    [SerializeField]
    private Skill[] skills = null;

    [SerializeField]
    private Skill[] unlockedByDefault = null;

    [SerializeField]
    private TMP_Text skillPointsText = null;

    public Skill[] MySkills { get => skills; set => skills = value; }

    private void Update()
    {
        UpdateSkillPointText();
    }

    public void TryUseSkill(Skill skill)
    {
        if(LevelingSystem.instance.skillPoints > 0 && skill.Click())
        {
            LevelingSystem.instance.skillPoints--;
        }
    }

    private void ResetTalents()
    {
        UpdateSkillPointText();

        foreach(Skill skill in MySkills)
        {
            skill.Lock();
        }

        foreach(Skill skill in unlockedByDefault)
        {
            skill.Unlock();
        }
    }

    public void UpdateSkillPointText()
    {
        if(LevelingSystem.instance != null)
        {
            skillPointsText.text = "Punkty umiejętności: " + LevelingSystem.instance.skillPoints;
        }       
    }
}
