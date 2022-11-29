using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPower : MonoBehaviour
{
    public int[] adn;
    private gamemanager miGameManager;
    void Start()
    {
        miGameManager = gamemanager.GetInstance();
        Randomize();
    }
    private void Awake()
    {
        miGameManager = gamemanager.GetInstance();
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
    public void OnMouseDown()
    {
        Debug.Log("clickeado el npc:" + this.gameObject.GetComponent<CharacterPower>());
        miGameManager.acusado = this.gameObject.GetComponent<CharacterPower>();        
        
    }
}
