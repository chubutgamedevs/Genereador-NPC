using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public GameObject indicador;                //Inidcador para hacer "trampa" y saber el culpable

    // NPC's en la rueda de reconocimiento
    public CharacterPower original;
    public CharacterPower clon0;
    public CharacterPower clon1;
    public CharacterPower clon2;
    public CharacterPower clon3;
    public CharacterPower clon4;
    public CharacterPower clon5;
    public CharacterPower clon6;

    // Variables varias
    public List<CharacterPower> sospechosos;
    private static gamemanager instance;
    public CharacterPower acusado;
    private CharacterPower chorro;
    public GameObject pistasobject; // <--Gameobject con las pistas sobre el sospechoso
    private string pista;
    private int random;
    private CharacterPower preso;
    public bool culpable;
    public GameObject trans;
    public Animator panel;
    public bool salida;


    //Singletone de gamemanager
    public static gamemanager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        salida = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (instance !=null && instance != this){
            Destroy(this.gameObject);
        }
    }
    //---------------------------------
    private void Start()
    {
        Debug.Log("Hola");
        MutarYClonar();
        panel = trans.GetComponent<Animator>();
        salida = true ;
    }
   //Cambiar un feature de cada uno de los no culpable
    public void MutarYClonar()
    {
        sospechosos = sospechosos.OrderBy(_ => Random.value).ToList();
        original = sospechosos[0];
        indicador.transform.position = original.transform.position;
        original.Randomize();

        for (int i = 1; i<sospechosos.Count; i++)
        {
            sospechosos[i].Clonar(original.adn);
            sospechosos[i].Mutar(i%3);
        }
    }

    //El sospechoso acusado es culpable?
    public void Criminal()
    {
        chorro = original;
        if (chorro == acusado){culpable = true;}
        else { culpable = false;}
        Final();
    }

    //Administrar pistas sobre el culpable
    public void Pistas()
    {
        pista = pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text;
        random = Random.Range(1, 3);
        if (random == 1)
        {
            pista = original.transform.Find("FeatureRopa").GetComponent<Feature>().pista;
        }
        if (random == 2)
        {
            pista = original.transform.Find("FeatureCara").GetComponent<Feature>().pista;
        }
        if (random == 3) 
        {
            pista = original.transform.Find("FeaturePelo").GetComponent<Feature>().pista;
        }
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = pista;
    }
    public void Final()
    {

        StartCoroutine(Esperar());
        
        //salida = !salida;
        //panel.SetBool("Salida", salida);
    }

    public void OnButtonReset()
    {
        culpable = false;
        salida = !false;
        SceneManager.LoadScene("Juego");
    }

    IEnumerator Esperar()
    {
        panel.SetBool("Salida", salida);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Final");
    }


}
