using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatestFireSkill : Skill
{
    public override bool Click()
    {
        if (base.Click())
        {
            //Give player skill's ability
            if (SpellSystem.instance != null)
            {
                SpellSystem.instance.MyFireballDamage += 20;
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
            SpellSystem.instance.MyFireballDamage -= 20;
        }
        MyCurrentCount = 0;
    }
}
