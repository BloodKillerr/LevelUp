using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherDefenceSkill : Skill
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give player skill's ability
            if (PlayerManager.instance != null)
            {
                PlayerManager.instance.player.GetComponent<PlayerStats>().armor.AddModifier(2);
            }

            return true;
        }

        return false;
    }

    public override void ResetSkill()
    {
        base.ResetSkill();

        for (int i=0; i<MyCurrentCount; i++)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().armor.RemoveModifier(2);
        }
        MyCurrentCount = 0;
    }
}
