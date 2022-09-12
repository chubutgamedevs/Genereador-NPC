using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sospechoso : MonoBehaviour
{
    public GameObject criminal;

    public void Mutante()
    {
        foreach (Transform f in transform)
        {
            Feature feat = f.gameObject.GetComponent<Feature>();
            feat.Mutar();
        }
    }

}
