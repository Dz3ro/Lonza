using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPart : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler
{
    private GameFreezer _gFrz;
    private void Awake()
    {
        _gFrz = GameObject.FindGameObjectWithTag("Player")
            .GetComponentInChildren<GameFreezer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _gFrz.FreezePlayerAction();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gFrz.UnfreezePlayerAction();

    }

}
