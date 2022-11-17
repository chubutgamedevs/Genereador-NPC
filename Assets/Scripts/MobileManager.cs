using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MobileManager : MonoBehaviour
{
    [SerializeField] GameObject sospechosos;
    private List<Wumpus> npcs;
    private MainGameManager gm;  
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<Wumpus>().ToList();
        Variantes();      
    }   
    
    public void Acusar(Wumpus npc)
    {
        gm.Acusar(npc);
    }

    public void GenerateNPCs()
    {
        SetearColores();
        npcs.ForEach(npc => npc.Generate());        
    }

    public void Variantes()
    {
        SetearColores();

        npcs = npcs.OrderBy(_ => Random.value).ToList();
        npcs[0].Generate();
        npcs[0].culpable = true;


        for (int i = 1; i < npcs.Count; i++)
        {
            npcs[i].Clonate(npcs[0]);
            npcs[i].Mutate(i);
            npcs[i].culpable = false;
        }
    }


    public void SetearColores()
    {
        int salto = 360 / npcs.Count(); //Cuantos Wumpus hay?

        int hue_base = Random.Range(0, 360); 
        foreach (Wumpus w in npcs)
        {
            w.SetColor(
                Color.HSVToRGB(hue_base/360f,1f,1f)
            );
            hue_base = (hue_base + salto) % 360;
        }
    }
}
