using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WumpusColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Color color = Color.white;
    [SerializeField] SpriteRenderer body;
    [SerializeField] Transform featureGroup;
    [SerializeField] SpriteRenderer estomago;
    private SpriteRenderer[] _features;
    void Awake(){
        _features = featureGroup.GetComponentsInChildren<SpriteRenderer>();
    }
    void Start()
    {
        body.color = color;
        foreach (SpriteRenderer feat in _features)
        {
            feat.color = color;
        }
    }
    
    public void RayosX(){
        DOTween.To(
            () => body.material.GetFloat("_Value"), 
            x => body.material.SetFloat("_Value", x), 
            0.0f, 2.0f)
            .OnComplete(() => estomago.enabled = true );
        foreach (SpriteRenderer feat in _features)
        {
            DOTween.To(() => feat.material.GetFloat("_Value"), x => feat.material.SetFloat("_Value", x), 0.0f, 2.0f);
        }
    }

    void OnDestroy() {
        Destroy(body.material);
        foreach (SpriteRenderer feat in _features)
        {
            Destroy(feat.material);
        }
    }
}
