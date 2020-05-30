using UnityEngine;

public class FullInventoryBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _partInventory;

    [SerializeField]
    private GameObject _fullInventory;

    private void Start()
    {
    }

    private void Update()
    {
        ToggleBetweenInventoryUI();
    }

    private void ToggleBetweenInventoryUI()
    {
        if (!Input.GetButtonDown("Fire3"))
            return;

        if (_partInventory.activeSelf)
        {
            _partInventory.SetActive(false);
            _fullInventory.SetActive(true);
        }
        else
        {
            _partInventory.SetActive(true);
            _fullInventory.SetActive(false);
        }
    }
}