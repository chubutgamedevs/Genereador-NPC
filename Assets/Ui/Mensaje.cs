using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class Mensaje : MonoBehaviour
{    
    public TextMeshPro texto;

    public void Mostrar(string mensaje){
        texto.text = mensaje;
        transform.DOMoveY(-8f, 0.5f);
    }

    public void Esconder(){
        transform.DOMoveY(-16f, 0.5f);
    }
}
