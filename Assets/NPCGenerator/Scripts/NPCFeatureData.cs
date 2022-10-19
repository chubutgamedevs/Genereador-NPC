using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPCFeatureData", order = 1)]
public class NPCFeatureData : ScriptableObject
{
    public Sprite sprite;
    public string pista;
}
