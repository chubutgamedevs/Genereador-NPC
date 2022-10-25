using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cortina : MonoBehaviour
{
    [SerializeField] GameObject izq;
    [SerializeField] GameObject der; 
   
    public void Cerrar(){
        izq.transform.DOMoveX(20 , 0.5f);
        der.transform.DOMoveX(-20, 0.5f);
    }
    public void Abrir(){
        izq.transform.DOMoveX(-45, 0.5f);
        der.transform.DOMoveX(45, 0.5f);
    }
}
