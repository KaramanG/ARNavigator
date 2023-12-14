using TMPro;
using UnityEngine;

public class PreviewScript : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;

    private string mainCameraTag = "MainCamera";
    private GameObject camera;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag(mainCameraTag);
    }

    private void Update()
    {
        titlePanel.transform.LookAt(camera.transform);
    }

    public void changeText(string newText)
    {
        titlePanel.GetComponent<TextMeshProUGUI>().text = newText;
    }
}
