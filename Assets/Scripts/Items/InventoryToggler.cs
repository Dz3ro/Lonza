using UnityEngine;

// toggles between full inventory and jsut the 1 bar

public class InventoryToggler : MonoBehaviour
{
    // values are assigned to null only to get rid of unity warning in console
    [SerializeField]
    private GameObject _partInventory = null;

    [SerializeField]
    private GameObject _fullInventory = null;

    private void Update()
    {
        ToggleBetweenInventoryUI();
    }

    private void ToggleBetweenInventoryUI()
    {
        if (!Input.GetButtonDown("Fire3"))
            return;

        var fullActive = _fullInventory.activeSelf;

        _partInventory.SetActive(fullActive);
        _fullInventory.SetActive(!fullActive);
    }
}