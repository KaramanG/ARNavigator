using TMPro;
using UnityEngine;

public class PreviewScript : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;

    private string mainCameraTag = "MainCamera";
    private GameObject cameraObject;

    private void Awake()
    {
        cameraObject = GameObject.FindGameObjectWithTag(mainCameraTag);
    }

    private void Update()
    {
        titlePanel.transform.LookAt(cameraObject.transform);
    }

    public void changeText(string newText)
    {
        titlePanel.GetComponent<TextMeshProUGUI>().text = newText;
    }
}
