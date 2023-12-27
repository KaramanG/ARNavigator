using Pathfinding;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private GameObject arrow;

    private string navPointTag = "NavPoint";
    private GameObject currentNavPoint;

    private bool isActive;
    private float minDistance = 0.33f;

    private void Awake()
    {
        currentNavPoint = GameObject.FindGameObjectWithTag(navPointTag);
        isActive = true;

        destinationSetter.target = currentNavPoint.transform;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (Vector3.Distance(arrow.transform.position, currentNavPoint.transform.position) <= minDistance)
            {
                Destroy(arrow);
            }
        }
    }

}
