using Pathfinding;
using UnityEngine;

public class NavCreator : MonoBehaviour
{
    [SerializeField] private GameObject navCreatorMenu;
    [SerializeField] private StartMenuScript startMenuScript;

    [SerializeField] private GameObject navPointPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform userPos;

    [SerializeField] private GameObject numpadMenu;
    [SerializeField] private GameObject scrollMenu;
    [SerializeField] private NavMenu navMenu;

    [SerializeField] private AstarPath aStarPath;

    private bool navCreatorActive;
    private GameObject currentNavPoint;
    private Vector3 userStartPos = new Vector3();
    public float minDistanceClose = 0.6f;

    public float timer = 1.25f;
    private float currentTimer = 0f;
    private string arrowTag = "Arrow";

    private void Start()
    {
        navCreatorActive = false;
        navCreatorMenu.SetActive(false);
    }

    public void ProccessNavPath(NavPoint navPoint, Vector3 cameraOffset)
    {
        toggleNavCreator();

        AstarData data = aStarPath.data;
        GridGraph mainGraph = data.gridGraph;

        Vector3 graphCenter = new Vector3(userPos.position.x, -1f, userPos.position.y);
        mainGraph.center = graphCenter;

        aStarPath.Scan();

        currentNavPoint = Instantiate(navPointPrefab, navPoint.Position + cameraOffset, Quaternion.identity);

        userStartPos = startMenuScript.GetUserGroundPos();
        userStartPos.y += 0.05f;
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
            Instantiate(arrowPrefab, userStartPos, Quaternion.identity);
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
        userStartPos = startMenuScript.GetUserGroundPos();
        userStartPos.y += 0.05f;
        aStarPath.Scan();
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

    public void IncreaseArrowSpeed()
    {
        if (timer > 0.5f)
        {
            if (arrowPrefab.GetComponent<AIPath>().maxSpeed < 1f)
            {
                timer -= 0.25f;

                arrowPrefab.GetComponent<AIPath>().maxSpeed += 0.125f;
                GameObject[] arrows = GameObject.FindGameObjectsWithTag(arrowTag);
                foreach (GameObject arrow in arrows)
                {
                    arrow.GetComponent<AIPath>().maxSpeed += 0.125f;
                }
            }
        }
    }

    public void DecreaseArrowSpeed()
    {
        if (timer < 2.5f)
        {
            if (arrowPrefab.GetComponent<AIPath>().maxSpeed > 0.125f)
            {
                timer += 0.25f;

                arrowPrefab.GetComponent<AIPath>().maxSpeed -= 0.125f;
                GameObject[] arrows = GameObject.FindGameObjectsWithTag(arrowTag);
                foreach (GameObject arrow in arrows)
                {
                    arrow.GetComponent<AIPath>().maxSpeed -= 0.125f;
                }
            }
        }
    }

}
