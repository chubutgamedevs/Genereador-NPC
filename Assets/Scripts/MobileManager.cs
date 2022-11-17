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
    float speed = 10.0f;
    private string Aceleracion;
    public GameObject pistasobject;
        
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<Wumpus>().ToList();
        Variantes();      
    }   

    private void Update() {
        Acelerometro();    
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

    public void Acelerometro(){
        //Vector3 dir = Vector3.zero;

        // we assume that device is held parallel to the ground
        // and Home button is in the right hand

        // remap device acceleration axis to game coordinates:
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis
        Debug.Log("EJE X: " +Input.acceleration.x +" EJE Y:"+ Input.acceleration.y+ " EJE Z:" + Input.acceleration.z);
        Aceleracion = ("EJE X: " +Input.acceleration.x +" EJE Y:"+ Input.acceleration.y+ " EJE Z:" + Input.acceleration.z);
        //dir.x = -Input.acceleration.y;
        //dir.z = Input.acceleration.x;

        // clamp acceleration vector to unit sphere
        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        //dir *= Time.deltaTime;

        // Move object
        //transform.Translate(dir * speed);
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = Aceleracion;
    }
}
