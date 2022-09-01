using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPower : MonoBehaviour
{

    void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        foreach (Transform f in transform)
        {
            Feature feat = f.gameObject.GetComponent<Feature>();
            feat.Randomize();
            Debug.Log(feat.pista);
        }
    }
}
