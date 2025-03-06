using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WriteSpellInfo : MonoBehaviour, IPointerClickHandler
{
    public int spell;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(spell)
        {
            case 1:
                if(SpellSystem.instance.MySpell1Unlocked)
                {
                    MessageFeedManager.MyInstance.WriteMessage("Kula ognia - Tworzy ognistą kulę");
                }      
                break;
            case 2:
                if(SpellSystem.instance.MySpell2Unlocked)
                {
                    MessageFeedManager.MyInstance.WriteMessage("Podmuch mrozu - Zamraża przeciwników");
                }       
                break;
            case 3:
                if (SpellSystem.instance.MySpell3Unlocked)
                {
                    MessageFeedManager.MyInstance.WriteMessage("Błogosławieństwo - Leczy użytkownika");
                }    
                break;
            case 4:
                if (SpellSystem.instance.MySpell4Unlocked)
                {
                    MessageFeedManager.MyInstance.WriteMessage("Berserk - Podwaja siłę użytkownika");
                }          
                break;
        }
    }
}
