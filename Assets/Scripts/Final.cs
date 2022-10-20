using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public NPCGenerator acusado;
    private bool carcel;
    private void Start()
    {
        acusado.Clonate(MainGameManager.GetInstance().acusado);

        if (MainGameManager.GetInstance().culpable)
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
    }
    public void Inocente()
    {
        Debug.Log("era un inoscente, mejor suerte la proxima :)");
    }
    public void Reset()
    {
        SceneManager.LoadScene("Mein");

    }
}
