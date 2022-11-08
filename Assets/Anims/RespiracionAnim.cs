using UnityEngine;
using DG.Tweening;

public class RespiracionAnim : MonoBehaviour
{
    public Vector3 amplitud = new Vector3(0,0.1f,0); // (X,Y,Z)
    private Tweener tween;
    
    private void Start(){
        tween = transform
            .DOScale(transform.localScale + amplitud, Random.Range(3.0f ,5.0f))
            .SetDelay(Random.Range(0.0f, 1.0f))
            .SetLoops(-1, LoopType.Yoyo); // Yoyo hace que la animación vaya y vuelva.
    }

    private void OnDestroy(){
        tween.Kill(); // Elimina el tween cuando el objeto no existe más.
    }
}