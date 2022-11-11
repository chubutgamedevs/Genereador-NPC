using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Final : MonoBehaviour
{
    public Wumpus acusado;
    private bool carcel;
    public TextMeshPro texto;
    private string mensaje; 
    public GameObject ReiniciarButton;
    public GameObject CartelitoOBJ;
    private MainGameManager gm;
    [SerializeField] MensajeFinal cartelito;
    [SerializeField] Cortina cortina;
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        cartelito = CartelitoOBJ.GetComponent<MensajeFinal>();

        if (MainGameManager.GetInstance() != null){
            acusado.Clonate(MainGameManager.GetInstance().GetAcusado());
        }       

        
        
        if (acusado.culpable) { 
            Culpable(); }
        else { Inocente(); }
        
        cortina.Abrir();
        StartCoroutine(Esperar());
    }

    public void Culpable()
    {
        texto.text = "Encontraste al culpable, enhorabuena";
    }
    public void Inocente()
    { 
        texto.text = "era un inocente, mejor suerte la proxima :)";
    }
    public void Reset()
    {
        StartCoroutine(OtroEsperar());
    }
    IEnumerator Esperar(){
        yield return new WaitForSeconds(0.5f);
        cartelito.Mostrar();
        yield return new WaitForSeconds(1);
        ReiniciarButton.SetActive(true);
        acusado.RayosX();
    }
    IEnumerator OtroEsperar(){
        cortina.Cerrar();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Mein");
    }

    private void EstomagoAparecer(){
        foreach (SpriteRenderer sp in acusado.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.color = Color.black;
        } 
    }
    
}
