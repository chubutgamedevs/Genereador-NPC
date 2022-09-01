using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feature : MonoBehaviour
{
	public Image imagen;
	public string pista;

	public FeatureData[] data;


	public void Randomize()
	{
		int i = Random.Range(0, data.Length);
		imagen.sprite = data[i].sprite;
		pista = data[i].pista;
	}
} 
