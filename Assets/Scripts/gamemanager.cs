using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public CharacterPower original;
    public CharacterPower clon0;
    public CharacterPower clon1;
    public CharacterPower clon2;

    public List<GameObject> sospechosos;

    private void Start()
    {
        GameObject[] clon = GameObject.FindGameObjectsWithTag("clon");
    }
    public void MutarYClonar()
    {
        original.Randomize();
        clon0.Clonar(original.adn);
        clon0.Mutar(0);

        clon1.Clonar(original.adn);
        clon1.Mutar(1);

        clon2.Clonar(original.adn);
        clon2.Mutar(2);
    }
}
