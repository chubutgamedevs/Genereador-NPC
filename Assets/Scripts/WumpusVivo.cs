using UnityEngine;
using DG.Tweening;

public class WumpusVivo : MonoBehaviour
{
    public Vector3 respirar = new Vector3(0,0.1f,0); // (X,Y,Z)
    
    private void Start(){
        Vector3 normal =  transform.localScale;
        float agitado = Random.Range(3.0f ,5.0f);

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(normal+respirar, agitado));
        s.Append(transform.DOScale(normal, agitado));
        s.SetLoops(-1);
        s.SetDelay(Random.Range(0.0f, 1.0f));
    }
}