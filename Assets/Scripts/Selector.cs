using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public MeinSceneManager sm;
    public void OnMouseDown()
    {
        sm.Acusar(transform.GetComponent<Wumpus>()); 
    }
}
