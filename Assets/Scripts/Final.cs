using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using DG.Tweening;

public class Final : MonoBehaviour
{
    public Wumpus acusado;
    private bool carcel;
    private string mensaje; 
    public GameObject ReiniciarButton;
    public GameObject CartelitoOBJ;
    private MainGameManager gm;
    [SerializeField] MensajeFinal cartelito;
    [SerializeField] Cortina cortina;
    public GameObject galleta;

    public GameObject CulpableIMG;
    public GameObject InocenteIMG;
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        cartelito = CartelitoOBJ.GetComponent<MensajeFinal>();

        if (gm != null){
            acusado.ClonateWumpus(gm.GetAcusado());            
        }       

        
        
        if (acusado.culpable) { 
            Culpable(); }
        else { Inocente(); }
        
        cortina.Abrir();
        StartCoroutine(Esperar());
    }

    public void Culpable()
    {
        galleta.SetActive(true);
        CulpableIMG.SetActive(true);
    }
    public void Inocente()
    { 
        InocenteIMG.SetActive(true);
    }
    public void Reset()
    {
        StartCoroutine(OtroEsperar());
    }
    IEnumerator Esperar(){
        yield return new WaitForSeconds(0.5f);
        cartelito.Mostrar();
        yield return new WaitForSeconds(3);
        ReiniciarButton.SetActive(true);
        acusado.RayosX();
    }
    IEnumerator OtroEsperar(){
        cortina.Cerrar();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("Mein");
    }

    private void EstomagoAparecer(){
        foreach (SpriteRenderer sp in acusado.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.color = Color.black;
        } 
    }
    
    
}
