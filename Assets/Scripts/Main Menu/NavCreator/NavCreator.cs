using UnityEngine;

public class NavCreator : MonoBehaviour
{
    [SerializeField] private GameObject navCreatorMenu;

    [SerializeField] private GameObject navPointPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform userPos;

    [SerializeField] private GameObject numpadMenu;
    [SerializeField] private GameObject scrollMenu;
    [SerializeField] private NavMenu navMenu;

    private bool navCreatorActive;
    private GameObject currentNavPoint;
    private float minDistanceClose = 0.33f;

    private float timer = 1.25f;
    private float currentTimer = 0f;
    private string arrowTag = "Arrow";

    private void Start()
    {
        navCreatorActive = false;
        navCreatorMenu.SetActive(false);
    }

    public void ProccessNavPath(NavPoint navPoint)
    {
        toggleNavCreator();
        
        currentNavPoint = Instantiate(navPointPrefab, navPoint.Position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (navCreatorActive)
        {
            GenerateArrows();
            if (Vector3.Distance(userPos.position, currentNavPoint.transform.position) < minDistanceClose)
            {
                closeNavCreator();
            }
        }
    }

    private void GenerateArrows()
    {
        if (currentTimer <= 0f)
        {
            Instantiate(arrowPrefab, userPos.position, Quaternion.identity);
            currentTimer = timer;
        }
        currentTimer -= Time.fixedDeltaTime;
    }

    private void toggleNavCreator()
    {
        if (!navCreatorActive)
        {
            navCreatorActive = true;

            numpadMenu.SetActive(false);
            scrollMenu.SetActive(false);

            navMenu.isPreviewModeActive = false;
            navMenu.updateButtons();

            navCreatorMenu.SetActive(true);

            return;
        }

        navCreatorActive = false;

        numpadMenu.SetActive(true);
        scrollMenu.SetActive(true);

        navMenu.isPreviewModeActive = true;
        navMenu.updateButtons();

        navCreatorMenu.SetActive(false);
    }

    public void refreshNavCreator()
    {
        resetArrows();
    }

    public void closeNavCreator()
    {
        Destroy(currentNavPoint);
        resetArrows();

        toggleNavCreator();
    }

    private void resetArrows()
    {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag(arrowTag);
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
    }
}
