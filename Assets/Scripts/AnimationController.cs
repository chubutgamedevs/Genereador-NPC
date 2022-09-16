using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator texto;
    public bool salida;

    // Start is called before the first frame update
    void Start()
    {
        texto = gameObject.GetComponent<Animator>();
        salida = true;
    }

    public void OnButtonEntrada()
    {
        salida = !salida;
        texto.SetBool("salida", salida);
    }

}
