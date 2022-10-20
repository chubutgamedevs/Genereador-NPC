using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinSceneManager : MonoBehaviour
{
    [SerializeField] GameObject sospechosos;
    private List<NPCGenerator> npcs;
    private MainGameManager gm;
    
    
    // public GameObject pistas;
    public Animator pista;
    public GameObject pistasobject;
    
    private int pistaActual = 0;    
    public GameObject elBotonDeLaSiguientePista;

    [SerializeField] Mensaje mensaje;
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<NPCGenerator>().ToList();
        Variantes();

        

        
        SiguientePista();        
    }   
    public void GenerateNPCs() => npcs.ForEach(npc => npc.Generate());

    public void Acusar(NPCGenerator npc)
    {
        gm.acusado = npc;
        Final();
    }

    public void Variantes()
    {
        npcs = npcs.OrderBy(_ => Random.value).ToList();
        Debug.Log("El culpable es " +npcs[0].name);
        npcs[0].Generate();
        npcs[0].culpable = true;

        for (int i = 1; i < npcs.Count; i++)
        {
            npcs[i].Clonate(npcs[0]);
            npcs[i].Mutate(i);
            npcs[i].culpable = false;
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
        pista.SetBool("Salida",true);
        mensaje.Esconder();

        yield return new WaitForSeconds(0.5f);
        
        
        string pp = SiguientePista();
        if(pista != null){
            pista.SetBool("Salida", false);
            mensaje.Mostrar(pp);
        } else {
            // No hay más testigos
            //Poner al bigote
            pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = "Ya pasaron todos los testigos, a quien acusas como culpable?";
            pista.SetBool("Salida", false);
            Debug.Log("No hay más testigos");
            mensaje.Mostrar("No hay más testigos");
            elBotonDeLaSiguientePista.SetActive(false);
        }
        
    } 
    public void ButtonPista(){
        StartCoroutine(OtraPista());
    }
    
    public string SiguientePista()
    {
        
        if (pistaActual >= npcs[0].GetFeatures().Count) {
            return null;
        }
        string pista = npcs[0].GetFeatures()[pistaActual].GetPista();
        pistaActual++;
        
        return pista;
    }
}
