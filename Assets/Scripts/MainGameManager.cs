using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] List<NPCGenerator> npcs;
    private static MainGameManager instance;
    private bool salida = true;
    public GameObject trans;
    //public GameObject panels;
    public GameObject pistas;
    public Animator transs;
   // public Animator panelss;
    public Animator pista;
    public GameObject pistasobject;
    private string texto;
    private int cont = 0;

    //Singletone de gamemanager
    public static MainGameManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (instance !=null && instance != this){
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GenerateNPCs();
    }

    public void GenerateNPCs() => npcs.ForEach(npc => npc.Generate());

    public void Variantes()
    {
        npcs = npcs.OrderBy(_ => Random.value).ToList();
        Debug.Log("El culpable es " +npcs[0].name);
        npcs[0].Generate();
        
        for (int i = 1; i < npcs.Count; i++)
        {
            npcs[i].Clonate(npcs[0]);
            npcs[i].Mutate(i);
        }
    }

    public void Acusar(NPCGenerator npc)
    {
        Debug.Log("Soy el gamemanager y estoy acusando a: "+  npc.name);
       if (npcs[0].name == npc.name)
        {
            Debug.Log("Enhorabuena, le diste perpetua al culpable");
        }
        else
        {
            Debug.Log("Mandaste un inocente al paredon");
        }
    }
    public void Final()
    {
        transs.SetBool("Salida", salida);
        SceneManager.LoadScene("Final");
        salida = !salida;
        transs.SetBool("Salida", salida);
    }
    IEnumerator OtraPista()
    {
        Debug.Log("Cambiando pista");
        pista.SetBool("Salida",salida);
        yield return new WaitForSeconds(0.5f);
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = texto;
        pista.SetBool("Salida", !salida);
    } 
    public void ButtonPista(){
        StartCoroutine(OtraPista());
    }
       public void Pistas()
    {
        texto = pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (cont == 1)
        {
            texto = npcs[0].transform.Find("FeatureRopa").GetComponent<Feature>().pista;
        }
        if (cont == 2)
        {
            texto = npcs[0].transform.Find("FeatureCara").GetComponent<Feature>().pista;
        }
        if (cont == 3) 
        {
            texto = npcs[0].transform.Find("FeaturePelo").GetComponent<Feature>().pista;
        }
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = texto;
    }

}
