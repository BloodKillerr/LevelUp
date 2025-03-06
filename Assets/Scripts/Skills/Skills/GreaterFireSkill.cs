using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterFireSkill : Skill
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give player skill's ability
            if (SpellSystem.instance != null)
            {
                SpellSystem.instance.MyFireballDamage += 5;
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
            SpellSystem.instance.MyFireballDamage -= 5;
        }
        MyCurrentCount = 0;
    }
}
