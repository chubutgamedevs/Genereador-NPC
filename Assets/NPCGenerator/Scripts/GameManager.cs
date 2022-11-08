using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject npcs;
    private List<NPCGenerator> _npcs;

    private void Awake(){
        _npcs = new List<NPCGenerator>(npcs.GetComponentsInChildren<NPCGenerator>());
        Debug.Log("Wumpuses recolectados" + _npcs.Count);
    }


    private void Start()
    {
        GenerateNPCs();
    }

    public void GenerateNPCs() => _npcs.ForEach(npc => npc.Generate());

    public void Variantes() {
        _npcs[0].Generate();

        for (int i = 1; i < _npcs.Count; i++)
        {
            _npcs[i].Clonate(_npcs[0]);
            _npcs[i].Mutate(i);
        }
    }
}
