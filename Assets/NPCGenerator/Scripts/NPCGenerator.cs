using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{   
    [SerializeField] protected Transform featureGroup; 
    private List<NPCFeature> _features;
    public bool culpable = false;

    protected virtual void Awake(){
        _features = new List<NPCFeature>(featureGroup.GetComponentsInChildren<NPCFeature>());
    }

    public List<NPCFeature> GetFeatures() => _features;

    public void Generate() => _features.ForEach(feat => feat.Generate());

    public void Clonate(NPCGenerator npc)
    {
        int i = 0;
        foreach (NPCFeature feat in npc.GetFeatures())
        {
            _features[i++].SetIndex(feat.GetIndex());  
        }
        culpable = npc.culpable;
    }

    public void Mutate(int feat) => _features[feat % _features.Count].Mutate();

    public List<int> GetDNA(){
        List<int> dna = new List<int>();
        foreach (NPCFeature feat in _features)
        {
            dna.Add(feat.GetIndex());
        }
        return dna;
    }
}
