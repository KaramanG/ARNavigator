using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = ("Configs/NavPointConfig"))]
public class NavPointConfig : ScriptableObject
{
    public List<NavPoint> NavPoints;
}
