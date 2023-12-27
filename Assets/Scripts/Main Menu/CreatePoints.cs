using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField] private GameObject scrollMenu;
    [SerializeField] private GameObject addPointsMenu;

    [SerializeField] private NavPointConfig navPointConfig;
    [SerializeField] private NavPointConfig customNavPointConfig;

    [SerializeField] private Transform userPos;
    [SerializeField] private NumpadScript numpadScript;

    [SerializeField] private StartMenuScript startMenuScript;
    [SerializeField] private ChangeMenu changeMenu;

    private void Start()
    {
        addPointsMenu.SetActive(false);
    }

    public void addPoint(bool isCustom)
    {
        if (numpadScript.GetCurrentString().Length > 0)
        {
            NavPoint newNavPoint = new NavPoint();
            newNavPoint.Name = CapitalizeString(numpadScript.GetCurrentString());
            newNavPoint.Position = userPos.position - startMenuScript.cameraOffset;

            if (isCustom)
            {
                customNavPointConfig.NavPoints.Add(newNavPoint);
            }
            else
            {
                navPointConfig.NavPoints.Add(newNavPoint);
            }

            numpadScript.ClearCurrentString();
            changeMenu.EnableScrollMenu();
        }
    }

    private string CapitalizeString(string s)
    {
        if (s.Length > 0)
        {
            char[] letters = s.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        return s;
    }
}
