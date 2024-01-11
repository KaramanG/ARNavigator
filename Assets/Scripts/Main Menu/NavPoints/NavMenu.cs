using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class NavMenu : MonoBehaviour
{
    [SerializeField] private NavPointConfig navPointConfig;
    [SerializeField] private NavPointConfig customNavPointConfig;

    public bool isCustomNavPointsSelected;

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private GridObjectCollection gridObjectCollection;
    [SerializeField] private Transform rootPos;
    
    [SerializeField] private GridObjectCollection customGridObjectCollection;
    [SerializeField] private Transform customRootPos;

    [SerializeField] private NumpadScript numpadScript;
    [SerializeField] private NavCreator navCreator;

    [SerializeField] private Transform rootPreviewHandler;
    [SerializeField] private GameObject navPointPreviewPrefab;
    private string navPointPreviewTag = "NavPointPreview";

    public bool isPreviewModeActive;

    [SerializeField] private StartMenuScript startMenuScript;

    private void Start()
    {
        isCustomNavPointsSelected = false;
        isPreviewModeActive = false;

        updateButtons();
    }

    public void updateButtons()
    {
        if (!isCustomNavPointsSelected)
        {
            updateType(gridObjectCollection, rootPos, navPointConfig);
            return;
        }
        updateType(customGridObjectCollection, customRootPos, customNavPointConfig);
    }

    private void updateType(GridObjectCollection gridObjectCollection, Transform rootPos, NavPointConfig navPointConfig)
    {
        DestroyChildObjects(rootPos);
        SpawnNavButtons(navPointConfig, buttonPrefab, rootPos);
        StartCoroutine(UpdateCollection(gridObjectCollection));
    }

    private void DestroyChildObjects(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
        
        DestroyPreviewPoints();
    }

    private void SpawnNavButtons(NavPointConfig navPointConfig, GameObject prefab, Transform rootPos)
    {
        foreach (var navPoint in navPointConfig.NavPoints)
        {
            if (NameBypassFilter(navPoint.Name, numpadScript.GetCurrentString()))
            {
                var button = Instantiate(prefab, rootPos);
                SpawnPreviewPoint(navPoint);

                if (button.TryGetComponent(out ButtonScript buttonScript))
                {
                    buttonScript.Initialize(navPoint);
                }

                buttonScript.OnButtonClicked += () => navCreator.ProccessNavPath(navPoint, startMenuScript.cameraOffset);  
            }
        }
    }

    private bool NameBypassFilter(string name, string filter)
    {
        if (filter != "")
        {
            return name.ToLower().Contains(filter);
        }
        return true;
    }

    private IEnumerator UpdateCollection(GridObjectCollection gridObjectCollection)
    {
        yield return new WaitForEndOfFrame();

        gridObjectCollection.UpdateCollection();
    }

    public void ChangeNavPointType()
    {
        if (!isCustomNavPointsSelected)
        {
            isCustomNavPointsSelected = true;
            updateButtons();

            gridObjectCollection.gameObject.SetActive(false);
            customGridObjectCollection.gameObject.SetActive(true);

            return;
        }

        isCustomNavPointsSelected = false;
        updateButtons();

        gridObjectCollection.gameObject.SetActive(true);
        customGridObjectCollection.gameObject.SetActive(false);
    }

    public void SpawnPreviewPoint(NavPoint navPoint)
    {
        if (isPreviewModeActive)
        {
            GameObject currentNavPointPreview = Instantiate(navPointPreviewPrefab, rootPreviewHandler);
            currentNavPointPreview.transform.position = navPoint.Position + startMenuScript.cameraOffset;

            PreviewScript previewScript = currentNavPointPreview.GetComponent<PreviewScript>();
            previewScript.changeText(navPoint.Name);
        }
    }

    public void DestroyPreviewPoints()
    {
        GameObject[] navPointsPreviewOnScene = GameObject.FindGameObjectsWithTag(navPointPreviewTag);
        foreach (GameObject navPointPreview in navPointsPreviewOnScene)
        {
            Destroy(navPointPreview);
        }
    }
}
