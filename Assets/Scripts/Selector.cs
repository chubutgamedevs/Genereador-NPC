using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public MainGameManager gm;
    public void OnMouseDown()
    {
        gm.Acusar(GetComponent<NPCGenerator>()); 
    }
}
