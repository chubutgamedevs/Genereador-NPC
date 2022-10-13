using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<NPCGenerator> npcs;

    private void Start()
    {
        GenerateNPCs();
    }

    public void GenerateNPCs() => npcs.ForEach(npc => npc.Generate());

    public void Variantes() {
        npcs[0].Generate();

        for (int i = 1; i < npcs.Count; i++)
        {
            npcs[i].Clonate(npcs[0]);
            npcs[i].Mutate(i);
        }
    }
}
