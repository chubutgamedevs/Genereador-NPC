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
    public GameObject estomago;
    public Material RashosEkis;
    [SerializeField] MensajeFinal cartelito;
    [SerializeField] Cortina cortina;
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        cartelito = CartelitoOBJ.GetComponent<MensajeFinal>();

        acusado.Clonate(MainGameManager.GetInstance().acusado);

        if (acusado.culpable) { Culpable(); }
        else { Inocente(); }
        
        cortina.Abrir();
        StartCoroutine(Esperar());
    }

    public void Culpable()
    {
        mensaje = "Encontraste al culpable, enhorabuena";
        texto.text = mensaje;
        
    }
    public void Inocente()
    {
        mensaje = "era un inocente, mejor suerte la proxima :)";
        texto.text = mensaje;
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
        EstomagoAparecer();
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
        //RashosEkis.SetFloat("Value", 1f);
        estomago.SetActive(true);
    }
    
}
