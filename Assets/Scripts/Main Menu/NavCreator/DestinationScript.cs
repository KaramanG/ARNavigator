using UnityEngine;

public class DestinationScript : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private bool isActive = false;

    private void Awake()
    {
        isActive = true;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            float yScale = Mathf.Sin(2 * Time.fixedTime) * 0.001f;
            float rotScale = ((Time.fixedTime) * 0.1f) % 360;

            cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y + yScale, cube.transform.position.z);
            cube.transform.Rotate(rotScale, 0f, -rotScale * 0.8f, Space.Self);
        }
    }
}
