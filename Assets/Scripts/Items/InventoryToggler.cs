using UnityEngine;

// toggles between invetories

public class InventoryToggler : MonoBehaviour
{
    // values are assigned to null only to get rid of unity warning in console
    public GameObject PartInventory = null;

    public GameObject FullInventory = null;
    public GameObject FullInventoryWithBox = null;
    public GameObject[] ButtonsInvWithBox = null;
    public GameObject[] ButtonsInvNoBox = null;

    // Height of buttons when they are clicked or not
    private float _posYSelected = 206.5f;

    private float _posYNotSelected = 218.8f;
    private float _posYSelectedWBox = 206.5f;
    private float _posYNotSelectedWBox = 218.8f;

    private void Update()
    {
        ToggleBetweenInventoryUI();
    }

    private void ToggleBetweenInventoryUI()
    {
        if (!Input.GetButtonDown("Fire3"))
            return;

        var PartInvIsActive = PartInventory.activeSelf;

        if (PartInvIsActive)
        {
            PartInventory.SetActive(false);
            FullInventory.SetActive(true);
        }
        else
        {
            PartInventory.SetActive(true);
            FullInventory.SetActive(false);
            FullInventoryWithBox.SetActive(false);
        }
    }

    public void SwitchMenu(string ButtonName)
    {
        var name = ButtonName;
        switch (name)
        {
            case "BarBag":
                BagInventory();
                break;

            case "BarPlayer":
                PlayerInventory(name);
                break;

            case "BarCraft":
                CraftInventory(name);
                break;

            default:
                print("this should never log");
                break;
        }
    }

    private void BagInventory()
    {
        if (FullInventory.activeSelf)
            return;

        PartInventory.SetActive(false);
        FullInventory.SetActive(true);
        FullInventoryWithBox.SetActive(false);

        MoveButtonsInFullInventory();
    }

    private void PlayerInventory(string SelectedButtonName)
    {
        PartInventory.SetActive(false);
        FullInventory.SetActive(false);
        FullInventoryWithBox.SetActive(true);

        MoveButtonsInFullInventoryWBox(SelectedButtonName);
    }

    private void CraftInventory(string SelectedButtonName)
    {
        PartInventory.SetActive(false);
        FullInventory.SetActive(false);
        FullInventoryWithBox.SetActive(true);

        MoveButtonsInFullInventoryWBox(SelectedButtonName);
    }

    private void MoveButtonsInFullInventory()
    {
        float posX;
        float posY;
        RectTransform rTr;

        foreach (var button in ButtonsInvNoBox)
        {
            if (button.name == "BarBag")
                posY = _posYSelected;
            else
                posY = _posYNotSelected;

            rTr = button.GetComponent<RectTransform>();
            posX = rTr.transform.localPosition.x;
            rTr.transform.localPosition = new Vector3(posX, posY, 0f);
        }
    }

    private void MoveButtonsInFullInventoryWBox(string SelectedButtonName)
    {
        var name = SelectedButtonName;
        float posX;
        float posY;
        RectTransform rTr;

        foreach (var button in ButtonsInvWithBox)
        {
            if (button.name == name)
                posY = _posYSelectedWBox;
            else
                posY = _posYNotSelectedWBox;

            rTr = button.GetComponent<RectTransform>();
            posX = rTr.transform.localPosition.x;
            rTr.transform.localPosition = new Vector3(posX, posY, 0f);
        }
    }
}