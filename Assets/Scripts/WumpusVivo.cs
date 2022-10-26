using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WumpusVivo : MonoBehaviour
{
    public Vector3 respirar; // (X,Y,Z)
    private Vector3 normal;
    private float agitado;


    private void Start(){
        respirar = new Vector3(0,0.1f,0);
        normal =  transform.localScale;
        StartCoroutine(StartTween());
        agitado = Random.Range(2.0f ,2.5f);
    }
    IEnumerator StartTween()
    {
        Tween initialTween = gameObject.transform.DOScale(normal + respirar, 1f).SetDelay(Random.Range(0,1.3f));

        yield return initialTween.WaitForCompletion();

        DOTween
            .Sequence()
            .Append
            (
                gameObject.transform.DOScale(normal+respirar, agitado)
            )
            .Append
            (
                gameObject.transform.DOScale(normal, agitado)
            )
            .Append
            (
                gameObject.transform.DOScale(normal+respirar, agitado)
            )
            .SetLoops(-1);

        yield return 0f;
    }

}
