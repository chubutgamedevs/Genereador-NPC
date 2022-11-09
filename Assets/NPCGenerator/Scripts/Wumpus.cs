using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wumpus : NPCGenerator
{
    public Color color = Color.white;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer estomago;
    private SpriteRenderer[] _features;

    protected override void Awake(){
        base.Awake();
        _features = featureGroup.GetComponentsInChildren<SpriteRenderer>();
    }

    void Start()
    {
        SetColor(color);
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

    public void SetColor(Color color){
        body.color = color;
        foreach (SpriteRenderer feat in _features)
        {
            feat.color = color;
        }
    }
}
