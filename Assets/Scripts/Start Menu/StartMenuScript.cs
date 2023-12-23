using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private NavMenu navMenu;

    [SerializeField] private GameObject cameraObject;
    public Vector3 cameraOffset;

    [SerializeField] private GameObject floor;

    private void Start()
    {
        CloseMenu();
    }

    public void StartMenu()
    {
        GetCameraOffset();
        floor.transform.position += cameraOffset;

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

    private void GetCameraOffset()
    {
        cameraOffset = cameraObject.transform.position;
    }
}