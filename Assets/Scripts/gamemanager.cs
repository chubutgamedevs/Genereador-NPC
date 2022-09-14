using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gamemanager : MonoBehaviour
{
    public CharacterPower original;
    public CharacterPower clon0;
    public CharacterPower clon1;
    public CharacterPower clon2;
    public CharacterPower clon3;
    public CharacterPower clon4;
    public CharacterPower clon5;
    public CharacterPower clon6;
    
    public List<CharacterPower> sospechosos;

    private void Start()
    {

        // GameObject[] clon = GameObject.FindGameObjectsWithTag("clon");

        //sospechosos = new List<CharacterPower>();
        MutarYClonar();

    }

    public void MutarYClonar()
    {
        sospechosos = sospechosos.OrderBy(_ => Random.value).ToList();
        original = sospechosos[0];
        original.Randomize();

        for (int i = 1; i<sospechosos.Count; i++)
        {
            sospechosos[i].Clonar(original.adn);
            sospechosos[i].Mutar(i%3);
        }
    }
}
