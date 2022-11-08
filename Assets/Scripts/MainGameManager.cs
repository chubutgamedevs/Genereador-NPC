using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;


public class MainGameManager : MonoBehaviour
{

    private static MainGameManager instance = null;
    private Wumpus _acusado;

    //Singleton
    public static MainGameManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Acusar(Wumpus npc){
        _acusado = npc;
    }

    public Wumpus GetAcusado(){
        return _acusado;
    }
}
