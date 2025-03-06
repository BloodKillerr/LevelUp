using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private int skillID = 0;

    [SerializeField]
    private Image sprite;

    [SerializeField]
    private TMP_Text countText = null;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private int maxCount = 0;

    [SerializeField]
    private int currentCount = 0;

    [SerializeField]
    private bool unlocked = false;

    [SerializeField]
    private Skill[] childSkills = null;

    [SerializeField]
    private string Description;

    public int MyCurrentCount { get => currentCount; set => currentCount = value; }
    public bool MyUnlocked { get => unlocked; set => unlocked = value; }
    public int MyMaxCount { get => maxCount; }
    public TMP_Text MyCountText { get => countText; set => countText = value; }
    public int MySkillID { get => skillID; }
    public Image MySprite { get => sprite; set => sprite = value; }
    public TMP_Text MyNameText { get => nameText; set => nameText = value; }
    public string MyDescription { get => Description; set => Description = value; }

    private void Awake()
    {
        MySprite = gameObject.GetComponent<Image>();

        RefreshCount();

        if (MyUnlocked)
        {
            Unlock();
        }
    }

    public virtual bool Click()
    {
        if(MyCurrentCount < MyMaxCount && MyUnlocked)
        {
            MyCurrentCount++;
            RefreshCount();

            if(MyCurrentCount == MyMaxCount)
            {
                foreach(Skill childskill in childSkills)
                {
                    childskill.Unlock();
                }
            }
            return true;
        }

        return false;
    }

    public virtual void ResetSkill()
    {
        
    }

    public void Lock()
    {
        MyUnlocked = false;
        MySprite.color = Color.gray;

        if(MyCountText != null && MyNameText != null)
        {
            MyCountText.color = Color.gray;
            MyNameText.color = Color.gray;
            MyNameText.text = "???";
        } 
    }

    public void Unlock()
    {
        MyUnlocked = true;
        MySprite.color = Color.white;

        if(MyCountText != null && MyNameText != null)
        {
            MyCountText.color = Color.white;
            MyNameText.color = Color.white;
            MyNameText.text = MyDescription;
        }       
    }

    public void RefreshCount()
    {
        MyCountText.text = MyCurrentCount + "/" + MyMaxCount;
    }
}
