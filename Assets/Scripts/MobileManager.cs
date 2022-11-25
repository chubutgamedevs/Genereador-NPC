using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;
using UnityEngine.Android;
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
    [SerializeField] private AudioClip _ruleta, _sShot;
    [SerializeField] private AudioSource _source;
    private int loops;
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
        for (int i = 0; i < 30; i++) 
        {
            _source.PlayOneShot(_ruleta);
        }
        
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
    public GameObject EsconderCanvas;
    public void ScreenShot(){
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        EsconderCanvas.SetActive(false);
        _source.PlayOneShot(_sShot);
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "GDV" )
            .SetText( "Hola! Te comparto mi wumpus!")
            .SetUrl( "https://github.com/chubutgamedevs" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            ScreenCapture.CaptureScreenshot("SomeWumpus"+Time.deltaTime+".png");
            yield return new WaitForSeconds(1f);
            EsconderCanvas.SetActive(true);
            foreach (Transform trans in sospechosos.GetComponentInChildren<Transform>())
            {
                trans.transform.DOScale(Vector3.zero,1f);
            } 
            foreach (Transform trans in sospechosos.GetComponentInChildren<Transform>())
            {
                trans.transform.DOScale(Vector3.one, 0f);
            } 
            yield return new WaitForSeconds(1f);
            Girar();            

        // Solo Android, Compartir directo en whatsapp:
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
    public void WumpusVerse(){
        StartCoroutine(BigBang());
    }
    private IEnumerator BigBang(){
        EsconderCanvas.SetActive(false);
        loops = 0;
     while (true){
            EsconderCanvas.SetActive(false);
            ScreenCapture.CaptureScreenshot("/home/gdv/Descargas/Wumpus/SomeWumpus"+Time.deltaTime+".png",4);
            ScreenCapture.CaptureScreenshot("SomeWumpus"+Time.deltaTime+".png");
            yield return new WaitForEndOfFrame();
            Variantes();
            loops = loops +1;
            yield return new WaitForEndOfFrame();
        }
    }
}
