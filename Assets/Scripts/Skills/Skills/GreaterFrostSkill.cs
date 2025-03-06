using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterFrostSkill : Skill
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give player skill's ability
            if (SpellSystem.instance != null)
            {
                SpellSystem.instance.MyFreezeRadius += 1f; 
            }

            return true;
        }

        return false;
    }

    public override void ResetSkill()
    {
        base.ResetSkill();

        for (int i = 0; i < MyCurrentCount; i++)
        {
            SpellSystem.instance.MyFreezeRadius -= 1f;
        }
        MyCurrentCount = 0;
    }
}
