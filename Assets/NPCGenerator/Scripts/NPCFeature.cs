using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCFeature : MonoBehaviour
{
    [SerializeField] int _index = 0;
    [SerializeField] List<NPCFeatureData> _data;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int GetIndex(){
        return _index;
    }

    public string GetPista(){
        return _data[_index].pista;
    }

    
    public void SetIndex(int index)
    {
		_index = index % _data.Count; // Acepta cualquier indice y lo acomoda a la cantidad de sprites
        _spriteRenderer.sprite = _data[_index].sprite;
	}

    public int Generate(){
        int i = Random.Range(0, _data.Count);
        SetIndex(i);
        return i;

    }

    public int Mutate(){
        int i = _index + Random.Range(1, _data.Count);
        SetIndex(i);
        return i;
    }
}
