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
    public GameObject rueda;
    private float tiempogiro = 0.7f;
    private float paso = 360/6;

    private bool _entradaDeshabilitada = false;
        
    public GameObject shareButton;
    private void Start()
    {
        gm = MainGameManager.GetInstance();
        // Mapea los npcs automaticamente.
        npcs = sospechosos.GetComponentsInChildren<Wumpus>().ToList();
        acomodarWumpus();
        Variantes();      
        Acelerometro();
        shareButton.transform.DOScale(1.5f, 0.3f).SetEase(Ease.OutElastic);
    }   

    private void Update() {   
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
        Debug.Log("EJE X: " +(Input.acceleration.x*10) +" EJE Y:"+ (Input.acceleration.y*10)+ " EJE Z:" + (Input.acceleration.z*10));
        //dir.x = -Input.acceleration.y;
        //dir.z = Input.acceleration.x;

        // clamp acceleration vector to unit sphere
        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        //dir *= Time.deltaTime;

        // Move object
        //transform.Translate(dir * speed);
        StartCoroutine(Debugtext());
    }
   IEnumerator Debugtext(){
        while (true){
        pistasobject.GetComponent<TMPro.TextMeshProUGUI>().text = Aceleracion;
        yield return new WaitForSeconds(0.5f);
        }
    }
    public IEnumerator Rotar(){
        if (_entradaDeshabilitada) yield break;
        
        _entradaDeshabilitada = true;
        shareButton.SetActive(false);
                
        GenerateNPCs();

        Tween t = rueda.transform.DORotate(
                new Vector3(0, 0, 3f*360+60),
                tiempogiro*6,
                RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutQuint);
        
        yield return t.WaitForCompletion();

        shareButton.transform.localScale = Vector3.one * 0.5f;
        shareButton.transform.DOScale(1.5f, 0.3f).SetEase(Ease.OutElastic);
        
        
        
        shareButton.SetActive(true);
        _entradaDeshabilitada = false;
                
    }
    public void Girar(){
        StartCoroutine(Rotar());
    }

    private void acomodarWumpus(){
        float i = 0;
        foreach (Transform trans in sospechosos.GetComponentInChildren<Transform>())
        {
            trans.position = new Vector3(
                25f * Mathf.Cos((i+0.5f) * Mathf.PI/3f),
                25f * Mathf.Sin((i+0.5f) * Mathf.PI/3f),
                trans.position.z);
            trans.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg *((i-1f) * Mathf.PI/3f),Vector3.forward);
            i -= 1;
        } 
    }
    public void NFTWumpus(){
        foreach (Transform trans in sospechosos.GetComponentInChildren<Transform>()){
            Variantes();
        }
    }
}
