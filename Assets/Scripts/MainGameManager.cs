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
    public GameObject pistas;
    public Animator pista;
    public GameObject pistasobject;
    private string texto;
    private int pistaActual = 0;

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
        SiguientePista();
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
        SceneManager.LoadScene("Final");
    }
    IEnumerator OtraPista()
    {
        Debug.Log("Cambiando pista");
        pista.SetBool("Salida",salida);
        yield return new WaitForSeconds(0.5f);
        
        if(SiguientePista()){
            pista.SetBool("Salida", !salida);
        } else {
            // No hay más testigos
            //Poner al bigote
            pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = "Ya pasaron todos los testigos, a quien acusas como culpable?";
            pista.SetBool("Salida", !salida);
            Debug.Log("No hay más testigos");
        }
        
    } 
    public void ButtonPista(){
        StartCoroutine(OtraPista());
    }
    
    public bool SiguientePista()
    {
        if (pistaActual >= npcs[0].GetFeatures().Count) {
            return false;
        }
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = npcs[0].GetFeatures()[pistaActual].GetPista();
        pistaActual++;
        
        return true;
    }

}
