using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public CharacterPower acusado;
    private bool carcel;
    private void Start()
    {
        carcel = gamemanager.GetInstance().culpable;
        if (carcel == true) { Culpable(); }
        if (carcel == false) { Inocente(); }
    }

    void Update()
    {
        acusado.Clonar(gamemanager.GetInstance().acusado.adn);
    }

    public void Culpable()
    {
        Debug.Log("Encontraste al culpable, enhorabuena");
    }
    public void Inocente()
    {
        Debug.Log("era un inoscente, mejor suerte la proxima :)");
    }
}
