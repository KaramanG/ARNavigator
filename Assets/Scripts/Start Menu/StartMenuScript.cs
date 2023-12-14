using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private NavMenu navMenu;

    private void Start()
    {
        CloseMenu();
    }

    public void StartMenu()
    {
        mainMenu.transform.position = startMenu.transform.position;
        mainMenu.transform.rotation = startMenu.transform.rotation;

        startMenu.SetActive(false);
        mainMenu.SetActive(true);

        navMenu.isPreviewModeActive = true;
        navMenu.updateButtons();
    }

    public void CloseMenu()
    {
        startMenu.SetActive(true);
        mainMenu.SetActive(false);

        navMenu.isPreviewModeActive = false;
        navMenu.DestroyPreviewPoints();
    }

    public void EndMenu()
    {
        Application.Quit();
    }
}