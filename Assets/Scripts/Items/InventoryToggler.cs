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
    public GameObject[] MenusExtra = null;

    // Height of buttons when they are clicked or not
    private readonly float _posYSelected = 206.5f;

    private readonly float _posYNotSelected = 218.8f;
    private readonly float _posYSelectedWBox = 206.5f;
    private readonly float _posYNotSelectedWBox = 218.8f;

    // the name of button that was clicked
    //that sets auto value bu reading button name
    private string _btnSelected;

    // Button names
    private readonly string _btnBag = "BarBag";

    private readonly string _btnPlayer = "BarPlayer";
    private readonly string _btnCraft = "BarCraft";

    // ExtraMenus names
    private readonly string _mnPlayer = "PagePlayer";

    private readonly string _mnCraft = "PageCraft";

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
        SwitchInventory(ButtonName);
    }

    private void SwitchInventory(string SelectedButtonName)
    {
        _btnSelected = SelectedButtonName;
        var name = _btnSelected;

        PartInventory.SetActive(false);
        FullInventory.SetActive(false);
        FullInventoryWithBox.SetActive(false);

        if (name == _btnBag)
        {
            FullInventory.SetActive(true);
            MoveButtonsInFullInventory();
        }
        else
        {
            FullInventoryWithBox.SetActive(true);
            MoveButtonsInFullInventoryWBox();
        }
        SwitchMenuAfterButtonPress();
    }

    private void MoveButtonsInFullInventory()
    {
        float posX;
        float posY;
        RectTransform rTr;

        foreach (var button in ButtonsInvNoBox)
        {
            if (button.name == _btnBag)
                posY = _posYSelected;
            else
                posY = _posYNotSelected;

            rTr = button.GetComponent<RectTransform>();
            posX = rTr.transform.localPosition.x;
            rTr.transform.localPosition = new Vector3(posX, posY, 0f);
        }
    }

    private void MoveButtonsInFullInventoryWBox()
    {
        var name = _btnSelected;
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

    private void SwitchMenuAfterButtonPress()
    {
        if (_btnSelected == null)
            name = _btnBag;
        else
            name = _btnSelected;

        if (name == _btnBag)
        {
            foreach (var menu in MenusExtra)
            {
                if (menu == null)
                    continue;
                menu.SetActive(false);
            }
        }
        else if (name == _btnPlayer)
        {
            foreach (var menu in MenusExtra)
            {
                if (menu == null)
                    continue;
                if (menu.name == _mnPlayer)
                {
                    menu.SetActive(true);
                    continue;
                }
                menu.SetActive(false);
            }
        }
        else if (name == _btnCraft)
        {
            foreach (var menu in MenusExtra)
            {
                if (menu == null)
                    continue;
                if (menu.name == _mnCraft)
                {
                    menu.SetActive(true);
                    continue;
                }
                menu.SetActive(false);
            }
        }
    }
}