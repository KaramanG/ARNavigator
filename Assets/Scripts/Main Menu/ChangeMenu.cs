using TMPro;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] private GameObject scrollMenu;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private GameObject navCreatorMenu;
    [SerializeField] private GameObject addPointsMenu;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private NavMenu navMenu;

    [SerializeField] private TextMeshProUGUI sidePanelText;
    private string scrollMenuString = "Список точек";
    private string scrollCustomMenuString = "Список особых точек";
    private string addPointsMenuString = "Панель добавления точек";
    private string debugMenuString = "Настройки";

    public void EnableScrollMenu()
    {
        debugMenu.SetActive(false);
        addPointsMenu.SetActive(false);

        if (!navMenu.isCustomNavPointsSelected)
        {
            sidePanelText.text = scrollMenuString;
        }
        else
        {
            sidePanelText.text = scrollCustomMenuString;
        }

        scrollMenu.SetActive(true);
    }

    public void EnableAddPointsMenu()
    {
        if (!addPointsMenu.activeSelf)
        {
            debugMenu.SetActive(false);
            scrollMenu.SetActive(false);

            sidePanelText.text = addPointsMenuString;
            addPointsMenu.SetActive(true);
            return;
        }
        EnableScrollMenu();
    }

    public void EnableDebugMenu()
    {
        if (!debugMenu.activeSelf)
        {
            addPointsMenu.SetActive(false);
            scrollMenu.SetActive(false);

            sidePanelText.text = debugMenuString;
            debugMenu.SetActive(true);
            return;
        }
        EnableScrollMenu();
    }
}
