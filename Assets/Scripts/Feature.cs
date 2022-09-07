using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feature : MonoBehaviour
{
	public Image imagen;
	public string pista;
	public bool esBody = false;

	public FeatureData[] data;
	private int adn;


	public int Randomize()
	{
		int i = Random.Range(0, data.Length);
		imagen.sprite = data[i].sprite;
		pista = data[i].pista;


		adn = i;
		return i;
	}

	public void Mutar()
    {
		adn = (adn + Random.Range(1, data.Length)) % data.Length;
		imagen.sprite = data[adn].sprite;
		pista = data[adn].pista;
	}

	//private void ColorBody()
	//{
	//	float sat = -1 / 4f + 0.5f + Random.value / 2f;
	//	float val = 1f - sat / 2f;
	//	float hue = Random.Range(40, 60) / 360f;

	//	if (Random.Range(0, 100) == 5)
	//	{
	//		hue = Random.Range(0, 360) / 360f;
	//	}

	//	imagen.color = Color.HSVToRGB(hue, sat, val);
	//}
}	

