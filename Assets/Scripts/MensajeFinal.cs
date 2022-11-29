using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;


public class MensajeFinal : MonoBehaviour
{   
    public void Mostrar(){
        transform.DOMoveY(7f, 0.5f);
        transform.DOScale(0.4f, 0.5f);
    }

    public void Esconder(){
        transform.DOMoveY(10f, 0.5f);
        transform.DOScale(10f, 0.5f);
    }
}
