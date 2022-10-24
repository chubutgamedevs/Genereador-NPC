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
    public NPCGenerator acusado;
    public GameObject A;
    public GameObject B;
    
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

    public void EntrarNivel(){
        A.transform.DOMoveX(20 , 0.5f);
        B.transform.DOMoveX(-20, 0.5f);
    }
    public void SalirNivel(){
        A.transform.DOMoveX(-45, 0.5f);
        B.transform.DOMoveX(45, 0.5f);
    }
}
