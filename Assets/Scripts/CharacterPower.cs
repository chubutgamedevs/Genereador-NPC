using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPower : MonoBehaviour
{
    public int[] adn;
    
    void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Feature feat = transform.GetChild(i).gameObject.GetComponent<Feature>();
            adn[i] = feat.Randomize();
        }            
    }

    public void Clonar(int[] adn)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Feature feat = transform.GetChild(i).GetComponent<Feature>();
            feat.SetADN(adn[i]);
        }
    }

    public void Mutar(int feat)
    {
        transform.GetChild(feat).GetComponent<Feature>().Mutar();
    }

}
