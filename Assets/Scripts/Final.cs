using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Final : MonoBehaviour
{
    public NPCGenerator acusado;
    private bool carcel;
    public TextMeshPro texto;
    private string mensaje; 
    public GameObject ReiniciarButton;
    public GameObject CartelitoOBJ;
    private MainGameManager gm;

    [SerializeField] private MensajeFinal cartelito;
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        cartelito = CartelitoOBJ.GetComponent<MensajeFinal>();

        acusado.Clonate(MainGameManager.GetInstance().acusado);

        if (acusado.culpable)
        {
            Culpable();
        }
        else
        {
            Inocente();
        }
        gm.SalirNivel();
        StartCoroutine(Esperar());
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
        gm.EntrarNivel();
        StartCoroutine(OtroEsperar());
    }
    IEnumerator Esperar(){
        yield return new WaitForSeconds(1);
        cartelito.Mostrar();
        yield return new WaitForSeconds(1);
        ReiniciarButton.SetActive(true);
    }
    IEnumerator OtroEsperar(){
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Mein");
    }
    
}
