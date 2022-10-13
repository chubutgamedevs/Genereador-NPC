using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] List<NPCFeature> _features;

    private void Awake(){
        _features = new List<NPCFeature>(transform.GetComponentsInChildren<NPCFeature>());
    }

    public List<NPCFeature> GetFeatures(){
        return _features;
    }

    public void Generate() => _features.ForEach(feat => feat.Generate());

    public void Clonate(NPCGenerator npc)
    {
        int i = 0;
        foreach (NPCFeature feat in npc.GetFeatures())
        {
            _features[i++].SetIndex(feat.GetIndex());  
        }
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
