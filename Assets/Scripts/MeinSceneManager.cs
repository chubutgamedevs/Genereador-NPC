using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinSceneManager : MonoBehaviour
{
    [SerializeField] GameObject sospechosos;
    private List<Wumpus> npcs;
    private MainGameManager gm;
    public botonpista bt;
    public Animator pista;
    private int pistaActual = 0;    
    [SerializeField] Cortina cortina;

    [SerializeField] Mensaje mensaje;
    
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<Wumpus>().ToList();
        Variantes();
        mensaje.Mostrar("Las moscas son tus testigos, Tocando aqui llamaras a la siguiente. Que el peso de la justicia te acompañe.");
        SiguientePista(); 
        cortina.Abrir();       
    }   
    
    public void Acusar(Wumpus npc)
    {
        gm.Acusar(npc);
        Final();
    }

    public void GenerateNPCs()
    {
        npcs.ForEach(npc => npc.Generate());
        SetearColores();
    }

    public void Variantes()
    {
        SetearColores();

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
        StartCoroutine(MasEsperas());
    }

    IEnumerator OtraPista()
    {
        mensaje.Esconder();

        yield return new WaitForSeconds(0.5f);
        
        
        string pp = SiguientePista();
        if(pp != null){
            mensaje.Mostrar(pp);
        } else {
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
    IEnumerator MasEsperas(){
        cortina.Cerrar();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Final");
    }

    public void SetearColores()
    {
        int salto = 360 / npcs.Count(); //Cuantos Wumpus hay?

        int hue_base = Random.Range(0, 360); 
        foreach (Wumpus w in npcs)
        {
            w.SetColor(
                Color.HSVToRGB(hue_base/360f,1f,1f)
            );
            hue_base = (hue_base + salto) % 360;
        }
    }
}
