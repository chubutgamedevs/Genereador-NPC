using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gamemanager : MonoBehaviour
{
    public GameObject indicador;
    public CharacterPower original;
    public CharacterPower clon0;
    public CharacterPower clon1;
    public CharacterPower clon2;
    public CharacterPower clon3;
    public CharacterPower clon4;
    public CharacterPower clon5;
    public CharacterPower clon6;
    
    public List<CharacterPower> sospechosos;

    private static gamemanager instance;

    public string acusado;
    private string chorro;

    public static gamemanager GetInstance()
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
        MutarYClonar();
    }

    public void MutarYClonar()
    {
        sospechosos = sospechosos.OrderBy(_ => Random.value).ToList();
        original = sospechosos[0];
        indicador.transform.position = original.transform.position;
        original.Randomize();

        for (int i = 1; i<sospechosos.Count; i++)
        {
            sospechosos[i].Clonar(original.adn);
            sospechosos[i].Mutar(i%3);
        }
    }
    public void Criminal()
    {
        chorro = original.name;
        if (chorro == acusado)
        {
            Debug.Log("Encontraste al culpable, enhorabuena");
        }
        else
        {
            Debug.Log("Fusilaron a un inoscente");
        }
    }

    public void whoIs()
    {

    }
}
