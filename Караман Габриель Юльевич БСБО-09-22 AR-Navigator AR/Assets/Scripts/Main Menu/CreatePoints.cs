using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField] private GameObject scrollMenu;
    [SerializeField] private GameObject addPointsMenu;
    private bool addPointsMenuActive;

    [SerializeField] private NavPointConfig navPointConfig;
    [SerializeField] private NavPointConfig customNavPointConfig;

    [SerializeField] private Transform userPos;
    [SerializeField] private NumpadScript numpadScript;

    private void Start()
    {
        addPointsMenu.SetActive(false);
        addPointsMenuActive = false;
    }

    public void addPoint(bool isCustom)
    {
        if (numpadScript.GetCurrentString().Length > 0)
        {
            NavPoint newNavPoint = new NavPoint();
            newNavPoint.Name = numpadScript.GetCurrentString();
            newNavPoint.Position = userPos.position;

            if (!isCustom)
            {
                navPointConfig.NavPoints.Add(newNavPoint);
                return;
            }
            customNavPointConfig.NavPoints.Add(newNavPoint);
        }
    }

    public void toggleAddPointMenu()
    {
        if (!addPointsMenuActive)
        {
            addPointsMenuActive = true;

            scrollMenu.SetActive(false);
            addPointsMenu.SetActive(true);

            return;
        }

        addPointsMenuActive = false;

        scrollMenu.SetActive(true);
        addPointsMenu.SetActive(false);
    }
}
