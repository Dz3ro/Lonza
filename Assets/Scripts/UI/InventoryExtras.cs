using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryExtras : MonoBehaviour, IPointerClickHandler
{
    private InventoryToggler _invTog;

    public void OnPointerClick(PointerEventData eventData)
    {
        _invTog.SwitchMenu(gameObject.name);
    }

    private void Awake()
    {
        _invTog = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryToggler>();
    }
}