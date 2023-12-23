using Pathfinding;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private GameObject arrow;

    private string navPointTag = "NavPoint";
    private GameObject currentNavPoint;

    private float timer = 5f;
    private bool isActive;

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
            if (timer <= 0f)
            {
                Destroy(arrow);
            }
            timer -= Time.fixedDeltaTime;
        }    
    }
}
