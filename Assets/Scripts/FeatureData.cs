using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FeatureData", order = 1)]
public class FeatureData : ScriptableObject
{
    public Sprite sprite;
    public string pista;
}
