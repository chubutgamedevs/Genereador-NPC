using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFeature : MonoBehaviour
{
    [SerializeField] int _index = 0;
    [SerializeField] List<Sprite> _sprites;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int GetIndex(){
        return _index;
    }

    public void SetIndex(int index)
    {
		_index = index % _sprites.Count; // Acepta cualquier indice y lo acomoda a la cantidad de sprites
        _spriteRenderer.sprite = _sprites[_index];
	}

    public int Generate(){
        int i = Random.Range(0, _sprites.Count);
        SetIndex(i);
        return i;
    }

    public int Mutate(){
        int i = _index + Random.Range(1, _sprites.Count);
        SetIndex(i);
        return i;
    }
}
