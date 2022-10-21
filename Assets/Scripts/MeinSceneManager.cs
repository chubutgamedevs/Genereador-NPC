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
    public botonpista bt;
    // public GameObject pistas;
    public Animator pista;
    private int pistaActual = 0;    

    [SerializeField] Mensaje mensaje;
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<NPCGenerator>().ToList();
        Variantes();
        mensaje.Mostrar("<-- Tocando aqui llamaras al siguiente testigo, suerte civil. Que el peso de la justicia te acompañe");
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
        mensaje.Esconder();

        yield return new WaitForSeconds(0.5f);
        
        
        string pp = SiguientePista();
        if(pp != null){
            mensaje.Mostrar(pp);
        } else {
            Debug.Log("No hay más testigos");
            mensaje.Mostrar("No hay más testigos, condena a alguien");
            Destroy(bt);
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
