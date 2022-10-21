using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Final : MonoBehaviour
{
    public NPCGenerator acusado;
    private bool carcel;
    public TextMeshPro texto;
    private string mensaje; 
    private void Start()
    
    {
        acusado.Clonate(MainGameManager.GetInstance().acusado);

        if (acusado.culpable)
        {
            Culpable();
        }
        else
        {
            Inocente();
        }
    }

    public void Culpable()
    {
        Debug.Log("Encontraste al culpable, enhorabuena");
        mensaje = "Encontraste al culpable, enhorabuena";
        texto.text = mensaje;
    }
    public void Inocente()
    {
        Debug.Log("era un inoscente, mejor suerte la proxima :)");
        mensaje = "era un inoscente, mejor suerte la proxima :)";
        texto.text = mensaje;
    }
    public void Reset()
    {
        SceneManager.LoadScene("Mein");
    }
    
}
