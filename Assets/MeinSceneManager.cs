using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinSceneManager : MonoBehaviour
{
    [SerializeField] List<NPCGenerator> npcs;
    private MainGameManager gm;
    
    private bool salida = true;
    public GameObject pistas;
    public Animator pista;
    public GameObject pistasobject;
    private string texto;
    private int pistaActual = 0;
    
    public GameObject elBotonDeLaSiguientePista;
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
    
        GenerateNPCs();
        gm.SetCulpable(npcs[0]);


        SiguientePista();
    }   
    public void GenerateNPCs() => npcs.ForEach(npc => npc.Generate());

    public void Acusar(NPCGenerator npc)
    {
        gm.SetAcusado(npc);
        Final();
    }

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

    public void Final()
    {
        // Transición entre escenas
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
            elBotonDeLaSiguientePista.SetActive(false);
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
