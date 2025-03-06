using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EQTooltipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public EquipmentManager eqManager;

    public int slot;

    void Start()
    {
        eqManager = EquipmentManager.instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eqManager.currentEquipment[slot] != null)
        {
            RealToolTip.instance.gameObject.transform.position = transform.position;
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            RealToolTip.instance.SetTooltip(eqManager.currentEquipment[slot]);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eqManager.currentEquipment[slot] != null)
        {
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
