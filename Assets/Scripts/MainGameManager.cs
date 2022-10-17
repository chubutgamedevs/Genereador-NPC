using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] List<NPCGenerator> npcs;
    private static MainGameManager instance;
    private bool salida;
    public GameObject trans;
    public GameObject panels;
    public Animator transs;
    public Animator panelss;

    //Singletone de gamemanager
    public static MainGameManager GetInstance()
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
        Dialogo();
    }
    public void Final()
    {
        transs.SetBool("Salida", salida);
        SceneManager.LoadScene("Final");
        salida = !salida;
        transs.SetBool("Salida", salida);
    }
    public void Dialogo()
    {
        panelss.SetBool("Salida", !salida);
    }

}
