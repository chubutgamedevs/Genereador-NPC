using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator texto;
    public bool salida;

    void Start()
    {
        texto = gameObject.GetComponent<Animator>();
    }

    public void OnButtonEntrada()
    {
        salida = !salida;
        texto.SetBool("salida", salida);
    }

}
