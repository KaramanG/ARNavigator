using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Configs/TilesConfig"))]
public class TilesConfig : ScriptableObject
{
    public List<Vector3> tileList;
}