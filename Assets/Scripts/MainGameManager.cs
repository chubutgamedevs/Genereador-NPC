using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] List<NPCGenerator> npcs;

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
    }


}
