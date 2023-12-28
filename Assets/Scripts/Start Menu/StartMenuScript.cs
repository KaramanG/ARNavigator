using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private NavMenu navMenu;

    [SerializeField] private GameObject cameraObject;
    public Vector3 cameraOffset;

    [SerializeField] private GameObject tiles;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private TilesConfig tilesConfig;
    private bool isFloorActive = false;

    private Vector3 oldPos = new Vector3(1000, 1000, 1000);
    public float minTileDistance = 0.5f;
    public float groundLevel = -0.3f;
    
    private void Start()
    {
        CloseMenu();
    }

    private void Update()
    {
        if (isFloorActive)
        {
            GenerateGround();
        }
    }

    private void GenerateGround()
    {
        if (Vector3.Distance(cameraObject.transform.position, oldPos) > minTileDistance)
        {
            Vector3 groundPos = GetUserGroundPos();
            GenerateGroundTile(groundPos);

            oldPos = cameraObject.transform.position;
        }
    }

    public Vector3 GetUserGroundPos()
    {
        return new Vector3(cameraObject.transform.position.x, groundLevel, cameraObject.transform.position.z);
    }

    public GameObject GenerateGroundTile(Vector3 pos)
    {
        GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
        tile.transform.SetParent(tiles.transform);

        return tile;
    }

    public void StartMenu()
    {
        isFloorActive = true;
        LoadFloor();

        mainMenu.transform.position = startMenu.transform.position;
        mainMenu.transform.rotation = startMenu.transform.rotation;

        startMenu.SetActive(false);
        mainMenu.SetActive(true);

        navMenu.isPreviewModeActive = true;
        navMenu.updateButtons();
    }

    public void CloseMenu()
    {
        isFloorActive = false;

        startMenu.SetActive(true);
        mainMenu.SetActive(false);

        navMenu.isPreviewModeActive = false;
        navMenu.DestroyPreviewPoints();
    }

    public void EndMenu()
    {
        Application.Quit();
    }

    private List<GameObject> GetFloorTiles()
    {
        List<GameObject> tilesOnScene = new List<GameObject>();
        tiles.GetChildGameObjects(tilesOnScene);
        return tilesOnScene;
    }

    public void ToggleFloor()
    {
        List<GameObject> tilesOnScene = GetFloorTiles();

        tilePrefab.GetComponent<MeshRenderer>().enabled = !tilePrefab.GetComponent<MeshRenderer>().enabled;

        foreach (var tile in tilesOnScene)
        {
            tile.GetComponent<MeshRenderer>().enabled = !tile.GetComponent<MeshRenderer>().enabled;
        }
    }

    public void ToggleFloorCreation()
    {
        isFloorActive = !isFloorActive;
    }

    public void DestroyFloor()
    {
        List<GameObject> tilesOnScene = GetFloorTiles();

        foreach (var tile in tilesOnScene)
        {
            Destroy(tile);
        }
    }

    public void SaveFloor()
    {
        List<GameObject> tilesOnScene = GetFloorTiles();

        tilesConfig.tileList.Clear();
        foreach (var tile in tilesOnScene)
        {
            tilesConfig.tileList.Add(tile.transform.position);
        }
    }

    public void LoadFloor()
    {
        foreach (var tilePos in tilesConfig.tileList)
        {
            GenerateGroundTile(tilePos);
        }
    }

    private void GetCameraOffset()
    {
        cameraOffset = cameraObject.transform.position;
    }

    public void SetNewOffset()
    {
        GetCameraOffset();
        UpdateWithOffset();
    }

    private void UpdateWithOffset()
    {
        tiles.transform.position += cameraOffset;
        groundLevel += cameraOffset.y;

        navMenu.DestroyPreviewPoints();
        navMenu.updateButtons();
    }

    public void SortNavPointList(NavPointConfig navPoints)
    {
        List<string> nameList = new List<string>();
        foreach (var navPoint in navPoints.NavPoints)
        {
            nameList.Add(navPoint.Name);
        }
        nameList.Sort();

        List<NavPoint> newNavPoints = new List<NavPoint>();
        for (int i = 0; i < nameList.Count; i++)
        {
            foreach (var navPoint in navPoints.NavPoints)
            {
                if (navPoint.Name == nameList[i])
                {
                    newNavPoints.Add(navPoint);
                    break;
                }
            }
        }
        navPoints.NavPoints = newNavPoints;

        navMenu.updateButtons();
    }
}