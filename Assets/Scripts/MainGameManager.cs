using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MainGameManager : MonoBehaviour
{
    
    private static MainGameManager instance;
    public NPCGenerator culpable;
    public NPCGenerator acusado;
    

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
   
    public void SetCulpable(NPCGenerator npc) {
        culpable = npc;
    }

    public void SetAcusado(NPCGenerator npc)
    {
        acusado = npc;
    }

    public bool IsAcusadoCulpable(){
        return acusado == culpable;
    }
    
}
